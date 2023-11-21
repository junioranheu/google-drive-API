using DriveAnheu.Application.UseCases.HistoricosExpiracoes.ObterDataUltimoHistoricoExpiracao;
using DriveAnheu.Application.UseCases.Itens.ChecarValidadeItem;
using DriveAnheu.Domain.Consts;
using DriveAnheu.Domain.Enums;
using DriveAnheu.Infrastructure.Factory.ConnectionFactory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static junioranheu_utils_package.Fixtures.Get;

namespace DriveAnheu.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuxiliaresController(
        IConnectionFactory _connectionFactory,
        IChecarValidadeItemCommand _checarValidadeItemCommand,
        IObterDataUltimoHistoricoExpiracaoQuery _obterDataUltimoHistoricoExpiracaoQuery
        ) : Controller
    {
        [HttpGet("obterStatusBanco")]
        [AllowAnonymous]
        public async Task<ActionResult<(bool, string)>> ObterStatusBanco(CancellationToken cancellationToken = default)
        {
            bool isOk = true;
            string desc = string.Empty;

            try
            {
                var connection = _connectionFactory.ObterMySqlConnection();

                await connection.OpenAsync(cancellationToken);
                return Ok(new { isOk, desc });
            }
            catch (Exception ex)
            {
                isOk = false;
                return Ok(new { isOk, ex.Message });
            }
        }

        [HttpGet("listarItemTipoEnum")]
        [ResponseCache(Duration = TemposConst.UmaHoraEmSegundos)]
        [AllowAnonymous]
        public ActionResult<List<dynamic>> ListarItemTipoEnum()
        {
            var lista = ListarEnum<ItemTipoEnum>();

            if (lista.Count == 0)
            {
                throw new Exception(ObterDescricaoEnum(CodigoErroEnum.NaoEncontrado));
            }

            return Ok(lista);
        }

        [HttpPost("forcarChecarValidadeItem")]
        [Authorize]
        public async Task<ActionResult> ForcarChecarValidadeItem(int m)
        {
            if (GerarHorarioBrasilia().Minute != m)
            {
                return StatusCode(StatusCodes.Status403Forbidden, "O parâmetro m está incorreto");
            }

            await _checarValidadeItemCommand.Execute(isForcar: true);
            return Ok(true);
        }

        [HttpGet("obterInfosHistoricoExpiracao")]
        [ResponseCache(Duration = TemposConst.UmMinutoEmSegundos)]
        [AllowAnonymous]
        public async Task<ActionResult<(DateTime, DateTime)>> ObterInfosHistoricoExpiracao()
        {
            DateTime? dataUltimoRegistro = await _obterDataUltimoHistoricoExpiracaoQuery.Execute();

            if (dataUltimoRegistro is null)
            {
                throw new Exception("Não há histórico de expiração no momento");
            }

            DateTime proximaVarredura = dataUltimoRegistro.GetValueOrDefault().AddHours(SistemaConst.OffsetChecarValidadeItemEmHoras);

            return Ok(new { dataUltimoRegistro, proximaVarredura });
        }
    }
}