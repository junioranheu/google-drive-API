using FluentValidation;

namespace DriveAnheu.Application.UseCases.Itens.Shared.Input
{
    public sealed class ItemInputValidator : AbstractValidator<ItemInput>
    {
        public ItemInputValidator()
        {
            RuleFor(x => x.Nome).NotNull().NotEmpty().MinimumLength(1).WithMessage("Nome do item é inválido");
            RuleFor(x => x.Tipo).IsInEnum().WithMessage("Tipo do item é inválido");
        }
    }
}