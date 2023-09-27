using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NIJ.Web.Migrations
{
    public partial class fotoProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Foto",
                table: "Projects",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FotoMineType",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Foto",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "FotoMineType",
                table: "Projects");
        }
    }
}
