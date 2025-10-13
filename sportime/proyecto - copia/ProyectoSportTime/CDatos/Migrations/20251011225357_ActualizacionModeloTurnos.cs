using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CDatos.Migrations
{
    /// <inheritdoc />
    public partial class ActualizacionModeloTurnos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Canchas",
                columns: table => new
                {
                    Cancha_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deporte_ID = table.Column<int>(type: "int", nullable: false),
                    Activa = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Canchas", x => x.Cancha_ID);
                });

            migrationBuilder.CreateTable(
                name: "Deportes",
                columns: table => new
                {
                    Deporte_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deportes", x => x.Deporte_ID);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Producto_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoProducto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Proveedor_ID = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Producto_ID);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    Proveedor_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.Proveedor_ID);
                });

            migrationBuilder.CreateTable(
                name: "TurnoProductos",
                columns: table => new
                {
                    TurnoProducto_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Turno_ID = table.Column<int>(type: "int", nullable: false),
                    Producto_ID = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurnoProductos", x => x.TurnoProducto_ID);
                });

            migrationBuilder.CreateTable(
                name: "Turnos",
                columns: table => new
                {
                    Turno_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoraInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoraFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Usuario_ID = table.Column<int>(type: "int", nullable: false),
                    Cancha_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turnos", x => x.Turno_ID);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Usuario_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroTelefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contraseña = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Usuario_ID);
                });

            migrationBuilder.InsertData(
                table: "Canchas",
                columns: new[] { "Cancha_ID", "Activa", "Deporte_ID" },
                values: new object[,]
                {
                    { 1, false, 1 },
                    { 2, false, 2 },
                    { 3, false, 3 },
                    { 4, false, 4 }
                });

            migrationBuilder.InsertData(
                table: "Deportes",
                columns: new[] { "Deporte_ID", "Nombre" },
                values: new object[,]
                {
                    { 1, "Fútbol" },
                    { 2, "Básquet" },
                    { 3, "Vóley" },
                    { 4, "Tenis" }
                });

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "Producto_ID", "Precio", "Proveedor_ID", "TipoProducto" },
                values: new object[,]
                {
                    { 1, 500m, 1, "Agua Mineral" },
                    { 2, 800m, 2, "Papas Fritas" },
                    { 3, 700m, 3, "Gaseosa" },
                    { 4, 600m, 4, "Barrita Energética" }
                });

            migrationBuilder.InsertData(
                table: "Proveedores",
                columns: new[] { "Proveedor_ID", "Direccion", "Email", "Nombre", "Telefono" },
                values: new object[,]
                {
                    { 1, "Buenos aires 510", "prov1@test.com", "Proveedor1", "1111" },
                    { 2, "Buenos aires 511", "prov2@test.com", "Proveedor2", "2222" },
                    { 3, "Buenos aires 512", "prov3@test.com", "Proveedor3", "3333" },
                    { 4, "Buenos aires 513", "prov4@test.com", "Proveedor4", "4444" }
                });

            migrationBuilder.InsertData(
                table: "TurnoProductos",
                columns: new[] { "TurnoProducto_ID", "Cantidad", "Producto_ID", "Turno_ID" },
                values: new object[,]
                {
                    { 1, 2, 1, 1 },
                    { 2, 1, 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "Turnos",
                columns: new[] { "Turno_ID", "Cancha_ID", "Estado", "HoraFin", "HoraInicio", "Usuario_ID" },
                values: new object[,]
                {
                    { 1, 1, "Confirmado", new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, 2, "Confirmado", new DateTime(2024, 1, 1, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 2 }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Usuario_ID", "Contraseña", "Email", "Nombre", "NumeroTelefono", "Rol" },
                values: new object[,]
                {
                    { 1, "1111", "usuario1@test.com", "Usuario1", "3493112233", "Cliente" },
                    { 2, "2222", "usuario2@test.com", "Usuario2", "3493112244", "Administrador" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Canchas");

            migrationBuilder.DropTable(
                name: "Deportes");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "TurnoProductos");

            migrationBuilder.DropTable(
                name: "Turnos");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
