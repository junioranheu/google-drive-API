
namespace DriveAnheu.Application.UseCases.Itens.ChecarValidadeItem
{
    public interface IChecarValidadeItemCommand
    {
        Task Execute(bool isForcar = false);
    }
}