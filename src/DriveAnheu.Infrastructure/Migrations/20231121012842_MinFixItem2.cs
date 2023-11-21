using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DriveAnheu.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MinFixItem2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataMod",
                table: "Itens",
                type: "datetime(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataMod",
                table: "Itens");
        }
    }
}
