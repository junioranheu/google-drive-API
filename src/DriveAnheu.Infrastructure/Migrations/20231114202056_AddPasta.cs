using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DriveAnheu.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPasta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Pastas",
                newName: "Guid");

            migrationBuilder.RenameIndex(
                name: "IX_Pastas_Id",
                table: "Pastas",
                newName: "IX_Pastas_Guid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Itens",
                newName: "Guid");

            migrationBuilder.RenameIndex(
                name: "IX_Itens_Id",
                table: "Itens",
                newName: "IX_Itens_Guid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Guid",
                table: "Pastas",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Pastas_Guid",
                table: "Pastas",
                newName: "IX_Pastas_Id");

            migrationBuilder.RenameColumn(
                name: "Guid",
                table: "Itens",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Itens_Guid",
                table: "Itens",
                newName: "IX_Itens_Id");
        }
    }
}
