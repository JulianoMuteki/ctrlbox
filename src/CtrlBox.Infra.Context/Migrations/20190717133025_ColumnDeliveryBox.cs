using Microsoft.EntityFrameworkCore.Migrations;

namespace CtrlBox.Infra.Context.Migrations
{
    public partial class ColumnDeliveryBox : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveriesBoxes_Deliveries_BoxID",
                table: "DeliveriesBoxes");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveriesBoxes_DeliveryID",
                table: "DeliveriesBoxes",
                column: "DeliveryID");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveriesBoxes_Deliveries_DeliveryID",
                table: "DeliveriesBoxes",
                column: "DeliveryID",
                principalTable: "Deliveries",
                principalColumn: "DeliveryID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveriesBoxes_Deliveries_DeliveryID",
                table: "DeliveriesBoxes");

            migrationBuilder.DropIndex(
                name: "IX_DeliveriesBoxes_DeliveryID",
                table: "DeliveriesBoxes");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveriesBoxes_Deliveries_BoxID",
                table: "DeliveriesBoxes",
                column: "BoxID",
                principalTable: "Deliveries",
                principalColumn: "DeliveryID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
