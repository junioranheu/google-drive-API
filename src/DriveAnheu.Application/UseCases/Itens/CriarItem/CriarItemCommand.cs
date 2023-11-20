using AutoMapper;
using DriveAnheu.Application.UseCases.Itens.Shared.Input;
using DriveAnheu.Domain.Entities;
using DriveAnheu.Infrastructure.Data;

namespace DriveAnheu.Application.UseCases.Itens.CriarItem
{
    public sealed class CriarItemCommand(DriveAnheuContext _context, IMapper _mapper) : ICriarItemCommand
    {
        public async Task<Guid> Execute(ItemInput input, int usuarioId)
        {
            var x = _mapper.Map<Item>(input);

            x.Guid = Guid.NewGuid();
            x.UsuarioId = usuarioId;

            var entityEntry = await _context.AddAsync(x);
            await _context.SaveChangesAsync();

            return entityEntry.Entity.Guid;
        }
    }
}