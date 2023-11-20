using AutoMapper;
using DriveAnheu.Application.UseCases.Itens.Shared.Base;
using DriveAnheu.Application.UseCases.Itens.Shared.Output;
using DriveAnheu.Domain.Entities;
using DriveAnheu.Domain.Enums;
using DriveAnheu.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DriveAnheu.Application.UseCases.Itens.ListarItem
{
    public sealed class ListarItemQuery(
        DriveAnheuContext _context,
        IWebHostEnvironment _webHostEnvironment,
        ILogger<ListarItemQuery> _logger,
        IMapper _mapper) : BaseItem, IListarItemQuery
    {
        public async Task<List<ItemOutput>?> Execute(Guid? guidPastaPai)
        {
            List<Item>? linq = await _context.Itens.
                               Where(i => i.GuidPastaPai == guidPastaPai).
                               AsNoTracking().ToListAsync();

            if (linq.Count == 0)
            {
                return [];
            }

            List<ItemOutput> output = _mapper.Map<List<ItemOutput>>(linq);

            foreach (var item in output)
            {
                if (item.Tipo == ItemTipoEnum.Pasta)
                {
                    continue;
                }

                try
                {
                    item.ArquivoBase64 = ObterArquivoBase64(_webHostEnvironment, item.Guid.ToString());
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: ex.Message);
                }
            }

            return output;
        }
    }
}