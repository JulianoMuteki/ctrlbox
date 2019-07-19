using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CtrlBox.Infra.Context.Migrations
{
    public partial class ProductPicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PictureID",
                table: "Products",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_PictureID",
                table: "Products",
                column: "PictureID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Pictures_PictureID",
                table: "Products",
                column: "PictureID",
                principalTable: "Pictures",
                principalColumn: "PictureID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Pictures_PictureID",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_PictureID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PictureID",
                table: "Products");
        }
    }
}
