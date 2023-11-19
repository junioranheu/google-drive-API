using DriveAnheu.Domain.Consts;
using DriveAnheu.Domain.Entities;
using DriveAnheu.Domain.Enums;
using DriveAnheu.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using static junioranheu_utils_package.Fixtures.Convert;
using static junioranheu_utils_package.Fixtures.Get;
using static junioranheu_utils_package.Fixtures.Post;

namespace DriveAnheu.Application.UseCases.Itens.ChecarValidadeItem
{
    public sealed class ChecarValidadeItemCommand(DriveAnheuContext _context, IWebHostEnvironment _webHostEnvironment) : IChecarValidadeItemCommand
    {
        public async Task Execute(bool isForcar = false)
        {
            List<Item>? listaExpirados = await _context.Itens.Where(i => EF.Functions.DateDiffHour(i.Data, GerarHorarioBrasilia()) > SistemaConst.OffsetChecarValidadeItemEmHoras).ToListAsync();

            //if (listaExpirados.Count == 0 && !isForcar)
            //{
            //    return;
            //}

            await DeletarItensExpirados_BancoDeDados(listaExpirados);
            DeletarItensExpirados_Arquivos(listaExpirados);
            List<Item> listaRecriados = await RecriarItensPadrao_BancoDeDados();
            await RecriarItensPadrao_Arquivos(listaRecriados);
        }

        private async Task DeletarItensExpirados_BancoDeDados(List<Item> listaExpirados)
        {
            _context.RemoveRange(listaExpirados);
            await _context.SaveChangesAsync();
        }

        private void DeletarItensExpirados_Arquivos(List<Item> listaExpirados)
        {
            List<string> nomesEspeficos = listaExpirados.Select(x => x.Nome).ToList();

            if (nomesEspeficos.Count == 0)
            {
                return;
            }

            DeletarArquivosEmPasta(path: SistemaConst.PathUploadItem, webRootPath: _webHostEnvironment.ContentRootPath, listaNomes: nomesEspeficos);
        }

        private async Task<List<Item>> RecriarItensPadrao_BancoDeDados()
        {
            Guid pasta1 = Guid.NewGuid();

            List<Item> listaItens =
            [
                new Item() { Guid = pasta1, Nome = "Pasta do @junioranheu", Tipo = ItemTipoEnum.Pasta, GuidPastaPai = null },
                new Item() { Guid = Guid.NewGuid(), Nome = "Pasta vazia", Tipo = ItemTipoEnum.Pasta, GuidPastaPai = null },
                new Item() { Guid = Guid.NewGuid(), Nome = "Rohee", Tipo = ItemTipoEnum.Imagem, GuidPastaPai = null },
                new Item() { Guid = Guid.NewGuid(), Nome = "Perro", Tipo = ItemTipoEnum.Imagem, GuidPastaPai = null },
                new Item() { Guid = Guid.NewGuid(), Nome = "Pota", Tipo = ItemTipoEnum.Imagem, GuidPastaPai = pasta1 }
            ];

            foreach (var item in listaItens)
            {
                await _context.AddAsync(item);
            }

            await _context.SaveChangesAsync();

            return listaItens;
        }

        private async Task RecriarItensPadrao_Arquivos(List<Item> listaRecriados)
        {
            if (listaRecriados.Count == 0)
            {
                return;
            }

            foreach (var item in listaRecriados)
            {
                if (item.Tipo == ItemTipoEnum.Pasta)
                {
                    continue;
                }

                string base64 = string.Empty;

                if (item.Nome.Contains("Rohee", StringComparison.OrdinalIgnoreCase))
                {
                    base64 = SistemaConst.Base64Rohee;
                }
                else if (item.Nome.Contains("Perro", StringComparison.OrdinalIgnoreCase))
                {
                    base64 = SistemaConst.Base64Perro;
                }
                else if (item.Nome.Contains("Pota", StringComparison.OrdinalIgnoreCase))
                {
                    base64 = SistemaConst.Base64Pota;
                }

                if (string.IsNullOrEmpty(base64))
                {
                    continue;
                }

                IFormFile iFormFile = ConverterBase64ParaIFormFile(base64);
                await SubirArquivoEmPasta(arquivo: iFormFile, nomeArquivoSemExtensao: item.Guid.ToString(), extensao: ".jpg", path: SistemaConst.PathUploadItem, nomeArquivoAnteriorSemExtensao: string.Empty, webRootPath: _webHostEnvironment.ContentRootPath);
            }
        }
    }
}