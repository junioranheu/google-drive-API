using DriveAnheu.Domain.Consts;
using Microsoft.AspNetCore.Hosting;
using static junioranheu_utils_package.Fixtures.Convert;

namespace DriveAnheu.Application.UseCases.Itens.Shared.Base
{
    public class BaseItem
    {
        public BaseItem() { }

        internal static string ObterArquivoBase64(IWebHostEnvironment webHostEnvironment, string nomeArquivo)
        {
            string path = Path.Combine(webHostEnvironment.ContentRootPath, SistemaConst.PathUploadItem);
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