using ATM_API.Core.Entities;  // Asegúrate de que las entidades estén en la capa de Dominio
using Microsoft.EntityFrameworkCore;

namespace ATM_API.Infrastructure.Contexts
{
    public class AppDbContext : DbContext
    {
        // Constructor sin parámetros (necesario para migraciones y pruebas)
        public AppDbContext() { }

        // Constructor con DbContextOptions (usado en la inyección de dependencias)
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // DbSets para cada entidad
        public DbSet<Card> Cards { get; set; }        
        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<Account> Accounts { get; set; }

        // Configuración del modelo (opcional, pero útil para configuraciones avanzadas)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la relación uno a muchos entre User y Account
            modelBuilder.Entity<Account>()
                .HasOne(a => a.User)
                .WithMany(u => u.Accounts)
                .HasForeignKey(a => a.UserId);

            // Configuración de la relación entre Card y Account
            modelBuilder.Entity<Card>()
                .HasOne(c => c.Account)
                .WithMany()
                .HasForeignKey(c => c.AccountId);

            // Configuración de la relación entre Transaction y Account
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Account)
                .WithMany()
                .HasForeignKey(t => t.AccountId);

            // Configuración para asegurar unicidad en CardNumber
            modelBuilder.Entity<Card>()
                .HasIndex(c => c.CardNumber)
                .IsUnique();

        }
    }
}
