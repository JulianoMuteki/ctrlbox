using Microsoft.EntityFrameworkCore.Migrations;

namespace CtrlBox.Infra.Context.Migrations
{
    public partial class CorrectMapStock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Stocks_ClientID",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_ProductID",
                table: "Stocks");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_ClientID",
                table: "Stocks",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_ProductID",
                table: "Stocks",
                column: "ProductID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Stocks_ClientID",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_ProductID",
                table: "Stocks");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_ClientID",
                table: "Stocks",
                column: "ClientID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_ProductID",
                table: "Stocks",
                column: "ProductID",
                unique: true);
        }
    }
}
