using AutoMapper;
using DriveAnheu.Application.UseCases.Itens.CriarItem;
using DriveAnheu.Application.UseCases.Itens.ListarItem;
using DriveAnheu.Application.UseCases.Itens.ObterItem;
using DriveAnheu.Application.UseCases.Itens.Shared.Input;
using DriveAnheu.Application.UseCases.Itens.Shared.Output;
using DriveAnheu.Application.UseCases.Itens.UploadItem;
using DriveAnheu.Domain.Entities;
using DriveAnheu.Infrastructure.Data;
using DriveAnheu.Tests.Fixtures;
using DriveAnheu.Tests.Fixtures.Mocks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
            var loggerMock = new Mock<ILogger<ObterItemQuery>>();
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
        public async Task Listar_TestarSucesso()
        {
            // Arrange;
            var webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
            var loggerMock = new Mock<ILogger<ListarItemQuery>>();
            var service = new ListarItemQuery(_context, webHostEnvironmentMock.Object, loggerMock.Object, _mapper);

            List<Item> inputs = [ItemMock.CriarItem(), ItemMock.CriarItem()];
            await _context.Itens.AddRangeAsync(inputs);
            await _context.SaveChangesAsync();

            // Act;
            var result = await service.Execute(inputs.FirstOrDefault()!.GuidPastaPai);

            // Assert;
            Assert.True(result?.Count > 0);
            Assert.Equal(inputs.FirstOrDefault()?.UsuarioId, result.FirstOrDefault()?.UsuarioId);
            Assert.Equal(inputs.FirstOrDefault()?.Guid, result.FirstOrDefault()?.Guid);
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

        [Fact]
        public async Task Upload_TestarSucesso()
        {
            // Arrange;
            var webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
            var criarItemCommandMock = new Mock<ICriarItemCommand>();
            var obterItemQueryMock = new Mock<IObterItemQuery>();

            var uploadItemCommand = new UploadItemCommand(webHostEnvironmentMock.Object, criarItemCommandMock.Object, obterItemQueryMock.Object);

            var mockFormFile = new Mock<IFormFile>();
            mockFormFile.Setup(f => f.FileName).Returns("test.txt");
            mockFormFile.Setup(f => f.Length).Returns(1024);
            mockFormFile.Setup(f => f.ContentType).Returns("text/plain");

            var input = new ItemUploadInput
            {
                GuidPastaPai = Guid.NewGuid(),
                Arquivo = mockFormFile.Object
            };

            int usuarioId = GerarNumeroAleatorio(1, 1000);
            criarItemCommandMock.Setup(c => c.Execute(It.IsAny<ItemInput>(), It.IsAny<int>())).ReturnsAsync(Guid.NewGuid());
            obterItemQueryMock.Setup(o => o.Execute(It.IsAny<Guid>())).ReturnsAsync(new ItemOutput());

            // Act;
            var result = await uploadItemCommand.Execute(input, usuarioId, isTesteUnitario: true);

            // Assert
            Assert.NotNull(result);
        }
    }
}