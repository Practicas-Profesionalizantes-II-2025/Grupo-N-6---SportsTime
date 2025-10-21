using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CDatos.Migrations
{
    /// <inheritdoc />
    public partial class AddClientes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Usuario_ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Usuario_ID",
                keyValue: 2);

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Cliente_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroTelefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Cliente_ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Usuario_ID", "Contraseña", "Email", "Nombre", "NumeroTelefono", "Rol" },
                values: new object[,]
                {
                    { 1, "1111", "usuario1@test.com", "Usuario1", "3493112233", "Cliente" },
                    { 2, "2222", "usuario2@test.com", "Usuario2", "3493112244", "Administrador" }
                });
        }
    }
}
