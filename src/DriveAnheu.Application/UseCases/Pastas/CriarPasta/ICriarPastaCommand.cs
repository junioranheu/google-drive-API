using DriveAnheu.Application.UseCases.Pastas.Shared.Input;

namespace DriveAnheu.Application.UseCases.Pastas.CriarPasta
{
    public interface ICriarPastaCommand
    {
        Task Execute(PastaInput input);
    }
}