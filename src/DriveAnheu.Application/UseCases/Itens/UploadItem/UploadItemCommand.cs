using DriveAnheu.Application.UseCases.Itens.CriarItem;
using DriveAnheu.Application.UseCases.Itens.ObterItem;
using DriveAnheu.Application.UseCases.Itens.Shared.Input;
using DriveAnheu.Application.UseCases.Itens.Shared.Output;
using DriveAnheu.Domain.Consts;
using DriveAnheu.Domain.Enums;
using Microsoft.AspNetCore.Hosting;
using System;
using static junioranheu_utils_package.Fixtures.Get;
using static junioranheu_utils_package.Fixtures.Post;

namespace DriveAnheu.Application.UseCases.Itens.UploadItem
{
    public sealed class UploadItemCommand(
        IWebHostEnvironment _webHostEnvironment,
        ICriarItemCommand _criarItemCommand,
        IObterItemQuery _obterItemQuery
        ) : IUploadItemCommand
    {
        public async Task<ItemOutput?> Execute(ItemUploadInput input, int usuarioId)
        {
            string extensao = ObterExtensao(input.Arquivo);
            Guid guid = await CriarItem(input, usuarioId, extensao);

            if (guid.ToString() is null || guid == Guid.Empty)
            {
                throw new Exception(ObterDescricaoEnum(CodigoErroEnum.BadRequest));
            }

            await UploadItem(input, guid, extensao);

            ItemOutput? output = await _obterItemQuery.Execute(guid);

            return output;
        }

        private async Task<Guid> CriarItem(ItemUploadInput input, int usuarioId, string extensao)
        {
            string nome = $"{GerarStringAleatoria(10, false)}{(!string.IsNullOrEmpty(extensao) ? extensao : string.Empty)}";

            ItemInput item = new()
            {
                Nome = nome,
                Tipo = ConverterExtensaoParaItemTipoEnum(extensao),
                GuidPastaPai = input.GuidPastaPai
            };

            Guid guid = await _criarItemCommand.Execute(item, usuarioId);

            return guid;
        }
        private async Task UploadItem(ItemUploadInput input, Guid guid, string extensao)
        {
            await SubirArquivoEmPasta(arquivo: input.Arquivo, nomeArquivoSemExtensao: guid.ToString(), extensao: extensao, path: SistemaConst.PathUploadItem, nomeArquivoAnteriorSemExtensao: string.Empty, webRootPath: _webHostEnvironment.ContentRootPath);
        }

        private static ItemTipoEnum ConverterExtensaoParaItemTipoEnum(string extensao)
        {
            if (string.IsNullOrWhiteSpace(extensao))
            {
                throw new ArgumentException("O parâmetro 'extensao' não pode ser nulo", nameof(extensao));
            }

            string extensionNormalizada = extensao.Replace(".", string.Empty).ToLowerInvariant();

            switch (extensionNormalizada)
            {
                // Imagens;
                case "jpg":
                case "jpeg":
                case "png":
                case "webp":
                    return ItemTipoEnum.Imagem;

                // Texto básico;
                case "txt":
                    return ItemTipoEnum.Txt;

                // Textos;
                case "doc":
                case "docx":
                    return ItemTipoEnum.Doc;

                // PDF;
                case "pdf":
                    return ItemTipoEnum.Pdf;

                // Sheets;
                case "xls":
                case "xlsx":
                case "csv":
                    return ItemTipoEnum.Planilha;

                //// Áudios;
                //case "mp3":
                //case "wav":
                //case "ogg":
                //case "flac":
                //case "aac":
                //    return ItemTipoEnum.Audio;

                //// Vídeos;
                //case "mp4":
                //case "mkv":
                //case "avi":
                //case "mov":
                //case "wmv":
                //    return ItemTipoEnum.Video;

                //// Arquivos de compressão;
                //case "zip":
                //case "rar":
                //case "tar":
                //case "7z":
                //    return ItemTipoEnum.ArquivoCompactado;

                //// Executáveis;
                //case "exe":
                //case "msi":
                //    return ItemTipoEnum.Executavel;

                //// Arquivos de código;
                //case "c":
                //case "cpp":
                //case "h":
                //case "java":
                //case "cs":
                //    return ItemTipoEnum.CodigoFonte;

                default:
                    return ItemTipoEnum.Pasta;
            }
        }
    }
}