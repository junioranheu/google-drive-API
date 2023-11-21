using DriveAnheu.Domain.Consts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using static junioranheu_utils_package.Fixtures.Convert;

namespace DriveAnheu.Application.UseCases.Itens.Shared.Base
{
    public class BaseItem
    {
        public BaseItem() { }

        private static (byte[] arquivoBytes, string extensao) ObterArquivo_Bytes(IWebHostEnvironment webHostEnvironment, string nomeArquivo)
        {
            string path = Path.Combine(webHostEnvironment.ContentRootPath, SistemaConst.PathUploadItem);
            string[] matchingFiles = Directory.GetFiles(path, $"{nomeArquivo}.*");

            if (matchingFiles.Length < 1)
            {
                return ([], string.Empty);
            }

            string? arquivo = matchingFiles.FirstOrDefault();

            if (arquivo is null)
            {
                return ([], string.Empty);
            }

            string extensao = Path.GetExtension(arquivo);
            byte[] bytes = File.ReadAllBytes(arquivo);

            return (bytes, extensao);
        }

        internal static IFormFile ObterArquivo_IFormFile(IWebHostEnvironment webHostEnvironment, string nomeArquivo)
        {
            (byte[] arquivoBytes, _) = ObterArquivo_Bytes(webHostEnvironment, nomeArquivo);
            IFormFile iFormFile = ConverterBytesParaIFormFile(arquivoBytes);

            return iFormFile;
        }

        internal static string ObterArquivo_Base64_Pipe_Extensao(IWebHostEnvironment webHostEnvironment, string nomeArquivo)
        {
            (byte[] arquivoBytes, string extensao) = ObterArquivo_Bytes(webHostEnvironment, nomeArquivo);
            string base64 = ConverterBytesParaBase64(arquivoBytes);

            return $"{base64}|{extensao}";
        }
    }
}