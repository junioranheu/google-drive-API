using DriveAnheu.Domain.Consts;
using DriveAnheu.Domain.Entities;
using DriveAnheu.Domain.Enums;
using DriveAnheu.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using static junioranheu_utils_package.Fixtures.Get;
using static junioranheu_utils_package.Fixtures.Post;

namespace DriveAnheu.Application.UseCases.Itens.DeletarItem
{
    public sealed class DeletarItemCommand(DriveAnheuContext _context, IWebHostEnvironment _webHostEnvironment) : IDeletarItemCommand
    {
        public async Task Execute(Guid guid, int usuarioId)
        {
            Item? linq = await _context.Itens.Where(i => i.Guid == guid).AsNoTracking().FirstOrDefaultAsync();

            if (linq is null)
            {
                throw new Exception(ObterDescricaoEnum(CodigoErroEnum.NaoEncontrado));
            }

            if (linq?.UsuarioId != usuarioId)
            {
                throw new Exception(ObterDescricaoEnum(CodigoErroEnum.NaoAutorizado_Item));
            }

            if (linq.Tipo != ItemTipoEnum.Pasta)
            {
                DeletarArquivosEmPasta(path: SistemaConst.PathUploadItem, webRootPath: _webHostEnvironment.ContentRootPath, listaNomes: [linq.Guid.ToString()]);
            }

            _context.Itens.Remove(linq);
            await _context.SaveChangesAsync();
        }
    }
}