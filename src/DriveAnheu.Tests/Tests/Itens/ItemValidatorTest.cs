using DriveAnheu.Application.UseCases.Itens.Shared.Input;
using DriveAnheu.Domain.Enums;
using FluentValidation.TestHelper;
using Xunit;
using static junioranheu_utils_package.Fixtures.Get;

namespace DriveAnheu.Tests.Tests.Itens
{
    public sealed class ItemValidatorTest
    {
        private readonly ItemInputValidator _validator;

        public ItemValidatorTest()
        {
            _validator = new ItemInputValidator();
        }

        [Theory]
        [InlineData("", false)]
        [InlineData("A", true)]
        [InlineData("AB", true)]
        [InlineData("Teste", true)]
        public void Validar_Nome(string nome, bool esperado)
        {
            // Arrange;
            var model = new ItemInput { Nome = nome, Tipo = ItemTipoEnum.Pasta };

            // Act;
            var result = _validator.TestValidate(model);

            // Assert;
            Assert.Equal(esperado, result.IsValid);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData(ItemTipoEnum.Doc, true)]
        public void Validar_Tipo(ItemTipoEnum? tipoEnum, bool esperado)
        {
            // Arrange;
            var model = new ItemInput { Nome = GerarStringAleatoria(5, false), Tipo = tipoEnum.GetValueOrDefault() };

            // Act;
            var result = _validator.TestValidate(model);

            // Assert;
            Assert.Equal(esperado, result.IsValid);
        }
    }
}