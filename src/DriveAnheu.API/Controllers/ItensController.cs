﻿using DriveAnheu.Application.UseCases.Itens.ChecarValidadeItem;
using DriveAnheu.Application.UseCases.Itens.CriarItem;
using DriveAnheu.Application.UseCases.Itens.DeletarItem;
using DriveAnheu.Application.UseCases.Itens.ListarFolderRotas;
using DriveAnheu.Application.UseCases.Itens.ListarItem;
using DriveAnheu.Application.UseCases.Itens.ObterItem;
using DriveAnheu.Application.UseCases.Itens.Shared.Input;
using DriveAnheu.Application.UseCases.Itens.Shared.Output;
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
        IDeletarItemCommand _deletarItemCommand
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

            await _checarValidadeItemCommand.Execute();
            ItemOutput? output = await _obterItemQuery.Execute(guid);

            if (output is null)
            {
                throw new Exception(ObterDescricaoEnum(CodigoErroEnum.NaoEncontrado));
            }

            return Ok(output);
        }

        [HttpGet("listar")]
        [Authorize]
        public async Task<ActionResult<ItemOutput>> ListarPorGuidPastaPai(Guid guidPastaPai)
        {
            await _checarValidadeItemCommand.Execute();
            List<ItemOutput>? output = await _listarItemQuery.Execute(guidPastaPai);

            return Ok(output);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Criar([FromForm] ItemInput input)
        {
            await _criarItemCommand.Execute(input);
            return Ok(true);
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

            Guid usuarioGuid = ObterUsuarioGuid();
            await _deletarItemCommand.Execute(guid, usuarioGuid);

            return Ok(true);
        }
    }
}