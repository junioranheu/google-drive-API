using AutoMapper;
using DriveAnheu.Application.UseCases.Pastas.Shared.Input;
using DriveAnheu.Domain.Entities;
using DriveAnheu.Infrastructure.Data;

namespace DriveAnheu.Application.UseCases.Pastas.CriarPasta
{
    public sealed class CriarPastaCommand(DriveAnheuContext _context, IMapper _mapper) : ICriarPastaCommand
    {
        public async Task Execute(PastaInput input)
        {
            var x = _mapper.Map<Pasta>(input);

            await _context.AddAsync(x);
            await _context.SaveChangesAsync();
        }
    }
}