using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CDatos.Migrations
{
    /// <inheritdoc />
    public partial class AddCanchasBackend : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Canchas",
                keyColumn: "Cancha_ID",
                keyValue: 1,
                column: "Activa",
                value: true);

            migrationBuilder.UpdateData(
                table: "Canchas",
                keyColumn: "Cancha_ID",
                keyValue: 2,
                column: "Activa",
                value: true);

            migrationBuilder.UpdateData(
                table: "Canchas",
                keyColumn: "Cancha_ID",
                keyValue: 3,
                column: "Activa",
                value: true);

            migrationBuilder.UpdateData(
                table: "Canchas",
                keyColumn: "Cancha_ID",
                keyValue: 4,
                column: "Activa",
                value: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Canchas",
                keyColumn: "Cancha_ID",
                keyValue: 1,
                column: "Activa",
                value: false);

            migrationBuilder.UpdateData(
                table: "Canchas",
                keyColumn: "Cancha_ID",
                keyValue: 2,
                column: "Activa",
                value: false);

            migrationBuilder.UpdateData(
                table: "Canchas",
                keyColumn: "Cancha_ID",
                keyValue: 3,
                column: "Activa",
                value: false);

            migrationBuilder.UpdateData(
                table: "Canchas",
                keyColumn: "Cancha_ID",
                keyValue: 4,
                column: "Activa",
                value: false);
        }
    }
}
