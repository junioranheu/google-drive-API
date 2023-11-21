using DriveAnheu.Domain.Consts;
using DriveAnheu.Domain.Entities;
using DriveAnheu.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using static junioranheu_utils_package.Fixtures.Get;

namespace DriveAnheu.Application.UseCases.Itens.EditarItem
{
    public sealed class EditarItemCommand(DriveAnheuContext _context) : IEditarItemCommand
    {
        public async Task Execute(Guid guid, string nome, int usuarioId)
        {
            Item? linq = await _context.Itens.Where(i => i.Guid == guid).AsNoTracking().FirstOrDefaultAsync();

            if (linq is null && guid != Guid.Empty)
            {
                throw new Exception(ObterDescricaoEnum(CodigoErroEnum.NaoEncontrado));
            }

            if (linq?.UsuarioId != usuarioId)
            {
                throw new Exception(ObterDescricaoEnum(CodigoErroEnum.NaoAutorizado_Item));
            }

            linq.Nome = nome;
            linq.DataMod = GerarHorarioBrasilia();

            _context.Itens.Update(linq);
            await _context.SaveChangesAsync();
        }
    }
}