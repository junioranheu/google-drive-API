using DriveAnheu.Application.UseCases.Pastas.CriarPasta;
using DriveAnheu.Application.UseCases.Pastas.ListarPasta;
using DriveAnheu.Application.UseCases.Pastas.ObterPasta;
using Microsoft.Extensions.DependencyInjection;

namespace DriveAnheu.Application.UseCases.Pastas
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPastasApplication(this IServiceCollection services)
        {
            services.AddScoped<IObterPastaQuery, ObterPastaQuery>();
            services.AddScoped<IListarPastaQuery, ListarPastaQuery>();
            services.AddScoped<ICriarPastaCommand, CriarPastaCommand>();

            return services;
        }
    }
}