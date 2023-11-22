using DriveAnheu.Application.UseCases.Itens.Shared.Input;
using DriveAnheu.Domain.Entities;
using DriveAnheu.Domain.Enums;
using static junioranheu_utils_package.Fixtures.Get;

namespace DriveAnheu.Tests.Fixtures.Mocks
{
    public static class ItemMock
    {
        public static Item CriarItem()
        {
            Item item = new()
            {
                ItemId = GerarNumeroAleatorio(1, 1000),
                Guid = Guid.NewGuid(),
                Nome = GerarStringAleatoria(10, false),
                Tipo = ItemTipoEnum.Pasta,
                GuidPastaPai = Guid.Empty,
                UsuarioId = GerarNumeroAleatorio(1, 1000)
            };

            return item;
        }

        public static ItemInput CriarItemInput()
        {
            ItemInput item = new()
            {
                Nome = GerarStringAleatoria(10, false),
                Tipo = ItemTipoEnum.Pasta,
                GuidPastaPai = Guid.Empty,
            };

            return item;
        }
    }
}