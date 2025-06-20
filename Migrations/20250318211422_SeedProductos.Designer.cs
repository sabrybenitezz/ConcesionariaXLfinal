// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ConcesionariaVehiculos.Data;

#nullable disable

namespace ConcesionariaVehiculos.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250318211422_SeedProductos")]
    partial class SeedProductos
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("ConcesionariaVehiculos.Cliente", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                b.Property<string>("Nombre")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("varchar(100)");

                b.Property<string>("Apellido")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("varchar(100)");

                b.Property<string>("Email")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("varchar(100)");

                b.Property<string>("Telefono")
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnType("varchar(20)");

                b.HasKey("Id");

                b.ToTable("Clientes", (string)null);
            });

            modelBuilder.Entity("ConcesionariaVehiculos.Vehiculo", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                b.Property<string>("Marca")
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("varchar(50)");

                b.Property<string>("Modelo")
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("varchar(50)");

                b.Property<int>("A�o")
                    .HasColumnType("int");

                b.Property<decimal>("Precio")
                    .HasColumnType("decimal(10,2)");

                b.Property<int>("Stock")
                    .HasColumnType("int");

                b.HasKey("Id");

                b.ToTable("Vehiculos", (string)null);
            });

            modelBuilder.Entity("ConcesionariaVehiculos.Venta", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                b.Property<int>("ClienteId")
                    .HasColumnType("int");

                b.Property<int>("VehiculoId")
                    .HasColumnType("int");

                b.Property<DateTime>("Fecha")
                    .HasColumnType("datetime(6)");

                b.Property<decimal>("Total")
                    .HasColumnType("decimal(10,2)");

                b.HasKey("Id");

                b.HasIndex("ClienteId");
                b.HasIndex("VehiculoId");

                b.ToTable("Ventas", (string)null);
            });

            modelBuilder.Entity("ConcesionariaVehiculos.ServicioPosVenta", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                b.Property<int>("ClienteId")
                    .HasColumnType("int");

                b.Property<string>("TipoServicio")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("varchar(100)");

                b.Property<DateTime>("Fecha")
                    .HasColumnType("datetime(6)");

                b.Property<string>("Estado")
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("varchar(50)");

                b.HasKey("Id");

                b.HasIndex("ClienteId");

                b.ToTable("ServiciosPostVenta", (string)null);
            });

            modelBuilder.Entity("ConcesionariaVehiculos.Models.Venta", b =>
            {
                b.HasOne("ConcesionariaVehiculos.Cliente", "Cliente")
                    .WithMany("Ventas")
                    .HasForeignKey("ClienteId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("ConcesionariaVehiculos", "Vehiculo")
                    .WithMany("Ventas")
                    .HasForeignKey("VehiculoId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("ConcesionariaVehiculos.ServicioPosVenta", b =>
            {
                b.HasOne("ConcesionariaVehiculos.Cliente", "Cliente")
                    .WithMany("ServiciosPosVenta")
                    .HasForeignKey("ClienteId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });
#pragma warning restore 612, 618
        }
    }
}