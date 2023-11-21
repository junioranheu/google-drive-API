using DriveAnheu.Application.UseCases.Itens.ChecarValidadeItem;
using DriveAnheu.Application.UseCases.Itens.CriarItem;
using DriveAnheu.Application.UseCases.Itens.DeletarItem;
using DriveAnheu.Application.UseCases.Itens.EditarItem;
using DriveAnheu.Application.UseCases.Itens.ListarFolderRotas;
using DriveAnheu.Application.UseCases.Itens.ListarItem;
using DriveAnheu.Application.UseCases.Itens.ObterItem;
using DriveAnheu.Application.UseCases.Itens.ObterTamanhoEmDiscoEmBytes;
using DriveAnheu.Application.UseCases.Itens.UploadItem;
using Microsoft.Extensions.DependencyInjection;

namespace DriveAnheu.Application.UseCases.Itens
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddItensApplication(this IServiceCollection services)
        {
            services.AddScoped<IObterItemQuery, ObterItemQuery>();
            services.AddScoped<IListarItemQuery, ListarItemQuery>();
            services.AddScoped<ICriarItemCommand, CriarItemCommand>();
            services.AddScoped<IChecarValidadeItemCommand, ChecarValidadeItemCommand>();
            services.AddScoped<IListarFolderRotasQuery, ListarFolderRotasQuery>();
            services.AddScoped<IDeletarItemCommand, DeletarItemCommand>();
            services.AddScoped<IEditarItemCommand, EditarItemCommand>();
            services.AddScoped<IUploadItemCommand, UploadItemCommand>();
            services.AddScoped<IObterTamanhoEmDiscoEmBytesQuery, ObterTamanhoEmDiscoEmBytesQuery>();

            return services;
        }
    }
}