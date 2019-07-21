using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CtrlBox.Infra.Context.Migrations
{
    public partial class TracyTypePicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PictureID",
                table: "TracesTypes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TracesTypes_PictureID",
                table: "TracesTypes",
                column: "PictureID");

            migrationBuilder.AddForeignKey(
                name: "FK_TracesTypes_Pictures_PictureID",
                table: "TracesTypes",
                column: "PictureID",
                principalTable: "Pictures",
                principalColumn: "PictureID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TracesTypes_Pictures_PictureID",
                table: "TracesTypes");

            migrationBuilder.DropIndex(
                name: "IX_TracesTypes_PictureID",
                table: "TracesTypes");

            migrationBuilder.DropColumn(
                name: "PictureID",
                table: "TracesTypes");
        }
    }
}
