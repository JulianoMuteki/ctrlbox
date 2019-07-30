using Microsoft.EntityFrameworkCore.Migrations;

namespace CtrlBox.Infra.Context.Migrations
{
    public partial class FixForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientsOptionsTypes_Clients_OptiontTypeID",
                table: "ClientsOptionsTypes");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientsOptionsTypes_Clients_ClientID",
                table: "ClientsOptionsTypes",
                column: "ClientID",
                principalTable: "Clients",
                principalColumn: "ClientID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientsOptionsTypes_Clients_ClientID",
                table: "ClientsOptionsTypes");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientsOptionsTypes_Clients_OptiontTypeID",
                table: "ClientsOptionsTypes",
                column: "OptiontTypeID",
                principalTable: "Clients",
                principalColumn: "ClientID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
