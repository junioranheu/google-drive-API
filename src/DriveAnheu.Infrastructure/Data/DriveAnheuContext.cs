using DriveAnheu.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DriveAnheu.Infrastructure.Data
{
    public class DriveAnheuContext(DbContextOptions<DriveAnheuContext> x) : DbContext(x)
    {
        // Outros;
        public DbSet<Log> Logs { get; set; }

        // Principal;
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Item> Itens { get; set; }
        public DbSet<HistoricoExpiracao> HistoricosExpiracoes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Cascade;
            }
        }
    }
}