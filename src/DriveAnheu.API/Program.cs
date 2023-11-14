using DriveAnheu.API;
using DriveAnheu.Application.Hubs.MiscHub;
using DriveAnheu.Domain.Consts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.FileProviders;
using Swashbuckle.AspNetCore.SwaggerUI;

#region builder
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddDependencyInjectionAPI();
    builder.Services.AddDependencyInjectionApplication(builder);
    builder.Services.AddDependencyInjectionInfrastructure(builder);
}
#endregion

#region app
WebApplication app = builder.Build();
{
    using IServiceScope scope = app.Services.CreateScope();
    IServiceProvider services = scope.ServiceProvider;

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{SistemaConst.NomeSistema}.API");
            // c.RoutePrefix = ""; // ***
            c.DocExpansion(DocExpansion.None);
        });

        app.UseDeveloperExceptionPage();
    }

    if (app.Environment.IsProduction())
    {
        app.UseHttpsRedirection();
    }

    app.UseCors(builder.Configuration["CORSSettings:Cors"]!);

    /// <summary>
    /// O trecho "app.UseWhen" abaixo é necessário quando a API tem uma resposta IAsyncEnumerable/Yield;
    /// O "UseResponseCompression" conflita com esse tipo de requisição, portanto é obrigatória a verificação abaixo;
    /// Caso não existam requisições desse tipo na API, é apenas necessário o trecho "app.UseResponseCompression()";
    /// </summary>
    app.UseWhen(context => !IsStreamingRequest(context), x =>
    {
        x.UseResponseCompression();
    });

    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();

    app.MapHub<MiscHub>("/miscHub");

    AddStaticFiles(app);

    app.Run();
}
#endregion

#region metodos_auxiliares
static bool IsStreamingRequest(HttpContext context)
{
    Endpoint? endpoint = context.GetEndpoint();

    if (endpoint is RouteEndpoint routeEndpoint)
    {
        ControllerActionDescriptor? acao = routeEndpoint.Metadata.GetMetadata<ControllerActionDescriptor>();

        if (acao is not null)
        {
            Type? tipo = acao.MethodInfo.ReturnType;

            if (tipo.IsGenericType && tipo.GetGenericTypeDefinition() == typeof(IAsyncEnumerable<>))
            {
                return true;
            }

            return false;
        }
    }

    return false;
}

static void AddStaticFiles(WebApplication app)
{
    IWebHostEnvironment env = app.Environment;

    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "Uploads")),
        RequestPath = "/Uploads",

        OnPrepareResponse = ctx =>
        {
            ctx.Context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
            ctx.Context.Response.Headers.Append("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
        }
    });
}
#endregion