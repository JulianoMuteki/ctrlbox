using Microsoft.EntityFrameworkCore.Migrations;

namespace CtrlBox.Infra.Context.Migrations
{
    public partial class NewColumnDiscount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalReturnedBoxes",
                table: "Sales");

            migrationBuilder.RenameColumn(
                name: "ExchangeQuantity",
                table: "SalesProducts",
                newName: "DiscountAmount");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DiscountAmount",
                table: "SalesProducts",
                newName: "ExchangeQuantity");

            migrationBuilder.AddColumn<int>(
                name: "TotalReturnedBoxes",
                table: "Sales",
                nullable: false,
                defaultValue: 0);
        }
    }
}
