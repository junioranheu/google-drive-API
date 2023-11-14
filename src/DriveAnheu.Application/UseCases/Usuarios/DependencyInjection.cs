using DriveAnheu.Application.UseCases.Usuarios.ObterUsuario;
using DriveAnheu.Application.UseCases.Usuarios.ObterUsuarioCache;
using Microsoft.Extensions.DependencyInjection;

namespace DriveAnheu.Application.UseCases.Usuarios
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUsuariosApplication(this IServiceCollection services)
        {
            services.AddScoped<IObterUsuarioQuery, ObterUsuarioQuery>();

            services.AddScoped<IObterUsuarioCacheQuery, ObterUsuarioCacheQuery>();

            return services;
        }
    }
}