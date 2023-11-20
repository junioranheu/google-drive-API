using DriveAnheu.Domain.Entities;
using DriveAnheu.Domain.Enums;

namespace DriveAnheu.Application.UseCases.Itens.Shared.Output
{
    public sealed class ItemOutput
    {
        public int ItemId { get; set; }

        public Guid Guid { get; set; } = Guid.Empty;

        public string Nome { get; set; } = string.Empty;

        public ItemTipoEnum Tipo { get; set; }

        public int? UsuarioId { get; set; }
        public Usuario? Usuarios { get; init; }

        public DateTime Data { get; set; }

        public Guid? GuidPastaPai { get; set; } = null;

        public string? ArquivoBase64 { get; set; } = string.Empty;
    }
}