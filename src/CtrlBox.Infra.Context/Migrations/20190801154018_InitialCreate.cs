using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CtrlBox.Infra.Context.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressID = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsDisable = table.Column<bool>(nullable: false),
                    CEP = table.Column<string>(maxLength: 15, nullable: false),
                    Street = table.Column<string>(maxLength: 250, nullable: false),
                    Number = table.Column<string>(maxLength: 15, nullable: false),
                    City = table.Column<string>(maxLength: 50, nullable: false),
                    District = table.Column<string>(maxLength: 250, nullable: false),
                    Estate = table.Column<string>(maxLength: 50, nullable: false),
                    Reference = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("AddressID", x => x.AddressID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OptiontsTypes",
                columns: table => new
                {
                    OptiontTypeMapID = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsDisable = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: false),
                    EClientType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("OptiontTypeMapID", x => x.OptiontTypeMapID);
                });

            migrationBuilder.CreateTable(
                name: "PaymentsMethods",
                columns: table => new
                {
                    PaymentMethodID = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsDisable = table.Column<bool>(nullable: false),
                    MethodName = table.Column<string>(maxLength: 50, nullable: false),
                    Descrition = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PaymentMethodID", x => x.PaymentMethodID);
                });

            migrationBuilder.CreateTable(
                name: "Pictures",
                columns: table => new
                {
                    PictureID = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsDisable = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Data = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Length = table.Column<int>(nullable: false),
                    Width = table.Column<int>(nullable: false),
                    Height = table.Column<int>(nullable: false),
                    ContentType = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PictureID", x => x.PictureID);
                });

            migrationBuilder.CreateTable(
                name: "SystemConfigurations",
                columns: table => new
                {
                    SystemID = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsDisable = table.Column<bool>(nullable: false),
                    CultureInfo = table.Column<string>(maxLength: 6, nullable: false),
                    UnitProduct = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SystemID", x => x.SystemID);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientID = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsDisable = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    Phone = table.Column<string>(maxLength: 250, nullable: false),
                    Contact = table.Column<string>(maxLength: 250, nullable: false),
                    AddressID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ClientID", x => x.ClientID);
                    table.ForeignKey(
                        name: "FK_Clients_Addresses_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Addresses",
                        principalColumn: "AddressID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BoxesTypes",
                columns: table => new
                {
                    BoxTypeID = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsDisable = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: false),
                    Lenght = table.Column<decimal>(type: "numeric(18,3)", nullable: false),
                    Height = table.Column<decimal>(type: "numeric(18,3)", nullable: false),
                    Width = table.Column<decimal>(type: "numeric(18,3)", nullable: false),
                    LengthUnit = table.Column<string>(nullable: true),
                    MaxProductsItems = table.Column<int>(nullable: false),
                    IsReturnable = table.Column<bool>(nullable: false),
                    PictureID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("BoxTypeID", x => x.BoxTypeID);
                    table.ForeignKey(
                        name: "FK_BoxesTypes_Pictures_PictureID",
                        column: x => x.PictureID,
                        principalTable: "Pictures",
                        principalColumn: "PictureID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsDisable = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: false),
                    Package = table.Column<string>(maxLength: 150, nullable: false),
                    Capacity = table.Column<double>(type: "float", nullable: false),
                    UnitMeasure = table.Column<string>(maxLength: 50, nullable: false),
                    UnitType = table.Column<string>(nullable: true),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    MassUnitWeight = table.Column<string>(maxLength: 50, nullable: false),
                    PictureID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ProductID", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_Products_Pictures_PictureID",
                        column: x => x.PictureID,
                        principalTable: "Pictures",
                        principalColumn: "PictureID",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "ClientsOptionsTypes",
                columns: table => new
                {
                    ClientID = table.Column<Guid>(nullable: false),
                    OptiontTypeID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientsOptionsTypes", x => new { x.ClientID, x.OptiontTypeID });
                    table.ForeignKey(
                        name: "FK_ClientsOptionsTypes_Clients_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Clients",
                        principalColumn: "ClientID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientsOptionsTypes_OptiontsTypes_OptiontTypeID",
                        column: x => x.OptiontTypeID,
                        principalTable: "OptiontsTypes",
                        principalColumn: "OptiontTypeMapID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    RouteID = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsDisable = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    KmDistance = table.Column<int>(nullable: false),
                    Truck = table.Column<string>(maxLength: 250, nullable: false),
                    HasOpenDelivery = table.Column<bool>(nullable: false),
                    ClientOriginID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("RouteID", x => x.RouteID);
                    table.ForeignKey(
                        name: "FK_Routes_Clients_ClientOriginID",
                        column: x => x.ClientOriginID,
                        principalTable: "Clients",
                        principalColumn: "ClientID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Boxes",
                columns: table => new
                {
                    BoxID = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsDisable = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: false),
                    Status = table.Column<int>(nullable: false),
                    PorcentFull = table.Column<int>(nullable: false),
                    BoxTypeID = table.Column<Guid>(nullable: false),
                    BoxParentID = table.Column<Guid>(nullable: true),
                    ProductID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("BoxID", x => x.BoxID);
                    table.ForeignKey(
                        name: "FK_Boxes_Boxes_BoxParentID",
                        column: x => x.BoxParentID,
                        principalTable: "Boxes",
                        principalColumn: "BoxID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Boxes_BoxesTypes_BoxTypeID",
                        column: x => x.BoxTypeID,
                        principalTable: "BoxesTypes",
                        principalColumn: "BoxTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Boxes_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
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
                        principalColumn: "ClientID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientsProducts_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductItems",
                columns: table => new
                {
                    ProductItemID = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsDisable = table.Column<bool>(nullable: false),
                    Barcode = table.Column<string>(maxLength: 14, nullable: false),
                    ProductID = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ProductItemID", x => x.ProductItemID);
                    table.ForeignKey(
                        name: "FK_ProductItems_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Deliveries",
                columns: table => new
                {
                    DeliveryID = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsDisable = table.Column<bool>(nullable: false),
                    RouteID = table.Column<Guid>(nullable: false),
                    UserID = table.Column<Guid>(nullable: false),
                    IsFinalized = table.Column<bool>(nullable: false),
                    DtStart = table.Column<DateTime>(nullable: false),
                    DtEnd = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 250, nullable: false),
                    FinalizedBy = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("DeliveryID", x => x.DeliveryID);
                    table.ForeignKey(
                        name: "FK_Deliveries_Routes_RouteID",
                        column: x => x.RouteID,
                        principalTable: "Routes",
                        principalColumn: "RouteID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Deliveries_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
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
                        principalColumn: "ClientID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoutesClients_Routes_RouteID",
                        column: x => x.RouteID,
                        principalTable: "Routes",
                        principalColumn: "RouteID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BoxesBarcodes",
                columns: table => new
                {
                    BoxBarcodeID = table.Column<Guid>(nullable: false),
                    BoxID = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsDisable = table.Column<bool>(nullable: false),
                    BarcodeEAN13 = table.Column<string>(maxLength: 13, nullable: false),
                    BarcodeGS1_128 = table.Column<string>(maxLength: 48, nullable: false),
                    RFID = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoxesBarcodes", x => new { x.BoxBarcodeID, x.BoxID });
                    table.ForeignKey(
                        name: "FK_BoxesBarcodes_Boxes_BoxID",
                        column: x => x.BoxID,
                        principalTable: "Boxes",
                        principalColumn: "BoxID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trackings",
                columns: table => new
                {
                    TrackingID = table.Column<Guid>(nullable: false),
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
                    table.PrimaryKey("TrackingID", x => x.TrackingID);
                    table.ForeignKey(
                        name: "FK_Trackings_Boxes_BoxID",
                        column: x => x.BoxID,
                        principalTable: "Boxes",
                        principalColumn: "BoxID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trackings_ProductItems_ProductItemID",
                        column: x => x.ProductItemID,
                        principalTable: "ProductItems",
                        principalColumn: "ProductItemID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trackings_TrackingsTypes_TrackingTypeID",
                        column: x => x.TrackingTypeID,
                        principalTable: "TrackingsTypes",
                        principalColumn: "TrackingTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BoxesProductItems",
                columns: table => new
                {
                    BoxID = table.Column<Guid>(nullable: false),
                    ProductItemID = table.Column<Guid>(nullable: false),
                    IsDelivered = table.Column<bool>(nullable: false),
                    IsItemRemovedBox = table.Column<bool>(nullable: false),
                    OrderID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoxesProductItems", x => new { x.BoxID, x.ProductItemID });
                    table.ForeignKey(
                        name: "FK_BoxesProductItems_Boxes_BoxID",
                        column: x => x.BoxID,
                        principalTable: "Boxes",
                        principalColumn: "BoxID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoxesProductItems_Deliveries_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Deliveries",
                        principalColumn: "DeliveryID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BoxesProductItems_ProductItems_ProductItemID",
                        column: x => x.ProductItemID,
                        principalTable: "ProductItems",
                        principalColumn: "ProductItemID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeliveriesDetails",
                columns: table => new
                {
                    DeliveryDetailID = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsDisable = table.Column<bool>(nullable: false),
                    ProductID = table.Column<Guid>(nullable: false),
                    QuantityProductItem = table.Column<int>(nullable: false),
                    OrderID = table.Column<Guid>(nullable: false),
                    ClientID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("DeliveryDetailID", x => x.DeliveryDetailID);
                    table.ForeignKey(
                        name: "FK_DeliveriesDetails_Clients_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Clients",
                        principalColumn: "ClientID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeliveriesDetails_Deliveries_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Deliveries",
                        principalColumn: "DeliveryID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeliveriesDetails_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    ExpenseID = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsDisable = table.Column<bool>(nullable: false),
                    OrderID = table.Column<Guid>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ExpenseID", x => x.ExpenseID);
                    table.ForeignKey(
                        name: "FK_Expenses_Deliveries_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Deliveries",
                        principalColumn: "DeliveryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrdersBoxes",
                columns: table => new
                {
                    BoxID = table.Column<Guid>(nullable: false),
                    OrderID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersBoxes", x => new { x.OrderID, x.BoxID });
                    table.ForeignKey(
                        name: "FK_OrdersBoxes_Boxes_BoxID",
                        column: x => x.BoxID,
                        principalTable: "Boxes",
                        principalColumn: "BoxID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdersBoxes_Deliveries_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Deliveries",
                        principalColumn: "DeliveryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    SaleID = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsDisable = table.Column<bool>(nullable: false),
                    ClientID = table.Column<Guid>(nullable: false),
                    DeliveryID = table.Column<Guid>(nullable: false),
                    ReceivedValue = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ForwardValue = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    IsFinished = table.Column<bool>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SaleID", x => x.SaleID);
                    table.ForeignKey(
                        name: "FK_Sales_Clients_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Clients",
                        principalColumn: "ClientID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sales_Deliveries_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Deliveries",
                        principalColumn: "DeliveryID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrackingsClients",
                columns: table => new
                {
                    TrackingID = table.Column<Guid>(nullable: false),
                    ClientID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackingsClients", x => new { x.ClientID, x.TrackingID });
                    table.ForeignKey(
                        name: "FK_TrackingsClients_Clients_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Clients",
                        principalColumn: "ClientID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrackingsClients_Trackings_TrackingID",
                        column: x => x.TrackingID,
                        principalTable: "Trackings",
                        principalColumn: "TrackingID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeliveriesBoxes",
                columns: table => new
                {
                    BoxID = table.Column<Guid>(nullable: false),
                    DeliveryDetailID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveriesBoxes", x => new { x.DeliveryDetailID, x.BoxID });
                    table.ForeignKey(
                        name: "FK_DeliveriesBoxes_Boxes_BoxID",
                        column: x => x.BoxID,
                        principalTable: "Boxes",
                        principalColumn: "BoxID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeliveriesBoxes_DeliveriesDetails_DeliveryDetailID",
                        column: x => x.DeliveryDetailID,
                        principalTable: "DeliveriesDetails",
                        principalColumn: "DeliveryDetailID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentID = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsDisable = table.Column<bool>(nullable: false),
                    TotalValueSale = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    RemainingValue = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    PaymentDate = table.Column<DateTime>(nullable: false),
                    IsPaid = table.Column<bool>(nullable: false),
                    IsCashPayment = table.Column<bool>(nullable: false),
                    NumberParcels = table.Column<int>(nullable: false),
                    SaleID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PaymentID", x => x.PaymentID);
                    table.ForeignKey(
                        name: "FK_Payments_Sales_SaleID",
                        column: x => x.SaleID,
                        principalTable: "Sales",
                        principalColumn: "SaleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesProducts",
                columns: table => new
                {
                    SaleID = table.Column<Guid>(nullable: false),
                    ProductID = table.Column<Guid>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    ValueProductSale = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    DiscountValueSale = table.Column<int>(nullable: false),
                    TotalValue = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesProducts", x => new { x.ProductID, x.SaleID });
                    table.ForeignKey(
                        name: "FK_SalesProducts_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalesProducts_Sales_SaleID",
                        column: x => x.SaleID,
                        principalTable: "Sales",
                        principalColumn: "SaleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentSchedules",
                columns: table => new
                {
                    PaymentScheduleID = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsDisable = table.Column<bool>(nullable: false),
                    BenefitValue = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ExprireDate = table.Column<DateTime>(nullable: false),
                    RealizedDate = table.Column<DateTime>(nullable: true),
                    PaymentID = table.Column<Guid>(nullable: false),
                    PaymentMethodID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PaymentScheduleID", x => x.PaymentScheduleID);
                    table.ForeignKey(
                        name: "FK_PaymentSchedules_Payments_PaymentID",
                        column: x => x.PaymentID,
                        principalTable: "Payments",
                        principalColumn: "PaymentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentSchedules_PaymentsMethods_PaymentMethodID",
                        column: x => x.PaymentMethodID,
                        principalTable: "PaymentsMethods",
                        principalColumn: "PaymentMethodID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Boxes_BoxParentID",
                table: "Boxes",
                column: "BoxParentID");

            migrationBuilder.CreateIndex(
                name: "IX_Boxes_BoxTypeID",
                table: "Boxes",
                column: "BoxTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Boxes_ProductID",
                table: "Boxes",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_BoxesBarcodes_BoxID",
                table: "BoxesBarcodes",
                column: "BoxID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BoxesProductItems_OrderID",
                table: "BoxesProductItems",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_BoxesProductItems_ProductItemID",
                table: "BoxesProductItems",
                column: "ProductItemID");

            migrationBuilder.CreateIndex(
                name: "IX_BoxesTypes_PictureID",
                table: "BoxesTypes",
                column: "PictureID");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_AddressID",
                table: "Clients",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_ClientsOptionsTypes_OptiontTypeID",
                table: "ClientsOptionsTypes",
                column: "OptiontTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ClientsProducts_ProductID",
                table: "ClientsProducts",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_RouteID",
                table: "Deliveries",
                column: "RouteID");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_UserID",
                table: "Deliveries",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveriesBoxes_BoxID",
                table: "DeliveriesBoxes",
                column: "BoxID");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveriesDetails_ClientID",
                table: "DeliveriesDetails",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveriesDetails_OrderID",
                table: "DeliveriesDetails",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveriesDetails_ProductID",
                table: "DeliveriesDetails",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_OrderID",
                table: "Expenses",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersBoxes_BoxID",
                table: "OrdersBoxes",
                column: "BoxID");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_SaleID",
                table: "Payments",
                column: "SaleID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentSchedules_PaymentID",
                table: "PaymentSchedules",
                column: "PaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentSchedules_PaymentMethodID",
                table: "PaymentSchedules",
                column: "PaymentMethodID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductItems_ProductID",
                table: "ProductItems",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_PictureID",
                table: "Products",
                column: "PictureID");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_ClientOriginID",
                table: "Routes",
                column: "ClientOriginID");

            migrationBuilder.CreateIndex(
                name: "IX_RoutesClients_RouteID",
                table: "RoutesClients",
                column: "RouteID");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_ClientID",
                table: "Sales",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_OrderId",
                table: "Sales",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesProducts_SaleID",
                table: "SalesProducts",
                column: "SaleID");

            migrationBuilder.CreateIndex(
                name: "IX_Trackings_BoxID",
                table: "Trackings",
                column: "BoxID");

            migrationBuilder.CreateIndex(
                name: "IX_Trackings_ProductItemID",
                table: "Trackings",
                column: "ProductItemID");

            migrationBuilder.CreateIndex(
                name: "IX_Trackings_TrackingTypeID",
                table: "Trackings",
                column: "TrackingTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_TrackingsClients_TrackingID",
                table: "TrackingsClients",
                column: "TrackingID");

            migrationBuilder.CreateIndex(
                name: "IX_TrackingsTypes_PictureID",
                table: "TrackingsTypes",
                column: "PictureID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BoxesBarcodes");

            migrationBuilder.DropTable(
                name: "BoxesProductItems");

            migrationBuilder.DropTable(
                name: "ClientsOptionsTypes");

            migrationBuilder.DropTable(
                name: "ClientsProducts");

            migrationBuilder.DropTable(
                name: "DeliveriesBoxes");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "OrdersBoxes");

            migrationBuilder.DropTable(
                name: "PaymentSchedules");

            migrationBuilder.DropTable(
                name: "RoutesClients");

            migrationBuilder.DropTable(
                name: "SalesProducts");

            migrationBuilder.DropTable(
                name: "SystemConfigurations");

            migrationBuilder.DropTable(
                name: "TrackingsClients");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "OptiontsTypes");

            migrationBuilder.DropTable(
                name: "DeliveriesDetails");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "PaymentsMethods");

            migrationBuilder.DropTable(
                name: "Trackings");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Boxes");

            migrationBuilder.DropTable(
                name: "ProductItems");

            migrationBuilder.DropTable(
                name: "TrackingsTypes");

            migrationBuilder.DropTable(
                name: "Deliveries");

            migrationBuilder.DropTable(
                name: "BoxesTypes");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Routes");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Pictures");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
