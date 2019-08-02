using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CtrlBox.Infra.Context.Migrations
{
    public partial class ChangeDeliveryIDForOrderID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Deliveries_OrderId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "DeliveryID",
                table: "Sales");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Sales",
                newName: "OrderID");

            migrationBuilder.RenameIndex(
                name: "IX_Sales_OrderId",
                table: "Sales",
                newName: "IX_Sales_OrderID");

            migrationBuilder.AlterColumn<Guid>(
                name: "OrderID",
                table: "Sales",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Deliveries_OrderID",
                table: "Sales",
                column: "OrderID",
                principalTable: "Deliveries",
                principalColumn: "DeliveryID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Deliveries_OrderID",
                table: "Sales");

            migrationBuilder.RenameColumn(
                name: "OrderID",
                table: "Sales",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Sales_OrderID",
                table: "Sales",
                newName: "IX_Sales_OrderId");

            migrationBuilder.AlterColumn<Guid>(
                name: "OrderId",
                table: "Sales",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "DeliveryID",
                table: "Sales",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Deliveries_OrderId",
                table: "Sales",
                column: "OrderId",
                principalTable: "Deliveries",
                principalColumn: "DeliveryID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
