using DriveAnheu.Application.UseCases.Itens.Shared.Output;

namespace DriveAnheu.Application.UseCases.Itens.ObterItem
{
    public interface IObterItemQuery
    {
        Task<ItemOutput?> Execute(Guid guidPastaPai);
    }
}