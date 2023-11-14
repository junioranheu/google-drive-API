using DriveAnheu.Application.UseCases.Usuarios.Shared.Output;

namespace DriveAnheu.Application.UseCases.Usuarios.AutenticarUsuario
{
    public interface IAutenticarUsuarioCommand
    {
        Task<UsuarioOutput> Execute();
    }
}