using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CtrlBox.Infra.Context.Migrations
{
    public partial class EntityCategoryClient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryID = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsDisable = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CategoryID", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "ClientsCategories",
                columns: table => new
                {
                    ClientID = table.Column<Guid>(nullable: false),
                    CategoryID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientsCategories", x => new { x.ClientID, x.CategoryID });
                    table.ForeignKey(
                        name: "FK_ClientsCategories_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientsCategories_Clients_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Clients",
                        principalColumn: "ClientID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientsCategories_CategoryID",
                table: "ClientsCategories",
                column: "CategoryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientsCategories");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
