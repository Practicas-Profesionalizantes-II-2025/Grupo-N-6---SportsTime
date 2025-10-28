using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CDatos.Migrations
{
    /// <inheritdoc />
    public partial class AddClienteToTurnos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Cliente_ID",
                table: "Turnos",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Turnos",
                keyColumn: "Turno_ID",
                keyValue: 1,
                column: "Cliente_ID",
                value: null);

            migrationBuilder.UpdateData(
                table: "Turnos",
                keyColumn: "Turno_ID",
                keyValue: 2,
                column: "Cliente_ID",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cliente_ID",
                table: "Turnos");
        }
    }
}
