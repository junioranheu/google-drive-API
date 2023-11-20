using DriveAnheu.Application.UseCases.Itens.Shared.Output;
using DriveAnheu.Domain.Entities;
using DriveAnheu.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DriveAnheu.Application.UseCases.Itens.ListarFolderRotas
{
    public sealed class ListarFolderRotasQuery(DriveAnheuContext _context) : IListarFolderRotasQuery
    {
        public async Task<List<FolderRotaOutput>> Execute(Guid guid)
        {
            List<FolderRotaOutput> output = [];

            if (guid == Guid.Empty)
            {
                return output;
            }

            Guid rota = Guid.Empty, rotaPastaPai = Guid.Empty;
            string nome = string.Empty;
            bool isExistePastaPai = true;

            do
            {
                (rota, nome, rotaPastaPai, isExistePastaPai) = await ObterRota(guid);

                guid = rotaPastaPai; // Renovar parâmetro;

                // Adicionar nova rota;
                FolderRotaOutput x = new()
                {
                    Guid = rota,
                    Nome = nome
                };

                output.Add(x);
            } while (isExistePastaPai);

            return output;
        }

        private async Task<(Guid rota, string nome, Guid rotaPastaPai, bool isExistePastaPai)> ObterRota(Guid guid)
        {
            Item? linq = await _context.Itens.Where(i => i.Guid == guid).AsNoTracking().FirstOrDefaultAsync();

            if (linq is null)
            {
                return (Guid.Empty, "Meu Drive comp.", Guid.Empty, false);
            }

            Guid rota = linq.Guid;
            string nome = linq.Nome;
            Guid rotaPastaPai = linq.GuidPastaPai;
            bool isExistePastaPai = (linq.GuidPastaPai != Guid.Empty) || (linq.Guid != Guid.Empty && linq.GuidPastaPai == Guid.Empty);

            return (rota, nome, rotaPastaPai, isExistePastaPai);
        }
    }
}