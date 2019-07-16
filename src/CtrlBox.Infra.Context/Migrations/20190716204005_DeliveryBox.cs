using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CtrlBox.Infra.Context.Migrations
{
    public partial class DeliveryBox : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusBox",
                table: "Boxes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DeliveriesBoxes",
                columns: table => new
                {
                    BoxID = table.Column<Guid>(nullable: false),
                    DeliveryID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveriesBoxes", x => new { x.BoxID, x.DeliveryID });
                    table.ForeignKey(
                        name: "FK_DeliveriesBoxes_Boxes_BoxID",
                        column: x => x.BoxID,
                        principalTable: "Boxes",
                        principalColumn: "BoxID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeliveriesBoxes_Deliveries_BoxID",
                        column: x => x.BoxID,
                        principalTable: "Deliveries",
                        principalColumn: "DeliveryID",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliveriesBoxes");

            migrationBuilder.DropColumn(
                name: "StatusBox",
                table: "Boxes");
        }
    }
}
