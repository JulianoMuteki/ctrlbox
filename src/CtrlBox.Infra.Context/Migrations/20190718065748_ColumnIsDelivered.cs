using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CtrlBox.Infra.Context.Migrations
{
    public partial class ColumnIsDelivered : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DeliveryID",
                table: "BoxesProductItems",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelivered",
                table: "BoxesProductItems",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_BoxesProductItems_DeliveryID",
                table: "BoxesProductItems",
                column: "DeliveryID");

            migrationBuilder.AddForeignKey(
                name: "FK_BoxesProductItems_Deliveries_DeliveryID",
                table: "BoxesProductItems",
                column: "DeliveryID",
                principalTable: "Deliveries",
                principalColumn: "DeliveryID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoxesProductItems_Deliveries_DeliveryID",
                table: "BoxesProductItems");

            migrationBuilder.DropIndex(
                name: "IX_BoxesProductItems_DeliveryID",
                table: "BoxesProductItems");

            migrationBuilder.DropColumn(
                name: "DeliveryID",
                table: "BoxesProductItems");

            migrationBuilder.DropColumn(
                name: "IsDelivered",
                table: "BoxesProductItems");
        }
    }
}
