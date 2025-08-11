using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class migracion3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Canchas_Deportes_Deporte_ID",
                table: "Canchas");

            migrationBuilder.DropForeignKey(
                name: "FK_consumicionProductos_Consumiciones_Consumicion_ID",
                table: "consumicionProductos");

            migrationBuilder.DropForeignKey(
                name: "FK_consumicionProductos_Productos_Producto_ID",
                table: "consumicionProductos");

            migrationBuilder.DropForeignKey(
                name: "FK_consumicionProductos_Turnos_TurnosTurno_ID",
                table: "consumicionProductos");

            migrationBuilder.DropForeignKey(
                name: "FK_consumicionXturnos_Productos_Producto_ID",
                table: "consumicionXturnos");

            migrationBuilder.DropForeignKey(
                name: "FK_consumicionXturnos_Turnos_Turno_ID",
                table: "consumicionXturnos");

            migrationBuilder.DropForeignKey(
                name: "FK_Elementos_Canchas_Cancha_ID",
                table: "Elementos");

            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Proveedores_Proveedor_ID",
                table: "Productos");

            migrationBuilder.DropForeignKey(
                name: "FK_Turnos_Administradores_Admin_ID",
                table: "Turnos");

            migrationBuilder.DropForeignKey(
                name: "FK_Turnos_Canchas_Cancha_ID",
                table: "Turnos");

            migrationBuilder.DropForeignKey(
                name: "FK_Turnos_Clientes_Cliente_ID",
                table: "Turnos");

            migrationBuilder.AddForeignKey(
                name: "FK_Canchas_Deportes_Deporte_ID",
                table: "Canchas",
                column: "Deporte_ID",
                principalTable: "Deportes",
                principalColumn: "Deporte_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_consumicionProductos_Consumiciones_Consumicion_ID",
                table: "consumicionProductos",
                column: "Consumicion_ID",
                principalTable: "Consumiciones",
                principalColumn: "Consumicion_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_consumicionProductos_Productos_Producto_ID",
                table: "consumicionProductos",
                column: "Producto_ID",
                principalTable: "Productos",
                principalColumn: "Producto_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_consumicionProductos_Turnos_TurnosTurno_ID",
                table: "consumicionProductos",
                column: "TurnosTurno_ID",
                principalTable: "Turnos",
                principalColumn: "Turno_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_consumicionXturnos_Productos_Producto_ID",
                table: "consumicionXturnos",
                column: "Producto_ID",
                principalTable: "Productos",
                principalColumn: "Producto_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_consumicionXturnos_Turnos_Turno_ID",
                table: "consumicionXturnos",
                column: "Turno_ID",
                principalTable: "Turnos",
                principalColumn: "Turno_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Elementos_Canchas_Cancha_ID",
                table: "Elementos",
                column: "Cancha_ID",
                principalTable: "Canchas",
                principalColumn: "Cancha_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Proveedores_Proveedor_ID",
                table: "Productos",
                column: "Proveedor_ID",
                principalTable: "Proveedores",
                principalColumn: "Proveedor_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Turnos_Administradores_Admin_ID",
                table: "Turnos",
                column: "Admin_ID",
                principalTable: "Administradores",
                principalColumn: "Admin_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Turnos_Canchas_Cancha_ID",
                table: "Turnos",
                column: "Cancha_ID",
                principalTable: "Canchas",
                principalColumn: "Cancha_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Turnos_Clientes_Cliente_ID",
                table: "Turnos",
                column: "Cliente_ID",
                principalTable: "Clientes",
                principalColumn: "Cliente_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Canchas_Deportes_Deporte_ID",
                table: "Canchas");

            migrationBuilder.DropForeignKey(
                name: "FK_consumicionProductos_Consumiciones_Consumicion_ID",
                table: "consumicionProductos");

            migrationBuilder.DropForeignKey(
                name: "FK_consumicionProductos_Productos_Producto_ID",
                table: "consumicionProductos");

            migrationBuilder.DropForeignKey(
                name: "FK_consumicionProductos_Turnos_TurnosTurno_ID",
                table: "consumicionProductos");

            migrationBuilder.DropForeignKey(
                name: "FK_consumicionXturnos_Productos_Producto_ID",
                table: "consumicionXturnos");

            migrationBuilder.DropForeignKey(
                name: "FK_consumicionXturnos_Turnos_Turno_ID",
                table: "consumicionXturnos");

            migrationBuilder.DropForeignKey(
                name: "FK_Elementos_Canchas_Cancha_ID",
                table: "Elementos");

            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Proveedores_Proveedor_ID",
                table: "Productos");

            migrationBuilder.DropForeignKey(
                name: "FK_Turnos_Administradores_Admin_ID",
                table: "Turnos");

            migrationBuilder.DropForeignKey(
                name: "FK_Turnos_Canchas_Cancha_ID",
                table: "Turnos");

            migrationBuilder.DropForeignKey(
                name: "FK_Turnos_Clientes_Cliente_ID",
                table: "Turnos");

            migrationBuilder.AddForeignKey(
                name: "FK_Canchas_Deportes_Deporte_ID",
                table: "Canchas",
                column: "Deporte_ID",
                principalTable: "Deportes",
                principalColumn: "Deporte_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_consumicionProductos_Consumiciones_Consumicion_ID",
                table: "consumicionProductos",
                column: "Consumicion_ID",
                principalTable: "Consumiciones",
                principalColumn: "Consumicion_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_consumicionProductos_Productos_Producto_ID",
                table: "consumicionProductos",
                column: "Producto_ID",
                principalTable: "Productos",
                principalColumn: "Producto_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_consumicionProductos_Turnos_TurnosTurno_ID",
                table: "consumicionProductos",
                column: "TurnosTurno_ID",
                principalTable: "Turnos",
                principalColumn: "Turno_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_consumicionXturnos_Productos_Producto_ID",
                table: "consumicionXturnos",
                column: "Producto_ID",
                principalTable: "Productos",
                principalColumn: "Producto_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_consumicionXturnos_Turnos_Turno_ID",
                table: "consumicionXturnos",
                column: "Turno_ID",
                principalTable: "Turnos",
                principalColumn: "Turno_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Elementos_Canchas_Cancha_ID",
                table: "Elementos",
                column: "Cancha_ID",
                principalTable: "Canchas",
                principalColumn: "Cancha_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Proveedores_Proveedor_ID",
                table: "Productos",
                column: "Proveedor_ID",
                principalTable: "Proveedores",
                principalColumn: "Proveedor_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Turnos_Administradores_Admin_ID",
                table: "Turnos",
                column: "Admin_ID",
                principalTable: "Administradores",
                principalColumn: "Admin_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Turnos_Canchas_Cancha_ID",
                table: "Turnos",
                column: "Cancha_ID",
                principalTable: "Canchas",
                principalColumn: "Cancha_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Turnos_Clientes_Cliente_ID",
                table: "Turnos",
                column: "Cliente_ID",
                principalTable: "Clientes",
                principalColumn: "Cliente_ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
