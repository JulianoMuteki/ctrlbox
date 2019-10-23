using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CtrlBox.Infra.Context.Migrations
{
    public partial class GraphicCodes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boxes_Products_ProductID",
                table: "Boxes");

            migrationBuilder.DropIndex(
                name: "IX_BoxesBarcodes_BoxID",
                table: "BoxesBarcodes");

            migrationBuilder.DropIndex(
                name: "IX_Boxes_ProductID",
                table: "Boxes");

            migrationBuilder.DropColumn(
                name: "ProductID",
                table: "Boxes");

            migrationBuilder.AddColumn<Guid>(
                name: "GraphicCodeID",
                table: "Boxes",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "GraphicsCodes",
                columns: table => new
                {
                    GraphicCodeID = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsDisable = table.Column<bool>(nullable: false),
                    BarcodeEAN13 = table.Column<string>(maxLength: 13, nullable: false),
                    BarcodeGS1_128 = table.Column<string>(maxLength: 48, nullable: false),
                    RFID = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("GraphicCodeID", x => x.GraphicCodeID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoxesBarcodes_BoxID",
                table: "BoxesBarcodes",
                column: "BoxID");

            migrationBuilder.CreateIndex(
                name: "IX_Boxes_GraphicCodeID",
                table: "Boxes",
                column: "GraphicCodeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Boxes_GraphicsCodes_GraphicCodeID",
                table: "Boxes",
                column: "GraphicCodeID",
                principalTable: "GraphicsCodes",
                principalColumn: "GraphicCodeID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boxes_GraphicsCodes_GraphicCodeID",
                table: "Boxes");

            migrationBuilder.DropTable(
                name: "GraphicsCodes");

            migrationBuilder.DropIndex(
                name: "IX_BoxesBarcodes_BoxID",
                table: "BoxesBarcodes");

            migrationBuilder.DropIndex(
                name: "IX_Boxes_GraphicCodeID",
                table: "Boxes");

            migrationBuilder.DropColumn(
                name: "GraphicCodeID",
                table: "Boxes");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductID",
                table: "Boxes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BoxesBarcodes_BoxID",
                table: "BoxesBarcodes",
                column: "BoxID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Boxes_ProductID",
                table: "Boxes",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_Boxes_Products_ProductID",
                table: "Boxes",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
