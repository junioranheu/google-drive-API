using DriveAnheu.Application.UseCases.Pastas.Shared.Output;

namespace DriveAnheu.Application.UseCases.Pastas.ListarPasta
{
    public interface IListarPastaQuery
    {
        Task<List<PastaOutput>?> Execute(Guid? guid);
    }
}