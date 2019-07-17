using Microsoft.EntityFrameworkCore.Migrations;

namespace CtrlBox.Infra.Context.Migrations
{
    public partial class ChangeColumnBoxParent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boxes_Boxes_BoxChildID",
                table: "Boxes");

            migrationBuilder.RenameColumn(
                name: "BoxChildID",
                table: "Boxes",
                newName: "BoxParentID");

            migrationBuilder.RenameIndex(
                name: "IX_Boxes_BoxChildID",
                table: "Boxes",
                newName: "IX_Boxes_BoxParentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Boxes_Boxes_BoxParentID",
                table: "Boxes",
                column: "BoxParentID",
                principalTable: "Boxes",
                principalColumn: "BoxID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boxes_Boxes_BoxParentID",
                table: "Boxes");

            migrationBuilder.RenameColumn(
                name: "BoxParentID",
                table: "Boxes",
                newName: "BoxChildID");

            migrationBuilder.RenameIndex(
                name: "IX_Boxes_BoxParentID",
                table: "Boxes",
                newName: "IX_Boxes_BoxChildID");

            migrationBuilder.AddForeignKey(
                name: "FK_Boxes_Boxes_BoxChildID",
                table: "Boxes",
                column: "BoxChildID",
                principalTable: "Boxes",
                principalColumn: "BoxID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
