using DriveAnheu.Application.UseCases.Usuarios.CriarUsuario;
using DriveAnheu.Application.UseCases.Usuarios.Shared.Output;
using DriveAnheu.Domain.Entities;
using DriveAnheu.Infrastructure.Auth.Token;

namespace DriveAnheu.Application.UseCases.Usuarios.AutenticarUsuario
{
    public sealed class AutenticarUsuarioCommand(IJwtTokenGenerator _jwtTokenGenerator, ICriarUsuarioCommand _criarUsuarioCommand) : IAutenticarUsuarioCommand
    {
        public async Task<UsuarioOutput> Execute()
        {
            Usuario input = new()
            {
                Guid = Guid.NewGuid()
            };

            UsuarioOutput output = await _criarUsuarioCommand.Execute(input);
            output.Token = _jwtTokenGenerator.GerarToken(output.UsuarioId, output.Guid);

            return output;
        }
    }
}