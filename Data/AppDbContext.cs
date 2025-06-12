using ConcesionariaVehiculos.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcesionariaVehiculos.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<ServicioPosVenta> ServiciosPosventa { get; set; }
        public DbSet<Factura> Facturas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Clientes");
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Apellido).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Email).IsRequired().HasMaxLength(150);
                entity.Property(c => c.Telefono).IsRequired().HasMaxLength(20);
            });

            modelBuilder.Entity<Vehiculo>(entity =>
            {
                entity.ToTable("Vehiculos");
                entity.HasKey(v => v.Id);
                entity.Property(v => v.Marca).IsRequired().HasMaxLength(100);
                entity.Property(v => v.Modelo).IsRequired().HasMaxLength(100);
                entity.Property(v => v.Año).IsRequired();
                entity.Property(v => v.Precio).IsRequired().HasColumnType("decimal(10,2)");
                entity.Property(v => v.Stock).IsRequired();
            });

            modelBuilder.Entity<Venta>(entity =>
            {
                entity.ToTable("Ventas");
                entity.HasKey(v => v.Id);
                entity.Property(v => v.Fecha).IsRequired();
                entity.Property(v => v.Total).IsRequired().HasColumnType("decimal(10,2)");

                entity.HasOne<Cliente>()
                    .WithMany()
                    .HasForeignKey(v => v.ClienteId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne<Vehiculo>()
                    .WithMany()
                    .HasForeignKey(v => v.VehiculoId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ServicioPosVenta>(entity =>
            {
                entity.ToTable("ServiciosPosVenta");
                entity.HasKey(s => s.Id);
                entity.Property(s => s.TipoServicio).IsRequired().HasMaxLength(100);
                entity.Property(s => s.Fecha).IsRequired();
                entity.Property(s => s.Estado).IsRequired().HasMaxLength(50);

                entity.HasOne<Cliente>()
                    .WithMany()
                    .HasForeignKey(s => s.ClienteId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Factura>(entity =>
            {
                entity.ToTable("Factura");
                entity.HasKey(f => f.Id);
                entity.Property(f => f.VentaId).IsRequired();
                entity.Property(f => f.ClienteNombre).IsRequired().HasMaxLength(100);
                entity.Property(f => f.Fecha).IsRequired();
                entity.Property(f => f.NumeroFactura).IsRequired().HasMaxLength(30);
                entity.Property(f => f.Total).HasColumnType("decimal(10,2)").IsRequired();
                entity.Property(f => f.Vehiculo).IsRequired();

                entity.HasOne(f => f.Venta)
                .WithMany(v => v.Facturas)
               .HasForeignKey(f => f.VentaId)
                .OnDelete(DeleteBehavior.Cascade);



            });





            AppDbContext.Seed(modelBuilder);
        }
    }

}
