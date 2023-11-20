using DriveAnheu.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static junioranheu_utils_package.Fixtures.Get;

namespace DriveAnheu.Domain.Entities
{
    [Index(nameof(Guid))]
    [Index(nameof(GuidPastaPai))]
    public sealed class Item
    {
        [Key]
        public int ItemId { get; set; }

        [Required]
        public Guid Guid { get; set; } = Guid.Empty;

        public string Nome { get; set; } = string.Empty;

        public ItemTipoEnum Tipo { get; set; }

        public Guid GuidPastaPai { get; set; } = Guid.Empty;

        public int? UsuarioId { get; set; }
        public Usuario? Usuarios { get; init; }

        public DateTime Data { get; set; } = GerarHorarioBrasilia();
    }
}