using DriveAnheu.Application.UseCases.Itens.Shared.Input;
using DriveAnheu.Application.UseCases.Itens.Shared.Output;

namespace DriveAnheu.Application.UseCases.Itens.UploadItem
{
    public interface IUploadItemCommand
    {
        Task<ItemOutput?> Execute(ItemUploadInput input, int usuarioId, bool? isTesteUnitario = false);
    }
}