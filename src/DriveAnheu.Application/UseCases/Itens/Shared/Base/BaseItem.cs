using DriveAnheu.Domain.Consts;
using Microsoft.AspNetCore.Hosting;
using static junioranheu_utils_package.Fixtures.Convert;

namespace DriveAnheu.Application.UseCases.Itens.Shared.Base
{
    public class BaseItem
    {
        public BaseItem() { }

        internal static string ObterArquivoBase64_Pipe_Extensao(IWebHostEnvironment webHostEnvironment, string nomeArquivo)
        {
            string path = Path.Combine(webHostEnvironment.ContentRootPath, SistemaConst.PathUploadItem);
            string[] matchingFiles = Directory.GetFiles(path, $"{nomeArquivo}.*");

            if (matchingFiles.Length < 1)
            {
                return string.Empty;
            }

            string? arquivo = matchingFiles.FirstOrDefault();

            if (arquivo is null)
            {
                return string.Empty;
            }

            string extensao = Path.GetExtension(arquivo);
            byte[] bytes = File.ReadAllBytes(arquivo);
            string base64 = ConverterBytesParaBase64(bytes);

            return $"{base64}|{extensao}";
        }
    }
}