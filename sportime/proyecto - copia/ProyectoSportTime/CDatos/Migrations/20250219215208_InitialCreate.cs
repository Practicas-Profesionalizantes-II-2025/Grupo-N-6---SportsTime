using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
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
                    LastLogIn = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Producto_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Proveedor_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Producto_ID);
                    table.ForeignKey(
                        name: "FK_Productos_Proveedores_Proveedor_ID",
                        column: x => x.Proveedor_ID,
                        principalTable: "Proveedores",
                        principalColumn: "Proveedor_ID",
                        onDelete: ReferentialAction.Restrict);
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
                        onDelete: ReferentialAction.Restrict);
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Turnos_Canchas_Cancha_ID",
                        column: x => x.Cancha_ID,
                        principalTable: "Canchas",
                        principalColumn: "Cancha_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Turnos_Clientes_Cliente_ID",
                        column: x => x.Cliente_ID,
                        principalTable: "Clientes",
                        principalColumn: "Cliente_ID",
                        onDelete: ReferentialAction.Restrict);
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_consumicionProductos_Productos_Producto_ID",
                        column: x => x.Producto_ID,
                        principalTable: "Productos",
                        principalColumn: "Producto_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_consumicionProductos_Turnos_TurnosTurno_ID",
                        column: x => x.TurnosTurno_ID,
                        principalTable: "Turnos",
                        principalColumn: "Turno_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "consumicionXturnos",
                columns: table => new
                {
                    ConsumicionXturno_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Turno_ID = table.Column<int>(type: "int", nullable: false),
                    Consumicion_ID = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_consumicionXturnos", x => x.ConsumicionXturno_ID);
                    table.ForeignKey(
                        name: "FK_consumicionXturnos_Consumiciones_Consumicion_ID",
                        column: x => x.Consumicion_ID,
                        principalTable: "Consumiciones",
                        principalColumn: "Consumicion_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_consumicionXturnos_Turnos_Turno_ID",
                        column: x => x.Turno_ID,
                        principalTable: "Turnos",
                        principalColumn: "Turno_ID",
                        onDelete: ReferentialAction.Restrict);
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
                name: "IX_consumicionXturnos_Consumicion_ID",
                table: "consumicionXturnos",
                column: "Consumicion_ID");

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
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Consumiciones");

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
