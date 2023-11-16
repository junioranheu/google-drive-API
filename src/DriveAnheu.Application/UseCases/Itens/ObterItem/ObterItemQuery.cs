using AutoMapper;
using DriveAnheu.Application.UseCases.Itens.Shared.Output;
using DriveAnheu.Domain.Entities;
using DriveAnheu.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DriveAnheu.Application.UseCases.Itens.ObterItem
{
    public sealed class ObterItemQuery(DriveAnheuContext _context, IMapper _mapper) : IObterItemQuery
    {
        public async Task<ItemOutput?> Execute(Guid guid)
        {
            Item? linq = await _context.Itens.Where(p => p.Guid == guid).AsNoTracking().FirstOrDefaultAsync();

            return _mapper.Map<ItemOutput>(linq);
        }
    }
}