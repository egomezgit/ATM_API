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

        // Configuración del modelo (opcional, pero útil para configuraciones avanzadas)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           


            // Configuración de la entidad Card
            modelBuilder.Entity<Card>(entity =>
            {
                entity.HasKey(c => c.Id);  // Clave primaria
                entity.Property(c => c.CardNumber).IsRequired().HasMaxLength(16);
                entity.Property(c => c.PINHash).IsRequired().HasMaxLength(4);
                //entity.Property(u => u.).IsRequired().HasMaxLength(100);
               // entity.Property(u => u.AccountNumber).IsRequired().HasMaxLength(20);
               // entity.Property(u => u.Balance).HasColumnType("decimal(18, 2)").IsRequired();
               // entity.Property(u => u.LastWithdrawalDate).IsRequired(false);  // Opcional
                entity.Property(u => u.IsBlocked).IsRequired().HasDefaultValue(false);

                // Relación uno a muchos: User -> Transactions
                entity.HasMany(u => u.Transactions)
                      .WithOne(t => t.User)
                      .HasForeignKey(t => t.CardNumber)
                      .HasPrincipalKey(u => u.CardNumber);
            });

            // Configuración de la entidad Transaction
            //modelBuilder.Entity<Transaction>(entity =>
            //{
            //    entity.HasKey(t => t.Id);  // Clave primaria
            //    entity.Property(t => t.CardNumber).IsRequired().HasMaxLength(16);
            //    entity.Property(t => t.Amount).HasColumnType("decimal(18, 2)").IsRequired();
            //    entity.Property(t => t.TransactionDate).IsRequired();
            //    entity.Property(t => t.TransactionType).IsRequired().HasMaxLength(50);

            //    // Índice para mejorar las consultas por número de tarjeta
            //    entity.HasIndex(t => t.CardNumber);
            //});
        }
    }
}
