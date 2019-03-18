using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CtrlBox.Infra.Context.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    QuantityBoxes = table.Column<int>(nullable: false),
                    BalanceDue = table.Column<float>(type: "float", nullable: false)

                },
                constraints: table =>
                {
                    table.PrimaryKey("ClientID", x => x.Id);
                });


            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: false),
                    Weight = table.Column<float>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ProductID", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    KmDistance = table.Column<int>(nullable: false),
                    Truck = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("RouteID", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    AmountBoxes = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("StockID", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientsProducts",
                columns: table => new
                {
                    ClientID = table.Column<Guid>(nullable: false),
                    ProductID = table.Column<Guid>(nullable: false),
                    Price = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientsProducts", x => new { x.ClientID, x.ProductID });
                    table.ForeignKey(
                        name: "FK_ClientsProducts_Clients_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientsProducts_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Deliveries",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    RouteID = table.Column<Guid>(nullable: false),
                    IsFinalized = table.Column<bool>(nullable: false),
                    DtStart = table.Column<DateTime>(nullable: false),
                    DtEnd = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 250, nullable: false),
                    FinalizedBy = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("DeliveryID", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deliveries_Routes_RouteID",
                        column: x => x.RouteID,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoutesClients",
                columns: table => new
                {
                    RouteID = table.Column<Guid>(nullable: false),
                    ClientID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoutesClients", x => new { x.ClientID, x.RouteID });
                    table.ForeignKey(
                        name: "FK_RoutesClients_Clients_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoutesClients_Routes_RouteID",
                        column: x => x.RouteID,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StocksProducts",
                columns: table => new
                {
                    StockID = table.Column<Guid>(nullable: false),
                    ProductID = table.Column<Guid>(nullable: false),
                    Amount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StocksProducts", x => new { x.ProductID, x.StockID });
                    table.ForeignKey(
                        name: "FK_StocksProducts_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StocksProducts_Stocks_StockID",
                        column: x => x.StockID,
                        principalTable: "Stocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    DeliveryID = table.Column<Guid>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ExpenseID", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_Deliveries_DeliveryID",
                        column: x => x.DeliveryID,
                        principalTable: "Deliveries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    ClientID = table.Column<Guid>(nullable: false),
                    DeliveryID = table.Column<Guid>(nullable: false),
                    ReceivedValue = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    ForwardValue = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    TotalReturnedBoxes = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SaleID", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sales_Clients_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sales_Deliveries_DeliveryID",
                        column: x => x.DeliveryID,
                        principalTable: "Deliveries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Checks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    SaleID = table.Column<Guid>(nullable: false),
                    Number = table.Column<int>(nullable: false),
                    Value = table.Column<float>(nullable: false),
                    DtExpire = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CheckID", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Checks_Sales_SaleID",
                        column: x => x.SaleID,
                        principalTable: "Sales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeliveriesProducts",
                columns: table => new
                {
                    DeliveryID = table.Column<Guid>(nullable: false),
                    ProductID = table.Column<Guid>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    SaleId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveriesProducts", x => new { x.DeliveryID, x.ProductID });
                    table.ForeignKey(
                        name: "FK_DeliveriesProducts_Deliveries_DeliveryID",
                        column: x => x.DeliveryID,
                        principalTable: "Deliveries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeliveriesProducts_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeliveriesProducts_Sales_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Sales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalesProducts",
                columns: table => new
                {
                    SaleID = table.Column<Guid>(nullable: false),
                    ProductID = table.Column<Guid>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    SaleValue = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    ExchangeQuantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesProducts", x => new { x.ProductID, x.SaleID });
                    table.ForeignKey(
                        name: "FK_SalesProducts_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalesProducts_Sales_SaleID",
                        column: x => x.SaleID,
                        principalTable: "Sales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Checks_SaleID",
                table: "Checks",
                column: "SaleID");

            migrationBuilder.CreateIndex(
                name: "IX_ClientsProducts_ProductID",
                table: "ClientsProducts",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_RouteID",
                table: "Deliveries",
                column: "RouteID");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveriesProducts_ProductID",
                table: "DeliveriesProducts",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveriesProducts_SaleId",
                table: "DeliveriesProducts",
                column: "SaleId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_DeliveryID",
                table: "Expenses",
                column: "DeliveryID");

            migrationBuilder.CreateIndex(
                name: "IX_RoutesClients_RouteID",
                table: "RoutesClients",
                column: "RouteID");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_ClientID",
                table: "Sales",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_DeliveryID",
                table: "Sales",
                column: "DeliveryID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesProducts_SaleID",
                table: "SalesProducts",
                column: "SaleID");

            migrationBuilder.CreateIndex(
                name: "IX_StocksProducts_StockID",
                table: "StocksProducts",
                column: "StockID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Checks");

            migrationBuilder.DropTable(
                name: "ClientsProducts");

            migrationBuilder.DropTable(
                name: "DeliveriesProducts");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "RoutesClients");

            migrationBuilder.DropTable(
                name: "SalesProducts");

            migrationBuilder.DropTable(
                name: "StocksProducts");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Deliveries");

            migrationBuilder.DropTable(
                name: "Routes");
        }
    }
}
