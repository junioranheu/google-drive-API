using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace DriveAnheu.API.Filters.Base
{
    public sealed class BaseFilter
    {
        public BaseFilter() { }

        internal static string BaseObterUsuarioId(ActionExecutedContext context)
        {
            return BaseObterUsuarioId(context);
        }

        internal static string BaseObterUsuarioId(AuthorizationFilterContext context)
        {
            return BaseObterUsuarioId(context);
        }

        internal static string BaseObterUsuarioId(ExceptionContext context)
        {
            return BaseObterUsuarioId(context);
        }

        #region usuario_id
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Marcar membros como estáticos", Justification = "<Pendente>")]
        internal int BaseObterUsuarioId(dynamic context)
        {
            if (context is ActionExecutedContext actionExecutedContext)
            {
                return ObterUsuarioId(actionExecutedContext);
            }
            else if (context is AuthorizationFilterContext authorizationFilterContext)
            {
                return ObterUsuarioId(authorizationFilterContext);
            }
            else if (context is ExceptionContext exceptionContext)
            {
                return ObterUsuarioId(exceptionContext);
            }

            return 0;

            static int ObterUsuarioId(dynamic context)
            {
                if (context.HttpContext.User.Identity!.IsAuthenticated)
                {
                    string usuarioId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
                    return Convert.ToInt32(usuarioId);
                }

                return 0;
            }
        }
        #endregion
    }
}