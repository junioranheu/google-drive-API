using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static junioranheu_utils_package.Fixtures.Get;

namespace DriveAnheu.Domain.Entities
{
    [Index(nameof(Id))]
    public sealed class Pasta
    {
        [Key]
        public int PastaId { get; set; }

        public Guid Id { get; set; } = Guid.Empty;

        public string Nome { get; set; } = string.Empty;

        public int? UsuarioId { get; set; }
        public Usuario? Usuarios { get; init; }

        public DateTime Data { get; set; } = GerarHorarioBrasilia();
    }
}