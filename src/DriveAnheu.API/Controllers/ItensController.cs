﻿using DriveAnheu.Application.UseCases.Itens.ChecarValidadeItem;
using DriveAnheu.Application.UseCases.Itens.CriarItem;
using DriveAnheu.Application.UseCases.Itens.DeletarItem;
using DriveAnheu.Application.UseCases.Itens.EditarItem;
using DriveAnheu.Application.UseCases.Itens.ListarFolderRotas;
using DriveAnheu.Application.UseCases.Itens.ListarItem;
using DriveAnheu.Application.UseCases.Itens.ObterItem;
using DriveAnheu.Application.UseCases.Itens.ObterTamanhoEmDiscoEmBytes;
using DriveAnheu.Application.UseCases.Itens.Shared.Input;
using DriveAnheu.Application.UseCases.Itens.Shared.Output;
using DriveAnheu.Application.UseCases.Itens.UploadItem;
using DriveAnheu.Domain.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static junioranheu_utils_package.Fixtures.Get;

namespace DriveAnheu.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItensController(
        IObterItemQuery _obterItemQuery,
        IListarItemQuery _listarItemQuery,
        ICriarItemCommand _criarItemCommand,
        IChecarValidadeItemCommand _checarValidadeItemCommand,
        IListarFolderRotasQuery _listarFolderRotasQuery,
        IDeletarItemCommand _deletarItemCommand,
        IEditarItemCommand _editarItemCommand,
        IUploadItemCommand _uploadItemCommand,
        IObterTamanhoEmDiscoEmBytesQuery _obterTamanhoEmDiscoEmBytesQuery
        ) : BaseController<ItensController>
    {
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ItemOutput>> Obter(Guid guid)
        {
            if (guid == Guid.Empty)
            {
                throw new Exception(ObterDescricaoEnum(CodigoErroEnum.BadRequest));
            }

            ItemOutput? output = await _obterItemQuery.Execute(guid);

            if (output is null)
            {
                throw new Exception(ObterDescricaoEnum(CodigoErroEnum.NaoEncontrado));
            }

            return Ok(output);
        }

        [HttpGet("listar")]
        [Authorize]
        public async Task<ActionResult<ItemOutput>> ListarPorGuidPastaPai(Guid guidPastaPai, string? key = "")
        {
            await _checarValidadeItemCommand.Execute();
            List<ItemOutput>? output = await _listarItemQuery.Execute(guidPastaPai, key);

            return Ok(output);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<string>> Criar(ItemInput input)
        {
            int usuarioId = await ObterUsuarioId();
            Guid guid = await _criarItemCommand.Execute(input, usuarioId);

            return Ok(guid);
        }

        [HttpGet("listarFolderRotas")]
        [Authorize]
        public async Task<ActionResult<List<FolderRotaOutput>>> ListarFolderRotas(Guid guid)
        {
            List<FolderRotaOutput> output = await _listarFolderRotasQuery.Execute(guid);

            return Ok(output);
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult> Deletar(Guid guid)
        {
            if (guid == Guid.Empty)
            {
                throw new Exception(ObterDescricaoEnum(CodigoErroEnum.BadRequest));
            }

            int usuarioId = await ObterUsuarioId();
            await _deletarItemCommand.Execute(guid, usuarioId);

            return Ok(true);
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult> Editar(Guid guid, string nome)
        {
            if (guid == Guid.Empty)
            {
                throw new Exception(ObterDescricaoEnum(CodigoErroEnum.BadRequest));
            }

            int usuarioId = await ObterUsuarioId();
            await _editarItemCommand.Execute(guid, nome, usuarioId);

            return Ok(true);
        }

        [HttpPost("upload")]
        [Authorize]
        public async Task<ActionResult<ItemOutput>> Upload([FromForm] ItemUploadInput input)
        {
            int usuarioId = await ObterUsuarioId();
            ItemOutput? resp = await _uploadItemCommand.Execute(input, usuarioId);

            if (resp is null)
            {
                throw new Exception(ObterDescricaoEnum(CodigoErroEnum.BadRequest));
            }

            return Ok(resp);
        }

        [HttpGet("obterTamanhoEmDiscoEmBytes")]
        [Authorize]
        public async Task<ActionResult<double>> ObterTamanhoEmDiscoEmBytes()
        {
            int usuarioId = await ObterUsuarioId();
            double tamanhoEmDiscoEmBytes = await _obterTamanhoEmDiscoEmBytesQuery.Execute(usuarioId);

            return Ok(tamanhoEmDiscoEmBytes);
        }
    }
}