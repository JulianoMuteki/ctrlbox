using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CtrlBox.Infra.Context.Migrations
{
    public partial class BoxTypePicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PictureID",
                table: "BoxesTypes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BoxesTypes_PictureID",
                table: "BoxesTypes",
                column: "PictureID");

            migrationBuilder.AddForeignKey(
                name: "FK_BoxesTypes_Pictures_PictureID",
                table: "BoxesTypes",
                column: "PictureID",
                principalTable: "Pictures",
                principalColumn: "PictureID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoxesTypes_Pictures_PictureID",
                table: "BoxesTypes");

            migrationBuilder.DropIndex(
                name: "IX_BoxesTypes_PictureID",
                table: "BoxesTypes");

            migrationBuilder.DropColumn(
                name: "PictureID",
                table: "BoxesTypes");
        }
    }
}
