using AutoMapper;
using DriveAnheu.Application.AutoMapper;
using DriveAnheu.Application.UseCases.Logs;
using DriveAnheu.Application.UseCases.Pastas;
using DriveAnheu.Application.UseCases.Usuarios;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DriveAnheu.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjectionApplication(this IServiceCollection services, WebApplicationBuilder builder)
        {
            AddAutoMapper(services);
            AddLogger(builder);
            AddSignalR(services);

            AddUseCases(services);

            return services;
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(x =>
            {
                x.AddProfile(new AutoMapperConfig());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        private static void AddLogger(WebApplicationBuilder builder)
        {
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
        }

        private static void AddSignalR(IServiceCollection services)
        {
            services.AddSignalR();
        }

        private static void AddUseCases(IServiceCollection services)
        {
            services.AddLogsApplication();
            services.AddUsuariosApplication();
            services.AddPastasApplication();
        }
    }
}