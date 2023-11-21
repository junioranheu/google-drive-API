
namespace DriveAnheu.Application.UseCases.Itens.EditarItem
{
    public interface IEditarItemCommand
    {
        Task Execute(Guid guid, string nome, int usuarioId);
    }
}