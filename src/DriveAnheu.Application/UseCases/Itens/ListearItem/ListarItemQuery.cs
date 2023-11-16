using AutoMapper;
using DriveAnheu.Application.UseCases.Itens.Shared.Output;
using DriveAnheu.Domain.Entities;
using DriveAnheu.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DriveAnheu.Application.UseCases.Itens.ListarItem
{
    public sealed class ListarItemQuery(DriveAnheuContext _context, IMapper _mapper) : IListarItemQuery
    {
        public async Task<List<ItemOutput>?> Execute(Guid? guidPastaPai)
        {
            List<Item>? linq = await _context.Itens.
                               Where(p => (guidPastaPai != Guid.Empty ? p.GuidPastaPai == guidPastaPai : true)).
                               AsNoTracking().ToListAsync();

            return _mapper.Map<List<ItemOutput>>(linq);
        }
    }
}