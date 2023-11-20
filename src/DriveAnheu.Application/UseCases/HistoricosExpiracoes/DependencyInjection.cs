using DriveAnheu.Application.UseCases.HistoricosExpiracoes.ObterDataUltimoHistoricoExpiracao;
using Microsoft.Extensions.DependencyInjection;

namespace DriveAnheu.Application.UseCases.HistoricosExpiracoes
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddHistoricosExpiracoesApplication(this IServiceCollection services)
        {
            services.AddScoped<IObterDataUltimoHistoricoExpiracaoQuery, ObterDataUltimoHistoricoExpiracaoQuery>();

            return services;
        }
    }
}