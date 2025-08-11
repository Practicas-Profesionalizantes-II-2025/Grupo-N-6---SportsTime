using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class Migracion2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_consumicionXturnos_Consumiciones_Consumicion_ID",
                table: "consumicionXturnos");

            migrationBuilder.RenameColumn(
                name: "Consumicion_ID",
                table: "consumicionXturnos",
                newName: "Producto_ID");

            migrationBuilder.RenameIndex(
                name: "IX_consumicionXturnos_Consumicion_ID",
                table: "consumicionXturnos",
                newName: "IX_consumicionXturnos_Producto_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_consumicionXturnos_Productos_Producto_ID",
                table: "consumicionXturnos",
                column: "Producto_ID",
                principalTable: "Productos",
                principalColumn: "Producto_ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_consumicionXturnos_Productos_Producto_ID",
                table: "consumicionXturnos");

            migrationBuilder.RenameColumn(
                name: "Producto_ID",
                table: "consumicionXturnos",
                newName: "Consumicion_ID");

            migrationBuilder.RenameIndex(
                name: "IX_consumicionXturnos_Producto_ID",
                table: "consumicionXturnos",
                newName: "IX_consumicionXturnos_Consumicion_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_consumicionXturnos_Consumiciones_Consumicion_ID",
                table: "consumicionXturnos",
                column: "Consumicion_ID",
                principalTable: "Consumiciones",
                principalColumn: "Consumicion_ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
