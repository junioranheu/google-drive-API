using DriveAnheu.Application.UseCases.Itens.Shared.Base;
using DriveAnheu.Domain.Entities;
using DriveAnheu.Domain.Enums;
using DriveAnheu.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DriveAnheu.Application.UseCases.Itens.ObterTamanhoEmDiscoEmBytes
{
    public sealed class ObterTamanhoEmDiscoEmBytesQuery(DriveAnheuContext _context, IWebHostEnvironment _webHostEnvironment) : BaseItem, IObterTamanhoEmDiscoEmBytesQuery
    {
        public async Task<double> Execute(int usuarioId)
        {
            List<Item>? linq = await _context.Itens.
                               Where(i => i.UsuarioId == usuarioId).
                               AsNoTracking().ToListAsync();

            if (linq.Count == 0)
            {
                return 0.0;
            }

            double tamanhoEmDisco = 0.0;

            foreach (var item in linq)
            {
                if (item.Tipo == ItemTipoEnum.Pasta)
                {
                    continue;
                }

                IFormFile arquivo = ObterArquivo_IFormFile(_webHostEnvironment, item.Guid.ToString());
                tamanhoEmDisco += arquivo.Length;
            }

            return tamanhoEmDisco;
        }
    }
}