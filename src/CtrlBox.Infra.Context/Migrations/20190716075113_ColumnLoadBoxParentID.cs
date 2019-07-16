using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CtrlBox.Infra.Context.Migrations
{
    public partial class ColumnLoadBoxParentID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LoadBoxParentID",
                table: "LoadBoxes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoadBoxes_LoadBoxParentID",
                table: "LoadBoxes",
                column: "LoadBoxParentID");

            migrationBuilder.AddForeignKey(
                name: "FK_LoadBoxes_LoadBoxes_LoadBoxParentID",
                table: "LoadBoxes",
                column: "LoadBoxParentID",
                principalTable: "LoadBoxes",
                principalColumn: "LoadBoxID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoadBoxes_LoadBoxes_LoadBoxParentID",
                table: "LoadBoxes");

            migrationBuilder.DropIndex(
                name: "IX_LoadBoxes_LoadBoxParentID",
                table: "LoadBoxes");

            migrationBuilder.DropColumn(
                name: "LoadBoxParentID",
                table: "LoadBoxes");
        }
    }
}
