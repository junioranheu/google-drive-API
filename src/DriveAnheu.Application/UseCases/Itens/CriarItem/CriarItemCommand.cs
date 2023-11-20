using AutoMapper;
using DriveAnheu.Application.UseCases.Itens.Shared.Input;
using DriveAnheu.Domain.Entities;
using DriveAnheu.Infrastructure.Data;

namespace DriveAnheu.Application.UseCases.Itens.CriarItem
{
    public sealed class CriarItemCommand(DriveAnheuContext _context, IMapper _mapper) : ICriarItemCommand
    {
        public async Task Execute(ItemInput input)
        {
            if (input.GuidPastaPai is null)
            {
                input.GuidPastaPai = Guid.Empty;
            }

            var x = _mapper.Map<Item>(input);

            await _context.AddAsync(x);
            await _context.SaveChangesAsync();
        }
    }
}