using DriveAnheu.Application.UseCases.Usuarios.ObterUsuarioCache;
using DriveAnheu.Application.UseCases.Usuarios.Shared.Output;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DriveAnheu.API.Controllers
{
    public abstract class BaseController<T> : Controller
    {
        protected Guid ObterUsuarioGuid()
        {
            if (User.Identity!.IsAuthenticated)
            {
                string guid = User.FindFirst(ClaimTypes.Thumbprint)!.Value;
                return Guid.Parse(guid);
            }

            return Guid.Empty;
        }

        protected async Task<int> ObterUsuarioId()
        {
            var service = HttpContext.RequestServices.GetService<IObterUsuarioCacheQuery>();
            UsuarioOutput? usuario = await service!.Execute(ObterUsuarioGuid());

            return usuario is not null ? usuario.UsuarioId : 0;
        }
    }
}