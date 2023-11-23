using DriveAnheu.Application.UseCases.Itens.Shared.Output;

namespace DriveAnheu.Application.UseCases.Itens.ListarItem
{
    public interface IListarItemQuery
    {
        Task<List<ItemOutput>?> Execute(Guid guid);
    }
}