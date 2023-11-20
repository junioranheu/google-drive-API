using DriveAnheu.Domain.Enums;

namespace DriveAnheu.Application.UseCases.Itens.Shared.Input
{
    public sealed class ItemInput
    {
        public string Nome { get; set; } = string.Empty;

        public ItemTipoEnum Tipo { get; set; }

        public Guid GuidPastaPai { get; set; } = Guid.Empty;
    }
}