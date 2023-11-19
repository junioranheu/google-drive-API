using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static junioranheu_utils_package.Fixtures.Get;

namespace DriveAnheu.Domain.Entities
{
    [Index(nameof(Data))]
    public sealed class HistoricoExpiracao
    {
        [Key]
        public int HistoricoExpiracaoId { get; set; }

        public DateTime Data { get; set; } = GerarHorarioBrasilia();
    }
}