using AutoMapper;
using DriveAnheu.Application.UseCases.Usuarios.AutenticarUsuario;
using DriveAnheu.Application.UseCases.Usuarios.CriarUsuario;
using DriveAnheu.Application.UseCases.Usuarios.ObterUsuario;
using DriveAnheu.Domain.Entities;
using DriveAnheu.Infrastructure.Auth.Token;
using DriveAnheu.Infrastructure.Data;
using DriveAnheu.Tests.Fixtures;
using DriveAnheu.Tests.Fixtures.Mocks;
using Moq;
using Xunit;

namespace DriveAnheu.Tests.Tests.Usuarios
{
    public sealed class UsuarioUseCaseTest
    {
        private readonly DriveAnheuContext _context;
        private readonly IMapper _mapper;

        public UsuarioUseCaseTest()
        {
            _context = Fixture.CriarContext();
            _mapper = Fixture.CriarMapper();
        }

        [Fact]
        public async Task Autenticar_TestarSucesso()
        {
            // Arrange
            var criarUsuarioCommandMock = new Mock<ICriarUsuarioCommand>();
            var jwtTokenGeneratorMock = new Mock<IJwtTokenGenerator>();

            var input = UsuarioMock.CriarUsuarioOutput();
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

        [Fact]
        public async Task Autenticar_TestarFalha()
        {
            // Arrange
            var criarUsuarioCommandMock = new Mock<ICriarUsuarioCommand>();
            var jwtTokenGeneratorMock = new Mock<IJwtTokenGenerator>();

            criarUsuarioCommandMock.Setup(x => x.Execute(It.IsAny<Usuario>())).ThrowsAsync(new Exception("Falha simulada"));

            var service = new AutenticarUsuarioCommand(jwtTokenGeneratorMock.Object, criarUsuarioCommandMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await service.Execute());
        }

        [Fact]
        public async Task Obter_TestarSucesso()
        {
            // Arrange
            var service = new ObterUsuarioQuery(_context, _mapper);

            var input = UsuarioMock.CriarUsuario();
            await _context.Usuarios.AddAsync(input);
            await _context.SaveChangesAsync();

            // Act
            var result = await service.Execute(input.UsuarioId, input.Guid);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(input.UsuarioId, result.UsuarioId);
            Assert.Equal(input.Guid, result.Guid);
        }

        [Fact]
        public async Task Criar_TestarSucesso()
        {
            var service = new CriarUsuarioCommand(_context, _mapper);

            // Act
            var input = UsuarioMock.CriarUsuario();
            var result = await service.Execute(input);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(input.UsuarioId, result.UsuarioId);
            Assert.Equal(input.Guid, result.Guid);

            var added = await _context.Usuarios.FindAsync(input.UsuarioId);
            Assert.NotNull(added);
        }
    }
}