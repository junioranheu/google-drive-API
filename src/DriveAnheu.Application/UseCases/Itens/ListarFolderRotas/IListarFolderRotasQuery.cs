
using DriveAnheu.Application.UseCases.Itens.Shared.Output;

namespace DriveAnheu.Application.UseCases.Itens.ListarFolderRotas
{
    public interface IListarFolderRotasQuery
    {
        Task<List<FolderRotaOutput>> Execute(Guid guid);
    }
}