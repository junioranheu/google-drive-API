using AutoMapper;
using DriveAnheu.Application.UseCases.Usuarios.Shared.Output;
using DriveAnheu.Domain.Entities;
using DriveAnheu.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DriveAnheu.Application.UseCases.Usuarios.ObterUsuario
{
    public sealed class ObterUsuarioQuery : IObterUsuarioQuery
    {
        private readonly DriveAnheuContext _context;
        private readonly IMapper _map;

        public ObterUsuarioQuery(DriveAnheuContext context, IMapper map)
        {
            _context = context;
            _map = map;
        }

        public async Task<UsuarioOutput?> Execute(int? id, Guid guid)
        {
            Usuario? linq = await _context.Usuarios.
                            Where(u =>
                                id > 0 ? u.UsuarioId == id : true &&
                                guid != Guid.Empty ? u.Guid == guid : true
                            ).AsNoTracking().FirstOrDefaultAsync();

            return _map.Map<UsuarioOutput>(linq);
        }
    }
}