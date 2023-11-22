using DriveAnheu.Application.UseCases.Usuarios.AutenticarUsuario;
using DriveAnheu.Application.UseCases.Usuarios.CriarUsuario;
using DriveAnheu.Application.UseCases.Usuarios.Shared.Output;
using DriveAnheu.Domain.Entities;
using DriveAnheu.Infrastructure.Auth.Token;
using Moq;
using Xunit;

namespace DriveAnheu.Tests.Tests.Usuarios
{
    public sealed class UsuarioUseCaseTest
    {
        [Fact]
        public async Task Autenticar_ChecarResultadoEsperado()
        {
            // Arrange
            var criarUsuarioCommandMock = new Mock<ICriarUsuarioCommand>();
            var jwtTokenGeneratorMock = new Mock<IJwtTokenGenerator>();

            UsuarioOutput input = new()
            {
                UsuarioId = 1,
                Guid = Guid.NewGuid()
            };

            criarUsuarioCommandMock.Setup(x => x.Execute(It.IsAny<Usuario>())).ReturnsAsync(input);

            string token = Guid.NewGuid().ToString();
            jwtTokenGeneratorMock.Setup(x => x.GerarToken(It.IsAny<int>(), It.IsAny<Guid>())).Returns(token);

            var service = new AutenticarUsuarioCommand(jwtTokenGeneratorMock.Object, criarUsuarioCommandMock.Object);

            // Act
            var result = await service.Execute();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(input.UsuarioId, result.UsuarioId);
            Assert.Equal(input.Guid, result.Guid);
            Assert.Equal(token, result.Token);
        }
    }
}