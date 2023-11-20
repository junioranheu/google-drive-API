using DriveAnheu.Application.UseCases.Usuarios.AutenticarUsuario;
using DriveAnheu.Application.UseCases.Usuarios.Shared.Output;
using DriveAnheu.Domain.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static junioranheu_utils_package.Fixtures.Get;

namespace DriveAnheu.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController(IWebHostEnvironment _environment, IAutenticarUsuarioCommand _autenticarUsuarioCommand) : BaseController<UsuariosController>
    {
        [HttpPost("autenticar")]
        [AllowAnonymous]
        public async Task<ActionResult<UsuarioOutput>> Autenticar()
        {
            if (User.Identity!.IsAuthenticated)
            {
                throw new Exception(ObterDescricaoEnum(CodigoErroEnum.UsuarioJaAutenticado));
            }

            return Ok(await _autenticarUsuarioCommand.Execute());
        }

        [HttpPost("autenticarLazy")]
        [AllowAnonymous]
#if DEBUG
        [ApiExplorerSettings(IgnoreApi = false)]
#else
[ApiExplorerSettings(IgnoreApi = true)]
#endif
        public async Task<ActionResult<string>> AutenticarLazy()
        {
            if (_environment.IsProduction())
            {
                return Ok(string.Empty);
            }

            return Ok((await _autenticarUsuarioCommand.Execute()).Token);
        }
    }
}