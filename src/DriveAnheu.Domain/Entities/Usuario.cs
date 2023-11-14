using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static junioranheu_utils_package.Fixtures.Get;

namespace DriveAnheu.Domain.Entities
{
    [Index(nameof(Guid))]
    public sealed class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }

        public Guid Guid { get; set; } = Guid.Empty;

        public DateTime Data { get; set; } = GerarHorarioBrasilia();
    }
}