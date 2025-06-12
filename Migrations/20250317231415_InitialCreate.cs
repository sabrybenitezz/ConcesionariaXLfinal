using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace technical_tests_backend_ssr.Migrations
{
	/// <inheritdoc />
	public partial class InitialCreate : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterDatabase()
				.Annotation("MySql:CharSet", "utf8mb4");

			migrationBuilder.CreateTable(
				name: "Clientes",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
					Nombre = table.Column<string>(type: "longtext", nullable: false)
						.Annotation("MySql:CharSet", "utf8mb4"),
					Apellido = table.Column<string>(type: "longtext", nullable: false)
						.Annotation("MySql:CharSet", "utf8mb4"),
					Email = table.Column<string>(type: "longtext", nullable: false)
						.Annotation("MySql:CharSet", "utf8mb4"),
					Telefono = table.Column<string>(type: "longtext", nullable: false)
						.Annotation("MySql:CharSet", "utf8mb4")
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Clientes", x => x.Id);
				})
				.Annotation("MySql:CharSet", "utf8mb4");

			migrationBuilder.CreateTable(
				name: "Vehiculos",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
					Marca = table.Column<string>(type: "longtext", nullable: false)
						.Annotation("MySql:CharSet", "utf8mb4"),
					Modelo = table.Column<string>(type: "longtext", nullable: false)
						.Annotation("MySql:CharSet", "utf8mb4"),
					A�o = table.Column<int>(type: "int", nullable: false),
					Precio = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
					Stock = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Vehiculos", x => x.Id);
				})
				.Annotation("MySql:CharSet", "utf8mb4");

			migrationBuilder.CreateTable(
				name: "ServiciosPosVenta",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
					ClienteId = table.Column<int>(type: "int", nullable: false),
					TipoServicio = table.Column<string>(type: "longtext", nullable: false)
						.Annotation("MySql:CharSet", "utf8mb4"),
					Fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false),
					Estado = table.Column<string>(type: "longtext", nullable: false)
						.Annotation("MySql:CharSet", "utf8mb4")
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ServiciosPosVenta", x => x.Id);
					table.ForeignKey(
						name: "FK_ServiciosPosVenta_Clientes_ClienteId",
						column: x => x.ClienteId,
						principalTable: "Clientes",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				})
				.Annotation("MySql:CharSet", "utf8mb4");

			migrationBuilder.CreateTable(
				name: "Ventas",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
					ClienteId = table.Column<int>(type: "int", nullable: false),
					VehiculoId = table.Column<int>(type: "int", nullable: false),
					Fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false),
					Total = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Ventas", x => x.Id);
					table.ForeignKey(
						name: "FK_Ventas_Clientes_ClienteId",
						column: x => x.ClienteId,
						principalTable: "Clientes",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Ventas_Vehiculos_VehiculoId",
						column: x => x.VehiculoId,
						principalTable: "Vehiculos",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				})
				.Annotation("MySql:CharSet", "utf8mb4");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "ServiciosPosVenta");
			migrationBuilder.DropTable(
				name: "Ventas");
			migrationBuilder.DropTable(
				name: "Clientes");
			migrationBuilder.DropTable(
				name: "Vehiculos");
		}
	}
}