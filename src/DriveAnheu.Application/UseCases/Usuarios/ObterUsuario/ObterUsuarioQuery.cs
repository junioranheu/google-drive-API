using AutoMapper;
using DriveAnheu.Application.UseCases.Usuarios.Shared.Output;
using DriveAnheu.Domain.Entities;
using DriveAnheu.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DriveAnheu.Application.UseCases.Usuarios.ObterUsuario
{
    public sealed class ObterUsuarioQuery(DriveAnheuContext _context, IMapper _map) : IObterUsuarioQuery
    {
        public async Task<UsuarioOutput?> Execute(int? id, Guid guid)
        {
            Usuario? linq = await _context.Usuarios.
                            Where(u =>
                                (id > 0 ? u.UsuarioId == id : true) &&
                                (guid != Guid.Empty ? u.Guid == guid : true)
                            ).AsNoTracking().FirstOrDefaultAsync();

            return _map.Map<UsuarioOutput>(linq);
        }
    }
}