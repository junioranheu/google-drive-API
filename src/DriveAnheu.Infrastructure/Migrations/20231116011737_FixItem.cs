using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DriveAnheu.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pastas");

            migrationBuilder.AddColumn<Guid>(
                name: "GuidPastaPai",
                table: "Itens",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<int>(
                name: "Tipo",
                table: "Itens",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Itens_GuidPastaPai",
                table: "Itens",
                column: "GuidPastaPai");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Itens_GuidPastaPai",
                table: "Itens");

            migrationBuilder.DropColumn(
                name: "GuidPastaPai",
                table: "Itens");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Itens");

            migrationBuilder.CreateTable(
                name: "Pastas",
                columns: table => new
                {
                    PastaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UsuarioId = table.Column<int>(type: "int", nullable: true),
                    Data = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Guid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Nome = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pastas", x => x.PastaId);
                    table.ForeignKey(
                        name: "FK_Pastas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Pastas_Guid",
                table: "Pastas",
                column: "Guid");

            migrationBuilder.CreateIndex(
                name: "IX_Pastas_UsuarioId",
                table: "Pastas",
                column: "UsuarioId");
        }
    }
}
