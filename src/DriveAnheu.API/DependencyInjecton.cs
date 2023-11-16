using DriveAnheu.API.Filters;
using DriveAnheu.Application.UseCases.Itens.Shared.Input;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;
using System.Text.Json.Serialization;
using static junioranheu_utils_package.Fixtures.Get;

namespace DriveAnheu.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjectionAPI(this IServiceCollection services)
        {
            AddCompression(services);
            AddControllers(services);
            AddMisc(services);
            AddValidators(services);

            return services;
        }

        private static void AddCompression(IServiceCollection services)
        {
            services.AddResponseCompression(x =>
            {
                x.EnableForHttps = true;
                x.Providers.Add<BrotliCompressionProvider>();
                x.Providers.Add<GzipCompressionProvider>();
            });

            services.Configure<BrotliCompressionProviderOptions>(x =>
            {
                x.Level = CompressionLevel.Optimal;
            });

            services.Configure<GzipCompressionProviderOptions>(x =>
            {
                x.Level = CompressionLevel.Optimal;
            });
        }

        private static void AddControllers(IServiceCollection services)
        {
            services.AddControllers(x =>
            {
                x.Filters.Add<RequestFilter>();
                x.Filters.Add<ErrorFilter>();
            }).
                AddJsonOptions(x =>
                {
                    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    x.JsonSerializerOptions.WriteIndented = true;
                });
        }

        private static void AddMisc(IServiceCollection services)
        {
            services.AddMemoryCache();
        }

        private static void AddValidators(IServiceCollection services)
        {
            #region api_behavior_validator
            services.Configure<ApiBehaviorOptions>(o =>
            {
                o.InvalidModelStateResponseFactory = actionContext =>
                {
                    var obj = new
                    {
                        Codigo = StatusCodes.Status400BadRequest,
                        Data = ObterDetalhesDataHora(),
                        Caminho = actionContext.HttpContext.Request.Path,
                        Mensagens = actionContext.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)
                    };

                    return new JsonResult(obj);
                };
            });
            #endregion

            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();

            AssemblyScanner? validators = AssemblyScanner.FindValidatorsInAssemblyContaining<ItemInputValidator>();
            validators.ForEach(x => services.AddValidatorsFromAssemblyContaining(x.ValidatorType));
        }
    }
}