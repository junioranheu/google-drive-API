using DriveAnheu.Application.UseCases.Usuarios.Shared.Output;

namespace DriveAnheu.Application.UseCases.Usuarios.ObterUsuarioCache
{
    public interface IObterUsuarioCacheQuery
    {
        Task<UsuarioOutput?> Execute(Guid guid);
    }
}