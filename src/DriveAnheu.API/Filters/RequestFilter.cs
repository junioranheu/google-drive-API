﻿using DriveAnheu.API.Filters.Base;
using DriveAnheu.Application.UseCases.Logs.CriarLog;
using DriveAnheu.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DriveAnheu.API.Filters
{
    public sealed class RequestFilter(ICriarLogCommand _criarLogCommand) : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext filterContextExecuting, ActionExecutionDelegate next)
        {
            if (filterContextExecuting.HttpContext.RequestAborted.IsCancellationRequested)
            {
                filterContextExecuting.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
                return;
            }

            ActionExecutedContext filterContextExecuted = await next();
            HttpRequest request = filterContextExecuted.HttpContext.Request;
            int? statusResposta = (filterContextExecuted.Result as ObjectResult)?.StatusCode;

            int usuarioId = new BaseFilter().BaseObterUsuarioId(filterContextExecuted);
            string parametros = ObterParametrosRequisicao(filterContextExecuting);
            string parametrosNormalizados = NormalizarParametros(parametros);

            Log log = new()
            {
                TipoRequisicao = request.Method ?? string.Empty,
                Endpoint = request.Path.Value ?? string.Empty,
                Parametros = parametrosNormalizados,
                Descricao = string.Empty,
                StatusResposta = statusResposta > 0 ? (int)statusResposta : StatusCodes.Status500InternalServerError,
                UsuarioId = usuarioId > 0 ? usuarioId : null
            };

            await _criarLogCommand.Execute(log);
        }

        private static string ObterParametrosRequisicao(ActionExecutingContext filterContextExecuting)
        {
            var parametros = filterContextExecuting.ActionArguments.FirstOrDefault().Value ?? string.Empty;

            try
            {
                string parametrosSerialiazed = !string.IsNullOrEmpty(parametros.ToString()) ? JsonConvert.SerializeObject(parametros) : string.Empty;
                return parametrosSerialiazed;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        private static string NormalizarParametros(string parametros)
        {
            try
            {
                if (!string.IsNullOrEmpty(parametros))
                {
                    string[] _listaKeysParaOcultarLog = ["Senha", "Password"];
                    bool isNecessitaOcultarKeys = _listaKeysParaOcultarLog.Any(x => parametros.Contains("\"" + x + "\":", StringComparison.OrdinalIgnoreCase));

                    if (isNecessitaOcultarKeys)
                    {
                        JObject? parametrosJson = JsonConvert.DeserializeObject<JObject>(parametros);

                        foreach (var item in _listaKeysParaOcultarLog)
                        {
                            OcultarKeyEmParametro(parametrosJson, item);
                        }

                        string? parametrosJsonStr = parametrosJson?.ToString(Formatting.Indented);

                        if (string.IsNullOrEmpty(parametrosJsonStr))
                        {
                            return string.Empty;
                        }

                        return parametrosJsonStr.Replace("\r\n", "") ?? string.Empty;
                    }
                }

                return parametros;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        private static void OcultarKeyEmParametro(JObject? parametroJson, string key)
        {
            if (parametroJson is not null && parametroJson[key] is not null)
            {
                parametroJson?.Property(key)?.Remove();
            }
        }
    }
}