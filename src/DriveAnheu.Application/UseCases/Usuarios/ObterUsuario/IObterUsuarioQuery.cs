using DriveAnheu.Application.UseCases.Usuarios.Shared.Output;

namespace DriveAnheu.Application.UseCases.Usuarios.ObterUsuario
{
    public interface IObterUsuarioQuery
    {
        Task<UsuarioOutput?> Execute(int? id, Guid guid);
    }
}