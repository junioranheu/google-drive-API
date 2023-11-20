using DriveAnheu.Domain.Entities;
using DriveAnheu.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DriveAnheu.Application.UseCases.HistoricosExpiracoes.ObterDataUltimoHistoricoExpiracao
{
    public sealed class ObterDataUltimoHistoricoExpiracaoQuery(DriveAnheuContext _context) : IObterDataUltimoHistoricoExpiracaoQuery
    {
        public async Task<DateTime?> Execute()
        {
            HistoricoExpiracao? linq = await _context.HistoricosExpiracoes.OrderByDescending(h => h.Data).AsNoTracking().FirstOrDefaultAsync();

            if (linq is null)
            {
                return null;
            }

            return linq.Data;
        }
    }
}