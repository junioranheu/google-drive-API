using AutoMapper;
using DriveAnheu.Application.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

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
            AddServices(services);

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
            services.AddTokensApplication();
            services.AddUsuariosApplication();
            services.AddUsuariosRolesApplication();
            services.AddWardsApplication();
            services.AddHashtagsApplication();
            services.AddWardsHashtagsApplication();
            services.AddAuxiliaresApplication();
            services.AddFeriadosApplication();
            services.AddFeriadosDatasApplication();
            services.AddFeriadosEstadosApplication();
            services.AddNewslettersCadastrosApplication();
            services.AddChatGPTApplication();
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddExportsService();
            services.AddImportsService();
            services.AddUsuariosService();
            services.AddResetarBancoDadosService();
        }
    }
}