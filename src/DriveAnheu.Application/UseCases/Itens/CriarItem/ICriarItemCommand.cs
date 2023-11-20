using DriveAnheu.Application.UseCases.Itens.Shared.Input;

namespace DriveAnheu.Application.UseCases.Itens.CriarItem
{
    public interface ICriarItemCommand
    {
        Task<Guid> Execute(ItemInput input, int usuarioId);
    }
}