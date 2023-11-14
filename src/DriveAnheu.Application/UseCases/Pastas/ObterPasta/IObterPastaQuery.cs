using DriveAnheu.Application.UseCases.Pastas.Shared.Output;

namespace DriveAnheu.Application.UseCases.Pastas.ObterPasta
{
    public interface IObterPastaQuery
    {
        Task<PastaOutput?> Execute(Guid guid);
    }
}