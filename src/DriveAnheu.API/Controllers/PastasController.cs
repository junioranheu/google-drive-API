using DriveAnheu.Application.UseCases.Pastas.ObterPasta;
using DriveAnheu.Application.UseCases.Pastas.Shared.Output;
using DriveAnheu.Domain.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static junioranheu_utils_package.Fixtures.Get;

namespace DriveAnheu.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PastasController(IObterPastaQuery _obterPastaQuery) : BaseController<PastasController>
    {
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<PastaOutput>> Obter(Guid guid)
        {
            if (guid == Guid.Empty)
            {
                throw new Exception(ObterDescricaoEnum(CodigoErroEnum.BadRequest));
            }

            PastaOutput? output = await _obterPastaQuery.Execute(guid);

            if (output is null)
            {
                throw new Exception(ObterDescricaoEnum(CodigoErroEnum.NaoEncontrado));
            }

            return Ok(output);
        }
    }
}