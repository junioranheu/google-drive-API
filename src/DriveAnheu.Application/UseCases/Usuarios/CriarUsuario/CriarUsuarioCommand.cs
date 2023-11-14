using AutoMapper;
using DriveAnheu.Application.UseCases.Usuarios.Shared.Output;
using DriveAnheu.Domain.Entities;
using DriveAnheu.Infrastructure.Data;

namespace DriveAnheu.Application.UseCases.Usuarios.CriarUsuario
{
    public sealed class CriarUsuarioCommand(DriveAnheuContext _context, IMapper _mapper) : ICriarUsuarioCommand
    {
        public async Task<UsuarioOutput> Execute(Usuario input)
        {
            await _context.AddAsync(input);
            await _context.SaveChangesAsync();

            return _mapper.Map<UsuarioOutput>(input);
        }
    }
}