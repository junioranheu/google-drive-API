using AutoMapper;
using DriveAnheu.Application.UseCases.Itens.Shared.Base;
using DriveAnheu.Application.UseCases.Itens.Shared.Output;
using DriveAnheu.Domain.Entities;
using DriveAnheu.Domain.Enums;
using DriveAnheu.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DriveAnheu.Application.UseCases.Itens.ObterItem
{
    public sealed class ObterItemQuery(
        DriveAnheuContext _context,
        IWebHostEnvironment _webHostEnvironment,
        ILogger<ObterItemQuery> _logger,
        IMapper _mapper) : BaseItem, IObterItemQuery
    {
        public async Task<ItemOutput?> Execute(Guid guid)
        {
            Item? linq = await _context.Itens.
                         Include(u => u.Usuarios).
                         Where(i => i.Guid == guid).
                         AsNoTracking().FirstOrDefaultAsync();

            if (linq is null)
            {
                return new ItemOutput();
            }

            ItemOutput output = _mapper.Map<ItemOutput>(linq);

            if (output.Tipo == ItemTipoEnum.Pasta)
            {
                return output;
            }

            try
            {
                output.ArquivoBase64_Pipe_Extensao = ObterArquivo_Base64_Pipe_Extensao(_webHostEnvironment, output.Guid.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{mensagemErro}", ex.Message);
            }

            return output;
        }
    }
}