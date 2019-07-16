using Microsoft.EntityFrameworkCore.Migrations;

namespace CtrlBox.Infra.Context.Migrations
{
    public partial class OptionalProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoadBoxes_Products_ProductId",
                table: "LoadBoxes");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "LoadBoxes",
                newName: "ProductID");

            migrationBuilder.RenameIndex(
                name: "IX_LoadBoxes_ProductId",
                table: "LoadBoxes",
                newName: "IX_LoadBoxes_ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_LoadBoxes_Products_ProductID",
                table: "LoadBoxes",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoadBoxes_Products_ProductID",
                table: "LoadBoxes");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "LoadBoxes",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_LoadBoxes_ProductID",
                table: "LoadBoxes",
                newName: "IX_LoadBoxes_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoadBoxes_Products_ProductId",
                table: "LoadBoxes",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
