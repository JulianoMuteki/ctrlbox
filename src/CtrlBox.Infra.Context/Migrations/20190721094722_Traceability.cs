using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CtrlBox.Infra.Context.Migrations
{
    public partial class Traceability : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TracesTypes",
                columns: table => new
                {
                    TraceTypeID = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsDisable = table.Column<bool>(nullable: false),
                    TypeTrace = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("TraceTypeID", x => x.TraceTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Traceabilities",
                columns: table => new
                {
                    TraceabilityID = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsDisable = table.Column<bool>(nullable: false),
                    ProductItemID = table.Column<Guid>(nullable: true),
                    BoxID = table.Column<Guid>(nullable: true),
                    TraceTypeID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("TraceabilityID", x => x.TraceabilityID);
                    table.ForeignKey(
                        name: "FK_Traceabilities_Boxes_BoxID",
                        column: x => x.BoxID,
                        principalTable: "Boxes",
                        principalColumn: "BoxID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Traceabilities_ProductItems_ProductItemID",
                        column: x => x.ProductItemID,
                        principalTable: "ProductItems",
                        principalColumn: "ProductItemID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Traceabilities_TracesTypes_TraceTypeID",
                        column: x => x.TraceTypeID,
                        principalTable: "TracesTypes",
                        principalColumn: "TraceTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TracesClients",
                columns: table => new
                {
                    TraceID = table.Column<Guid>(nullable: false),
                    ClientID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TracesClients", x => new { x.ClientID, x.TraceID });
                    table.ForeignKey(
                        name: "FK_TracesClients_Clients_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Clients",
                        principalColumn: "ClientID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TracesClients_Traceabilities_TraceID",
                        column: x => x.TraceID,
                        principalTable: "Traceabilities",
                        principalColumn: "TraceabilityID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Traceabilities_BoxID",
                table: "Traceabilities",
                column: "BoxID");

            migrationBuilder.CreateIndex(
                name: "IX_Traceabilities_ProductItemID",
                table: "Traceabilities",
                column: "ProductItemID");

            migrationBuilder.CreateIndex(
                name: "IX_Traceabilities_TraceTypeID",
                table: "Traceabilities",
                column: "TraceTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_TracesClients_TraceID",
                table: "TracesClients",
                column: "TraceID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TracesClients");

            migrationBuilder.DropTable(
                name: "Traceabilities");

            migrationBuilder.DropTable(
                name: "TracesTypes");
        }
    }
}
