using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static junioranheu_utils_package.Fixtures.Get;

namespace DriveAnheu.Domain.Entities
{
    [Index(nameof(Id))]
    public sealed class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }

        public Guid Id { get; set; } = Guid.Empty;

        public DateTime Data { get; set; } = GerarHorarioBrasilia();
    }
}