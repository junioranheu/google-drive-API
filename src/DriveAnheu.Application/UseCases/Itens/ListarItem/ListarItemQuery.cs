using AutoMapper;
using DriveAnheu.Application.UseCases.Itens.Shared.Output;
using DriveAnheu.Domain.Consts;
using DriveAnheu.Domain.Entities;
using DriveAnheu.Domain.Enums;
using DriveAnheu.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static junioranheu_utils_package.Fixtures.Convert;

namespace DriveAnheu.Application.UseCases.Itens.ListarItem
{
    public sealed class ListarItemQuery(
        DriveAnheuContext _context,
        IWebHostEnvironment _webHostEnvironment,
        ILogger<ListarItemQuery> _logger,
        IMapper _mapper) : IListarItemQuery
    {
        public async Task<List<ItemOutput>?> Execute(Guid? guidPastaPai)
        {
            List<Item>? linq = await _context.Itens.
                               Where(i => i.GuidPastaPai == guidPastaPai).
                               AsNoTracking().ToListAsync();

            List<ItemOutput> output = _mapper.Map<List<ItemOutput>>(linq);

            foreach (var item in output)
            {
                if (item.Tipo == ItemTipoEnum.Pasta)
                {
                    continue;
                }

                try
                {
                    item.ArquivoBase64 = ObterArquivoBase64(item.Guid.ToString());
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: ex.Message);
                }
            }

            return output;
        }

        private string ObterArquivoBase64(string nomeArquivo)
        {
            string path = Path.Combine(_webHostEnvironment.ContentRootPath, SistemaConst.PathUploadItem);
            string[] matchingFiles = Directory.GetFiles(path, $"{nomeArquivo}.*");

            if (matchingFiles.Length < 1)
            {
                return string.Empty;
            }

            string arquivo = matchingFiles[0];
            byte[] bytes = File.ReadAllBytes(arquivo);
            string base64 = ConverterBytesParaBase64(bytes);

            return base64;
        }
    }
}