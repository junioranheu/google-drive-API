using AutoMapper;
using DriveAnheu.Application.UseCases.Itens.CriarItem;
using DriveAnheu.Application.UseCases.Itens.ListarItem;
using DriveAnheu.Application.UseCases.Itens.ObterItem;
using DriveAnheu.Infrastructure.Data;
using DriveAnheu.Tests.Fixtures;
using DriveAnheu.Tests.Fixtures.Mocks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using static junioranheu_utils_package.Fixtures.Get;

namespace DriveAnheu.Tests.Tests.Itens
{
    public sealed class ItemUseCaseTest
    {
        private readonly DriveAnheuContext _context;
        private readonly IMapper _mapper;

        public ItemUseCaseTest()
        {
            _context = Fixture.CriarContext();
            _mapper = Fixture.CriarMapper();
        }

        [Fact]
        public async Task Obter_TestarSucesso()
        {
            // Arrange;
            var webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
            var loggerMock = new Mock<ILogger<ListarItemQuery>>();
            var service = new ObterItemQuery(_context, webHostEnvironmentMock.Object, loggerMock.Object, _mapper);

            var input = ItemMock.CriarItem();
            await _context.Itens.AddAsync(input);
            await _context.SaveChangesAsync();

            // Act;
            var result = await service.Execute(input.Guid);

            // Assert;
            Assert.NotNull(result);
            Assert.Equal(input.UsuarioId, result.UsuarioId);
            Assert.Equal(input.Guid, result.Guid);
        }

        [Fact]
        public async Task Criar_TestarSucesso()
        {
            // Arrange;
            var service = new CriarItemCommand(_context, _mapper);

            // Act
            var input = ItemMock.CriarItemInput();
            int usuarioId = GerarNumeroAleatorio(1, 1000);
            Guid result = await service.Execute(input, usuarioId);

            // Assert
            Assert.NotEqual(Guid.Empty, result);

            var added = await _context.Itens.Where(x => x.Guid == result).AsNoTracking().FirstOrDefaultAsync();
            Assert.NotNull(added);
        }
    }
}