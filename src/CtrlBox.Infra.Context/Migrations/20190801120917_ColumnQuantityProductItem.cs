using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CtrlBox.Infra.Context.Migrations
{
    public partial class ColumnQuantityProductItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveriesProducts_Sales_SaleId",
                table: "DeliveriesProducts");

            migrationBuilder.DropIndex(
                name: "IX_DeliveriesProducts_SaleId",
                table: "DeliveriesProducts");

            migrationBuilder.DropColumn(
                name: "SaleId",
                table: "DeliveriesProducts");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "DeliveriesProducts",
                newName: "QuantityProductItem");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuantityProductItem",
                table: "DeliveriesProducts",
                newName: "Amount");

            migrationBuilder.AddColumn<Guid>(
                name: "SaleId",
                table: "DeliveriesProducts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeliveriesProducts_SaleId",
                table: "DeliveriesProducts",
                column: "SaleId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveriesProducts_Sales_SaleId",
                table: "DeliveriesProducts",
                column: "SaleId",
                principalTable: "Sales",
                principalColumn: "SaleID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
