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
            if (guid == Guid.Empty)
            {
                throw new Exception(ObterDescricaoEnum(CodigoErroEnum.BadRequest));
            }

            List<Item>? linq = await _context.Itens.Where(i => i.Guid == guid || i.GuidPastaPai == guid).AsNoTracking().ToListAsync();

            if (linq.Count == 0)
            {
                throw new Exception(ObterDescricaoEnum(CodigoErroEnum.NaoEncontrado));
            }

            Item? itemPrincipal = linq.Where(x => x.Guid == guid).FirstOrDefault();

            if (itemPrincipal is null)
            {
                throw new Exception(ObterDescricaoEnum(CodigoErroEnum.NaoEncontrado));
            }

            if (itemPrincipal.UsuarioId != usuarioId)
            {
                throw new Exception(ObterDescricaoEnum(CodigoErroEnum.NaoAutorizado_Item));
            }

            foreach (var item in linq)
            {
                if (item.Tipo != ItemTipoEnum.Pasta)
                {
                    DeletarArquivosEmPasta(path: SistemaConst.PathUploadItem, webRootPath: _webHostEnvironment.ContentRootPath, listaNomes: [item.Guid.ToString()]);
                }
            }

            _context.Itens.RemoveRange(linq);
            await _context.SaveChangesAsync();
        }
    }
}