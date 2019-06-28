using Microsoft.EntityFrameworkCore.Migrations;

namespace CtrlBox.Infra.Context.Migrations
{
    public partial class RelationSale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Payments_SaleID",
                table: "Payments");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_SaleID",
                table: "Payments",
                column: "SaleID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Payments_SaleID",
                table: "Payments");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_SaleID",
                table: "Payments",
                column: "SaleID");
        }
    }
}
