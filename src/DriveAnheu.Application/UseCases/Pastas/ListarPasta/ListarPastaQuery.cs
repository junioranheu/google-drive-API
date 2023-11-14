using AutoMapper;
using DriveAnheu.Application.UseCases.Pastas.Shared.Output;
using DriveAnheu.Domain.Entities;
using DriveAnheu.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DriveAnheu.Application.UseCases.Pastas.ListarPasta
{
    public sealed class ListarPastaQuery(DriveAnheuContext _context, IMapper _mapper) : IListarPastaQuery
    {
        public async Task<List<PastaOutput>?> Execute(Guid? guid)
        {
            List<Pasta>? linq = await _context.Pastas.
                                Where(p => (guid != Guid.Empty ? p.Guid == guid : true)).
                                AsNoTracking().ToListAsync();

            return _mapper.Map<List<PastaOutput>>(linq);
        }
    }
}
