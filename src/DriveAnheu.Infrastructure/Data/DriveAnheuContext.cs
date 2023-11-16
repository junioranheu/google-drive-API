using DriveAnheu.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DriveAnheu.Infrastructure.Data
{
    public class DriveAnheuContext(DbContextOptions<DriveAnheuContext> x) : DbContext(x)
    {
        // Outros;
        public DbSet<Log> Logs { get; set; }

        // Usuários;
        public DbSet<Usuario> Usuarios { get; set; }

        // Principal;
        public DbSet<Item> Itens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Cascade;
            }
        }
    }
}