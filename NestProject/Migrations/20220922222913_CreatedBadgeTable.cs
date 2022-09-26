using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NestProject.Migrations
{
    public partial class CreatedBadgeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BadgeId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Badges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Badges", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_BadgeId",
                table: "Products",
                column: "BadgeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Badges_BadgeId",
                table: "Products",
                column: "BadgeId",
                principalTable: "Badges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Badges_BadgeId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Badges");

            migrationBuilder.DropIndex(
                name: "IX_Products_BadgeId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "BadgeId",
                table: "Products");
        }
    }
}
