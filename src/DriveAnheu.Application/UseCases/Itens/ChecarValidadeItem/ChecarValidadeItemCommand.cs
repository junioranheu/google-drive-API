using DriveAnheu.Domain.Consts;
using DriveAnheu.Domain.Entities;
using DriveAnheu.Domain.Enums;
using DriveAnheu.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using static junioranheu_utils_package.Fixtures.Get;

namespace DriveAnheu.Application.UseCases.Itens.ChecarValidadeItem
{
    public sealed class ChecarValidadeItemCommand(DriveAnheuContext _context) : IChecarValidadeItemCommand
    {
        public async Task Execute(bool isForcar = false)
        {
            List<Item>? listaExpirados = await _context.Itens.Where(i => EF.Functions.DateDiffHour(i.Data, GerarHorarioBrasilia()) > SistemaConst.OffsetChecarValidadeItemEmHoras).ToListAsync();

            if (listaExpirados.Count == 0 && !isForcar)
            {
                return;
            }

            const string path = "XD";
            await DeletarItensExpirados_BancoDeDados(listaExpirados);
            await DeletarItensExpirados_Arquivos(listaExpirados);
            List<Item> listaRecriados = await RecriarItensPadrao_BancoDeDados();
            await RecriarItensPadrao_Arquivos(listaRecriados);
        }

        private async Task DeletarItensExpirados_BancoDeDados(List<Item> listaExpirados)
        {
            _context.RemoveRange(listaExpirados);
            await _context.SaveChangesAsync();
        }

        private async Task DeletarItensExpirados_Arquivos(List<Item> listaExpirados)
        {

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
   
        }

        /// <summary>
        /// // Example: Delete all files in the folder
        /// string folderPath = xqwldkqwp-dkqwopdkqwo0pifjqweoifhewofhew
        /// DeleteFilesInFolder(folderPath);
        /// Example: Delete only .txt and .log files
        ///  string[] fileTypes = { ".txt", ".log" };
        /// DeleteFilesInFolder(folderPath, fileTypes);
        ///   // Example: Delete files with specified names
        /// List<string> specificFileNames = new List<string> { "example1.txt", "example2.txt" };
        /// deletedFiles = DeleteFilesInFolder(folderPath, fileNames: specificFileNames);
        /// </summary>
        private static bool DeletarArquivosEmPasta(string path, List<string>? listaExtensoes = null, List<string>? listaNomes = null)
        {
            if (Directory.Exists(path))
            {
                string[] files;

                if (listaExtensoes is not null && listaExtensoes.Count > 0)
                {
                    files = Directory.GetFiles(path).Where(x => listaExtensoes.Any(extensao => x.EndsWith(extensao, StringComparison.OrdinalIgnoreCase))).ToArray();
                }
                else if (listaNomes is not null && listaNomes.Count > 0)
                {
                    files = Directory.GetFiles(path).Where(x => listaNomes.Contains(Path.GetFileName(x), StringComparer.OrdinalIgnoreCase)).ToArray();
                }
                else
                {
                    files = Directory.GetFiles(path);
                }

                foreach (string file in files)
                {
                    File.Delete(file);
                }

                return true;
            }

            return false;
        }
    }
}