using DriveAnheu.Application.UseCases.Shared.Models.Output;
using DriveAnheu.Domain.Consts;
using DriveAnheu.Domain.Enums;
using DriveAnheu.Infrastructure.Factory.ConnectionFactory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Reflection;
using static junioranheu_utils_package.Fixtures.Get;

namespace DriveAnheu.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuxiliaresController(IConnectionFactory _connectionFactory) : Controller
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
        public ActionResult<List<EnumOutput>> ListarItemTipoEnum()
        {
            List<EnumOutput> lista = ListarEnum<ItemTipoEnum>();

            if (lista.Count == 0)
            {
                throw new Exception(ObterDescricaoEnum(CodigoErroEnum.NaoEncontrado));
            }

            return Ok(lista);
        }

        #region metodos_auxiliares
        /// <summary>
        /// Recebe um <Enum> e lista todos os valores dele mapeados pela classe de resposta "EnumOutput";
        /// O método trata o Enum caso ele tenha/não tenha objetos com "[Description]";
        /// </summary>
        private static List<EnumOutput> ListarEnum<TEnum>() where TEnum : Enum
        {
            return Enum.GetValues(typeof(TEnum)).
                   Cast<TEnum>().
                   Select(x =>
                   {
                       FieldInfo? info = x.GetType().GetField(x.ToString());
                       string desc = info!.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() is DescriptionAttribute descriptionAttribute ? descriptionAttribute.Description : x.ToString();
                       return new EnumOutput { Id = (int)(object)x, Item = desc };
                   }).ToList();
        }
        #endregion
    }
}