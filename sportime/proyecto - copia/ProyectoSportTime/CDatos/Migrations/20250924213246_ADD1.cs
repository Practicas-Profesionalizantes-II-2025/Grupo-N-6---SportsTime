using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CDatos.Migrations
{
    /// <inheritdoc />
    public partial class ADD1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Administradores",
                columns: table => new
                {
                    Admin_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contraseña = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSuperAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administradores", x => x.Admin_ID);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Cliente_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroTelefono = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Cliente_ID);
                });

            migrationBuilder.CreateTable(
                name: "Consumiciones",
                columns: table => new
                {
                    Consumicion_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consumiciones", x => x.Consumicion_ID);
                });

            migrationBuilder.CreateTable(
                name: "Deportes",
                columns: table => new
                {
                    Deporte_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deportes", x => x.Deporte_ID);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    Proveedor_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.Proveedor_ID);
                });

            migrationBuilder.CreateTable(
                name: "Canchas",
                columns: table => new
                {
                    Cancha_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deporte_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Canchas", x => x.Cancha_ID);
                    table.ForeignKey(
                        name: "FK_Canchas_Deportes_Deporte_ID",
                        column: x => x.Deporte_ID,
                        principalTable: "Deportes",
                        principalColumn: "Deporte_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Producto_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Proveedor_ID = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Producto_ID);
                    table.ForeignKey(
                        name: "FK_Productos_Proveedores_Proveedor_ID",
                        column: x => x.Proveedor_ID,
                        principalTable: "Proveedores",
                        principalColumn: "Proveedor_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Elementos",
                columns: table => new
                {
                    Elemento_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Cancha_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Elementos", x => x.Elemento_ID);
                    table.ForeignKey(
                        name: "FK_Elementos_Canchas_Cancha_ID",
                        column: x => x.Cancha_ID,
                        principalTable: "Canchas",
                        principalColumn: "Cancha_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Turnos",
                columns: table => new
                {
                    Turno_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoraInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoraFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cliente_ID = table.Column<int>(type: "int", nullable: false),
                    Admin_ID = table.Column<int>(type: "int", nullable: false),
                    Cancha_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turnos", x => x.Turno_ID);
                    table.ForeignKey(
                        name: "FK_Turnos_Administradores_Admin_ID",
                        column: x => x.Admin_ID,
                        principalTable: "Administradores",
                        principalColumn: "Admin_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Turnos_Canchas_Cancha_ID",
                        column: x => x.Cancha_ID,
                        principalTable: "Canchas",
                        principalColumn: "Cancha_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Turnos_Clientes_Cliente_ID",
                        column: x => x.Cliente_ID,
                        principalTable: "Clientes",
                        principalColumn: "Cliente_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "consumicionProductos",
                columns: table => new
                {
                    ConsumicionProducto_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Consumicion_ID = table.Column<int>(type: "int", nullable: false),
                    Producto_ID = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    TurnosTurno_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_consumicionProductos", x => x.ConsumicionProducto_ID);
                    table.ForeignKey(
                        name: "FK_consumicionProductos_Consumiciones_Consumicion_ID",
                        column: x => x.Consumicion_ID,
                        principalTable: "Consumiciones",
                        principalColumn: "Consumicion_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_consumicionProductos_Productos_Producto_ID",
                        column: x => x.Producto_ID,
                        principalTable: "Productos",
                        principalColumn: "Producto_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_consumicionProductos_Turnos_TurnosTurno_ID",
                        column: x => x.TurnosTurno_ID,
                        principalTable: "Turnos",
                        principalColumn: "Turno_ID");
                });

            migrationBuilder.CreateTable(
                name: "consumicionXturnos",
                columns: table => new
                {
                    ConsumicionXturno_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Turno_ID = table.Column<int>(type: "int", nullable: false),
                    Producto_ID = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_consumicionXturnos", x => x.ConsumicionXturno_ID);
                    table.ForeignKey(
                        name: "FK_consumicionXturnos_Productos_Producto_ID",
                        column: x => x.Producto_ID,
                        principalTable: "Productos",
                        principalColumn: "Producto_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_consumicionXturnos_Turnos_Turno_ID",
                        column: x => x.Turno_ID,
                        principalTable: "Turnos",
                        principalColumn: "Turno_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Administradores",
                columns: new[] { "Admin_ID", "Contraseña", "Email", "IsSuperAdmin", "Nombre" },
                values: new object[,]
                {
                    { 1, "1234", "admin1@test.com", true, "Admin1" },
                    { 2, "1234", "admin2@test.com", false, "Admin2" },
                    { 3, "1234", "admin3@test.com", false, "Admin3" },
                    { 4, "1234", "admin4@test.com", true, "Admin4" }
                });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Cliente_ID", "Nombre", "NumeroTelefono" },
                values: new object[,]
                {
                    { 1, "Juan Perez", 111111111 },
                    { 2, "Ana Gomez", 222222222 },
                    { 3, "Carlos Lopez", 333333333 },
                    { 4, "Maria Diaz", 444444444 }
                });

            migrationBuilder.InsertData(
                table: "Consumiciones",
                columns: new[] { "Consumicion_ID", "Cantidad", "Precio" },
                values: new object[,]
                {
                    { 1, 2, 1000m },
                    { 2, 3, 1500m },
                    { 3, 1, 500m },
                    { 4, 5, 2500m }
                });

            migrationBuilder.InsertData(
                table: "Deportes",
                columns: new[] { "Deporte_ID", "Tipo" },
                values: new object[,]
                {
                    { 1, "Fútbol" },
                    { 2, "Básquet" },
                    { 3, "Vóley" },
                    { 4, "Tenis" }
                });

            migrationBuilder.InsertData(
                table: "Proveedores",
                columns: new[] { "Proveedor_ID", "Email", "Nombre", "Telefono" },
                values: new object[,]
                {
                    { 1, "prov1@test.com", "Proveedor1", "1111" },
                    { 2, "prov2@test.com", "Proveedor2", "2222" },
                    { 3, "prov3@test.com", "Proveedor3", "3333" },
                    { 4, "prov4@test.com", "Proveedor4", "4444" }
                });

            migrationBuilder.InsertData(
                table: "Canchas",
                columns: new[] { "Cancha_ID", "Deporte_ID" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 4 }
                });

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "Producto_ID", "Descripcion", "Precio", "Proveedor_ID", "Tipo" },
                values: new object[,]
                {
                    { 1, "Agua Mineral", 500m, 1, "Bebida" },
                    { 2, "Papas Fritas", 800m, 2, "Snack" },
                    { 3, "Gaseosa", 700m, 3, "Bebida" },
                    { 4, "Barrita Energética", 600m, 4, "Snack" }
                });

            migrationBuilder.InsertData(
                table: "Elementos",
                columns: new[] { "Elemento_ID", "Cancha_ID", "Cantidad", "Nombre" },
                values: new object[,]
                {
                    { 1, 1, 10, "Pelotas Fútbol" },
                    { 2, 2, 8, "Pelotas Básquet" },
                    { 3, 3, 12, "Pelotas Vóley" },
                    { 4, 4, 6, "Raquetas" }
                });

            migrationBuilder.InsertData(
                table: "Turnos",
                columns: new[] { "Turno_ID", "Admin_ID", "Cancha_ID", "Cliente_ID", "HoraFin", "HoraInicio" },
                values: new object[,]
                {
                    { 1, 1, 1, 1, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, 2, 2, new DateTime(2024, 1, 1, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "consumicionProductos",
                columns: new[] { "ConsumicionProducto_ID", "Cantidad", "Consumicion_ID", "Producto_ID", "TurnosTurno_ID" },
                values: new object[,]
                {
                    { 1, 2, 1, 1, null },
                    { 2, 1, 2, 2, null },
                    { 3, 3, 3, 3, null },
                    { 4, 2, 4, 4, null }
                });

            migrationBuilder.InsertData(
                table: "consumicionXturnos",
                columns: new[] { "ConsumicionXturno_ID", "Cantidad", "Producto_ID", "Turno_ID" },
                values: new object[,]
                {
                    { 3, 3, 3, 3 },
                    { 4, 4, 4, 4 },
                    { 1, 1, 1, 1 },
                    { 2, 2, 2, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Canchas_Deporte_ID",
                table: "Canchas",
                column: "Deporte_ID");

            migrationBuilder.CreateIndex(
                name: "IX_consumicionProductos_Consumicion_ID",
                table: "consumicionProductos",
                column: "Consumicion_ID");

            migrationBuilder.CreateIndex(
                name: "IX_consumicionProductos_Producto_ID",
                table: "consumicionProductos",
                column: "Producto_ID");

            migrationBuilder.CreateIndex(
                name: "IX_consumicionProductos_TurnosTurno_ID",
                table: "consumicionProductos",
                column: "TurnosTurno_ID");

            migrationBuilder.CreateIndex(
                name: "IX_consumicionXturnos_Producto_ID",
                table: "consumicionXturnos",
                column: "Producto_ID");

            migrationBuilder.CreateIndex(
                name: "IX_consumicionXturnos_Turno_ID",
                table: "consumicionXturnos",
                column: "Turno_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Elementos_Cancha_ID",
                table: "Elementos",
                column: "Cancha_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_Proveedor_ID",
                table: "Productos",
                column: "Proveedor_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_Admin_ID",
                table: "Turnos",
                column: "Admin_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_Cancha_ID",
                table: "Turnos",
                column: "Cancha_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_Cliente_ID",
                table: "Turnos",
                column: "Cliente_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "consumicionProductos");

            migrationBuilder.DropTable(
                name: "consumicionXturnos");

            migrationBuilder.DropTable(
                name: "Elementos");

            migrationBuilder.DropTable(
                name: "Consumiciones");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Turnos");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "Administradores");

            migrationBuilder.DropTable(
                name: "Canchas");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Deportes");
        }
    }
}
