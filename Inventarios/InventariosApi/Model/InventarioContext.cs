using InventariosApi.Entity;
using Microsoft.EntityFrameworkCore;

namespace InventariosApi.Model
{
    public class InventarioContext : DbContext
    {
        public InventarioContext(DbContextOptions<InventarioContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Inventarios> Inventarios { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost\\DEV;Database=Reservas_proxy;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inventarios>(entity =>
            {
                entity.Property(e => e.IdInventario).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasColumnName("nombre")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TipoProducto)
                    .HasColumnName("tipoproducto")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Precio)
                .HasColumnName("precio");

                entity.Property(e => e.Anaquel)
                    .HasColumnName("anaquel")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Activo)
                    .HasColumnName("activo");
            });
        }
    }
}
