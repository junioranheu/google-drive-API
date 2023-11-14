using DriveAnheu.Application.UseCases.Usuarios.Shared.Output;
using DriveAnheu.Domain.Entities;

namespace DriveAnheu.Application.UseCases.Usuarios.CriarUsuario
{
    public interface ICriarUsuarioCommand
    {
        Task<UsuarioOutput> Execute(Usuario input);
    }
}