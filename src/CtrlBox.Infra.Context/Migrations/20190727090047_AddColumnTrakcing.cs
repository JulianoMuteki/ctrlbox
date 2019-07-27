using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CtrlBox.Infra.Context.Migrations
{
    public partial class AddColumnTrakcing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TracesClients");

            migrationBuilder.DropTable(
                name: "Traceabilities");

            migrationBuilder.DropTable(
                name: "TracesTypes");

            migrationBuilder.CreateTable(
                name: "TrackingsTypes",
                columns: table => new
                {
                    TrackingTypeID = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsDisable = table.Column<bool>(nullable: false),
                    TrackType = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: false),
                    PictureID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("TrackingTypeID", x => x.TrackingTypeID);
                    table.ForeignKey(
                        name: "FK_TrackingsTypes_Pictures_PictureID",
                        column: x => x.PictureID,
                        principalTable: "Pictures",
                        principalColumn: "PictureID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BoxesTrackings",
                columns: table => new
                {
                    BoxTrackingID = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsDisable = table.Column<bool>(nullable: false),
                    ProductItemID = table.Column<Guid>(nullable: true),
                    BoxID = table.Column<Guid>(nullable: true),
                    TrackingTypeID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("BoxTrackingID", x => x.BoxTrackingID);
                    table.ForeignKey(
                        name: "FK_BoxesTrackings_Boxes_BoxID",
                        column: x => x.BoxID,
                        principalTable: "Boxes",
                        principalColumn: "BoxID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BoxesTrackings_ProductItems_ProductItemID",
                        column: x => x.ProductItemID,
                        principalTable: "ProductItems",
                        principalColumn: "ProductItemID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BoxesTrackings_TrackingsTypes_TrackingTypeID",
                        column: x => x.TrackingTypeID,
                        principalTable: "TrackingsTypes",
                        principalColumn: "TrackingTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BoxsTrackingsClients",
                columns: table => new
                {
                    BoxTrackingID = table.Column<Guid>(nullable: false),
                    ClientID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoxsTrackingsClients", x => new { x.ClientID, x.BoxTrackingID });
                    table.ForeignKey(
                        name: "FK_BoxsTrackingsClients_BoxesTrackings_BoxTrackingID",
                        column: x => x.BoxTrackingID,
                        principalTable: "BoxesTrackings",
                        principalColumn: "BoxTrackingID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoxsTrackingsClients_Clients_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Clients",
                        principalColumn: "ClientID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoxesTrackings_BoxID",
                table: "BoxesTrackings",
                column: "BoxID");

            migrationBuilder.CreateIndex(
                name: "IX_BoxesTrackings_ProductItemID",
                table: "BoxesTrackings",
                column: "ProductItemID");

            migrationBuilder.CreateIndex(
                name: "IX_BoxesTrackings_TrackingTypeID",
                table: "BoxesTrackings",
                column: "TrackingTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_BoxsTrackingsClients_BoxTrackingID",
                table: "BoxsTrackingsClients",
                column: "BoxTrackingID");

            migrationBuilder.CreateIndex(
                name: "IX_TrackingsTypes_PictureID",
                table: "TrackingsTypes",
                column: "PictureID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoxsTrackingsClients");

            migrationBuilder.DropTable(
                name: "BoxesTrackings");

            migrationBuilder.DropTable(
                name: "TrackingsTypes");

            migrationBuilder.CreateTable(
                name: "TracesTypes",
                columns: table => new
                {
                    TraceTypeID = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsDisable = table.Column<bool>(nullable: false),
                    PictureID = table.Column<Guid>(nullable: true),
                    TypeTrace = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("TraceTypeID", x => x.TraceTypeID);
                    table.ForeignKey(
                        name: "FK_TracesTypes_Pictures_PictureID",
                        column: x => x.PictureID,
                        principalTable: "Pictures",
                        principalColumn: "PictureID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Traceabilities",
                columns: table => new
                {
                    TraceabilityID = table.Column<Guid>(nullable: false),
                    BoxID = table.Column<Guid>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsDisable = table.Column<bool>(nullable: false),
                    ProductItemID = table.Column<Guid>(nullable: true),
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
                    ClientID = table.Column<Guid>(nullable: false),
                    TraceID = table.Column<Guid>(nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_TracesTypes_PictureID",
                table: "TracesTypes",
                column: "PictureID");
        }
    }
}
