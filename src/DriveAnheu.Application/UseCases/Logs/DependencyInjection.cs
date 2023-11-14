using DriveAnheu.Application.UseCases.Logs.CriarLog;
using Microsoft.Extensions.DependencyInjection;

namespace DriveAnheu.Application.UseCases.Logs
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddLogsApplication(this IServiceCollection services)
        {
            services.AddScoped<ICriarLogCommand, CriarLogCommand>();

            return services;
        }
    }
}