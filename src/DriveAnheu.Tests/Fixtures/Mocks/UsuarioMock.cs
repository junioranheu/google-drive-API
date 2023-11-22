using DriveAnheu.Application.UseCases.Usuarios.Shared.Output;
using DriveAnheu.Domain.Entities;
using static junioranheu_utils_package.Fixtures.Get;

namespace DriveAnheu.Tests.Fixtures.Mocks
{
    public static class UsuarioMock
    {
        public static Usuario CriarUsuario()
        {
            Usuario usuario = new()
            {
                UsuarioId = GerarNumeroAleatorio(1, 1000),
                Guid = Guid.NewGuid(),
                Data = GerarHorarioBrasilia()
            };

            return usuario;
        }

        public static UsuarioOutput CriarUsuarioOutput()
        {
            UsuarioOutput usuario = new()
            {
                UsuarioId = GerarNumeroAleatorio(1, 1000),
                Guid = Guid.NewGuid(),
                Data = GerarHorarioBrasilia()
            };

            return usuario;
        }
    }
}