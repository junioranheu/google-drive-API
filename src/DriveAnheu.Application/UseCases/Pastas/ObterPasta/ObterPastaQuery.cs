using AutoMapper;
using DriveAnheu.Application.UseCases.Pastas.Shared.Output;
using DriveAnheu.Domain.Entities;
using DriveAnheu.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DriveAnheu.Application.UseCases.Pastas.ObterPasta
{
    public sealed class ObterPastaQuery(DriveAnheuContext _context, IMapper _mapper) : IObterPastaQuery
    {
        public async Task<PastaOutput?> Execute(Guid guid)
        {
            Pasta? linq = await _context.Pastas.Where(p => p.Guid == guid).AsNoTracking().FirstOrDefaultAsync();

            return _mapper.Map<PastaOutput>(linq);
        }
    }
}