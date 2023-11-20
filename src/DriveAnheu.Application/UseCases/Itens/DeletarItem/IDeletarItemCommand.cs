
namespace DriveAnheu.Application.UseCases.Itens.DeletarItem
{
    public interface IDeletarItemCommand
    {
        Task Execute(Guid guid, int usuarioId);
    }
}