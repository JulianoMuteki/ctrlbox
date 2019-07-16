﻿// <auto-generated />
using System;
using CtrlBox.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CtrlBox.Infra.Context.Migrations
{
    [DbContext(typeof(CtrlBoxContext))]
    partial class CtrlBoxContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CtrlBox.Domain.Entities.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("AddressID");

                    b.Property<string>("CEP")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("CreationDate");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<string>("Estate")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<bool>("IsDelete");

                    b.Property<bool>("IsDisable");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<string>("Reference")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.HasKey("Id")
                        .HasName("AddressID");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("CtrlBox.Domain.Entities.Box", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("BoxID");

                    b.Property<string>("Barcode")
                        .IsRequired()
                        .HasMaxLength(14);

                    b.Property<Guid?>("BoxChildID");

                    b.Property<Guid>("BoxTypeID");

                    b.Property<DateTime>("CreationDate");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<bool>("IsDelete");

                    b.Property<bool>("IsDisable");

                    b.Property<Guid?>("ProductID");

                    b.HasKey("Id")
                        .HasName("BoxID");

                    b.HasIndex("BoxChildID");

                    b.HasIndex("BoxTypeID");

                    b.HasIndex("ProductID");

                    b.ToTable("Boxes");
                });

            modelBuilder.Entity("CtrlBox.Domain.Entities.BoxProductItem", b =>
                {
                    b.Property<Guid>("BoxID");

                    b.Property<Guid>("ProductItemID");

                    b.HasKey("BoxID", "ProductItemID");

                    b.HasIndex("ProductItemID");

                    b.ToTable("BoxesProductItems");
                });

            modelBuilder.Entity("CtrlBox.Domain.Entities.BoxType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("BoxTypeID");

                    b.Property<DateTime>("CreationDate");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<bool>("IsDelete");

                    b.Property<bool>("IsDisable");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id")
                        .HasName("BoxTypeID");

                    b.ToTable("BoxesTypes");
                });

            modelBuilder.Entity("CtrlBox.Domain.Entities.Check", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CheckID");

                    b.Property<DateTime>("CreationDate");

                    b.Property<DateTime>("DateModified");

                    b.Property<DateTime?>("DtExpire");

                    b.Property<bool>("IsDelete");

                    b.Property<bool>("IsDisable");

                    b.Property<int>("Number");

                    b.Property<Guid>("SaleID");

                    b.Property<float>("Value");

                    b.HasKey("Id")
                        .HasName("CheckID");

                    b.HasIndex("SaleID");

                    b.ToTable("Checks");
                });

            modelBuilder.Entity("CtrlBox.Domain.Entities.Client", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ClientID");

                    b.Property<Guid>("AddressID");

                    b.Property<double>("BalanceDue")
                        .HasColumnType("float");

                    b.Property<string>("Contact")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<DateTime>("CreationDate");

                    b.Property<DateTime>("DateModified");

                    b.Property<bool>("IsDelete");

                    b.Property<bool>("IsDisable");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<int>("QuantityBoxes");

                    b.HasKey("Id")
                        .HasName("ClientID");

                    b.HasIndex("AddressID");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("CtrlBox.Domain.Entities.ClientProductValue", b =>
                {
                    b.Property<Guid>("ClientID");

                    b.Property<Guid>("ProductID");

                    b.Property<float>("Price");

                    b.HasKey("ClientID", "ProductID");

                    b.HasIndex("ProductID");

                    b.ToTable("ClientsProducts");
                });

            modelBuilder.Entity("CtrlBox.Domain.Entities.Delivery", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("DeliveryID");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<DateTime>("CreationDate");

                    b.Property<DateTime>("DateModified");

                    b.Property<DateTime?>("DtEnd");

                    b.Property<DateTime>("DtStart");

                    b.Property<string>("FinalizedBy")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<bool>("IsDelete");

                    b.Property<bool>("IsDisable");

                    b.Property<bool>("IsFinalized");

                    b.Property<Guid>("RouteID");

                    b.Property<Guid>("UserID");

                    b.HasKey("Id")
                        .HasName("DeliveryID");

                    b.HasIndex("RouteID");

                    b.HasIndex("UserID");

                    b.ToTable("Deliveries");
                });

            modelBuilder.Entity("CtrlBox.Domain.Entities.DeliveryProduct", b =>
                {
                    b.Property<Guid>("DeliveryID");

                    b.Property<Guid>("ProductID");

                    b.Property<int>("Amount");

                    b.Property<Guid?>("SaleId");

                    b.HasKey("DeliveryID", "ProductID");

                    b.HasIndex("ProductID");

                    b.HasIndex("SaleId");

                    b.ToTable("DeliveriesProducts");
                });

            modelBuilder.Entity("CtrlBox.Domain.Entities.Expense", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ExpenseID");

                    b.Property<DateTime>("CreationDate");

                    b.Property<DateTime>("DateModified");

                    b.Property<Guid>("DeliveryID");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<bool>("IsDelete");

                    b.Property<bool>("IsDisable");

                    b.Property<double>("Value");

                    b.HasKey("Id")
                        .HasName("ExpenseID");

                    b.HasIndex("DeliveryID");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("CtrlBox.Domain.Entities.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("PaymentID");

                    b.Property<DateTime>("CreationDate");

                    b.Property<DateTime>("DateModified");

                    b.Property<bool>("IsCashPayment");

                    b.Property<bool>("IsDelete");

                    b.Property<bool>("IsDisable");

                    b.Property<bool>("IsPaid");

                    b.Property<int>("NumberParcels");

                    b.Property<DateTime>("PaymentDate");

                    b.Property<decimal>("RemainingValue")
                        .HasColumnType("decimal(10,2)");

                    b.Property<Guid>("SaleID");

                    b.Property<decimal>("TotalValueSale")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("Id")
                        .HasName("PaymentID");

                    b.HasIndex("SaleID")
                        .IsUnique();

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("CtrlBox.Domain.Entities.PaymentMethod", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("PaymentMethodID");

                    b.Property<DateTime>("CreationDate");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("Descrition")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<bool>("IsDelete");

                    b.Property<bool>("IsDisable");

                    b.Property<string>("MethodName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id")
                        .HasName("PaymentMethodID");

                    b.ToTable("PaymentsMethods");
                });

            modelBuilder.Entity("CtrlBox.Domain.Entities.PaymentSchedule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("PaymentScheduleID");

                    b.Property<decimal>("BenefitValue")
                        .HasColumnType("decimal(10,2)");

                    b.Property<DateTime>("CreationDate");

                    b.Property<DateTime>("DateModified");

                    b.Property<DateTime>("ExprireDate");

                    b.Property<bool>("IsDelete");

                    b.Property<bool>("IsDisable");

                    b.Property<Guid>("PaymentID");

                    b.Property<Guid>("PaymentMethodID");

                    b.Property<DateTime?>("RealizedDate");

                    b.HasKey("Id")
                        .HasName("PaymentScheduleID");

                    b.HasIndex("PaymentID");

                    b.HasIndex("PaymentMethodID");

                    b.ToTable("PaymentSchedules");
                });

            modelBuilder.Entity("CtrlBox.Domain.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ProductID");

                    b.Property<DateTime>("CreationDate");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<bool>("IsDelete");

                    b.Property<bool>("IsDisable");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<string>("UnitMeasure")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id")
                        .HasName("ProductID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("CtrlBox.Domain.Entities.ProductItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ProductItemID");

                    b.Property<string>("Barcode")
                        .IsRequired()
                        .HasMaxLength(14);

                    b.Property<DateTime>("CreationDate");

                    b.Property<DateTime>("DateModified");

                    b.Property<bool>("InBox");

                    b.Property<bool>("IsDelete");

                    b.Property<bool>("IsDisable");

                    b.Property<Guid>("ProductID");

                    b.Property<string>("Weight")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id")
                        .HasName("ProductItemID");

                    b.HasIndex("ProductID");

                    b.ToTable("ProductItems");
                });

            modelBuilder.Entity("CtrlBox.Domain.Entities.Route", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("RouteID");

                    b.Property<DateTime>("CreationDate");

                    b.Property<DateTime>("DateModified");

                    b.Property<bool>("HasOpenDelivery");

                    b.Property<bool>("IsDelete");

                    b.Property<bool>("IsDisable");

                    b.Property<int>("KmDistance");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<string>("Truck")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.HasKey("Id")
                        .HasName("RouteID");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("CtrlBox.Domain.Entities.RouteClient", b =>
                {
                    b.Property<Guid>("ClientID");

                    b.Property<Guid>("RouteID");

                    b.HasKey("ClientID", "RouteID");

                    b.HasIndex("RouteID");

                    b.ToTable("RoutesClients");
                });

            modelBuilder.Entity("CtrlBox.Domain.Entities.Sale", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("SaleID");

                    b.Property<Guid>("ClientID");

                    b.Property<DateTime>("CreationDate");

                    b.Property<DateTime>("DateModified");

                    b.Property<Guid>("DeliveryID");

                    b.Property<decimal>("ForwardValue")
                        .HasColumnType("decimal(10,2)");

                    b.Property<bool>("IsDelete");

                    b.Property<bool>("IsDisable");

                    b.Property<bool>("IsFinished");

                    b.Property<decimal>("ReceivedValue")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("Id")
                        .HasName("SaleID");

                    b.HasIndex("ClientID");

                    b.HasIndex("DeliveryID");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("CtrlBox.Domain.Entities.SaleProduct", b =>
                {
                    b.Property<Guid>("ProductID");

                    b.Property<Guid>("SaleID");

                    b.Property<int>("DiscountValueSale");

                    b.Property<int>("Quantity");

                    b.Property<decimal>("TotalValue")
                        .HasColumnType("decimal(10,2)");

                    b.Property<decimal>("ValueProductSale")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("ProductID", "SaleID");

                    b.HasIndex("SaleID");

                    b.ToTable("SalesProducts");
                });

            modelBuilder.Entity("CtrlBox.Domain.Entities.Stock", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("StockID");

                    b.Property<int>("AmountBoxes");

                    b.Property<DateTime>("CreationDate");

                    b.Property<DateTime>("DateModified");

                    b.Property<bool>("IsDelete");

                    b.Property<bool>("IsDisable");

                    b.HasKey("Id")
                        .HasName("StockID");

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("CtrlBox.Domain.Entities.StockProduct", b =>
                {
                    b.Property<Guid>("ProductID");

                    b.Property<Guid>("StockID");

                    b.Property<int>("Amount");

                    b.HasKey("ProductID", "StockID");

                    b.HasIndex("StockID");

                    b.ToTable("StocksProducts");
                });

            modelBuilder.Entity("CtrlBox.Domain.Entities.SystemConfiguration", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("SystemID");

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("CultureInfo")
                        .IsRequired()
                        .HasMaxLength(6);

                    b.Property<DateTime>("DateModified");

                    b.Property<bool>("IsDelete");

                    b.Property<bool>("IsDisable");

                    b.Property<string>("UnitProduct")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id")
                        .HasName("SystemID");

                    b.ToTable("SystemConfigurations");
                });

            modelBuilder.Entity("CtrlBox.Domain.Identity.ApplicationRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("CtrlBox.Domain.Identity.ApplicationRoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("CtrlBox.Domain.Identity.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("CtrlBox.Domain.Identity.ApplicationUserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("CtrlBox.Domain.Identity.ApplicationUserLogin", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<Guid>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("CtrlBox.Domain.Identity.ApplicationUserRole", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("CtrlBox.Domain.Identity.ApplicationUserToken", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("CtrlBox.Domain.Entities.Box", b =>
                {
                    b.HasOne("CtrlBox.Domain.Entities.Box", "BoxChild")
                        .WithMany("BoxesChildren")
                        .HasForeignKey("BoxChildID");

                    b.HasOne("CtrlBox.Domain.Entities.BoxType", "BoxType")
                        .WithMany("Boxes")
                        .HasForeignKey("BoxTypeID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CtrlBox.Domain.Entities.Product", "Product")
                        .WithMany("Boxes")
                        .HasForeignKey("ProductID");
                });

            modelBuilder.Entity("CtrlBox.Domain.Entities.BoxProductItem", b =>
                {
                    b.HasOne("CtrlBox.Domain.Entities.Box", "Box")
                        .WithMany("BoxesProductItems")
                        .HasForeignKey("BoxID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CtrlBox.Domain.Entities.ProductItem", "ProductItem")
                        .WithMany("LoadBoxesProductItems")
                        .HasForeignKey("ProductItemID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CtrlBox.Domain.Entities.Check", b =>
                {
                    b.HasOne("CtrlBox.Domain.Entities.Sale", "Sale")
                        .WithMany("Checks")
                        .HasForeignKey("SaleID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CtrlBox.Domain.Entities.Client", b =>
                {
                    b.HasOne("CtrlBox.Domain.Entities.Address", "Address")
                        .WithMany("Clients")
                        .HasForeignKey("AddressID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CtrlBox.Domain.Entities.ClientProductValue", b =>
                {
                    b.HasOne("CtrlBox.Domain.Entities.Client", "Client")
                        .WithMany("CustomersProductsValues")
                        .HasForeignKey("ClientID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CtrlBox.Domain.Entities.Product", "Product")
                        .WithMany("CustomersProductsValues")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CtrlBox.Domain.Entities.Delivery", b =>
                {
                    b.HasOne("CtrlBox.Domain.Entities.Route", "Route")
                        .WithMany("Deliveries")
                        .HasForeignKey("RouteID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CtrlBox.Domain.Identity.ApplicationUser", "User")
                        .WithMany("Deliveries")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CtrlBox.Domain.Entities.DeliveryProduct", b =>
                {
                    b.HasOne("CtrlBox.Domain.Entities.Delivery", "Delivery")
                        .WithMany("DeliveriesProducts")
                        .HasForeignKey("DeliveryID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CtrlBox.Domain.Entities.Product", "Product")
                        .WithMany("DeliveriesProducts")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CtrlBox.Domain.Entities.Sale")
                        .WithMany("DeliveriesProducts")
                        .HasForeignKey("SaleId");
                });

            modelBuilder.Entity("CtrlBox.Domain.Entities.Expense", b =>
                {
                    b.HasOne("CtrlBox.Domain.Entities.Delivery", "Delivery")
                        .WithMany("Expenses")
                        .HasForeignKey("DeliveryID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CtrlBox.Domain.Entities.Payment", b =>
                {
                    b.HasOne("CtrlBox.Domain.Entities.Sale", "Sale")
                        .WithOne("Payment")
                        .HasForeignKey("CtrlBox.Domain.Entities.Payment", "SaleID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CtrlBox.Domain.Entities.PaymentSchedule", b =>
                {
                    b.HasOne("CtrlBox.Domain.Entities.Payment", "Payment")
                        .WithMany("PaymentsSchedules")
                        .HasForeignKey("PaymentID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CtrlBox.Domain.Entities.PaymentMethod", "PaymentMethod")
                        .WithMany("PaymentsSchedules")
                        .HasForeignKey("PaymentMethodID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CtrlBox.Domain.Entities.ProductItem", b =>
                {
                    b.HasOne("CtrlBox.Domain.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CtrlBox.Domain.Entities.RouteClient", b =>
                {
                    b.HasOne("CtrlBox.Domain.Entities.Client", "Client")
                        .WithMany("RoutesClients")
                        .HasForeignKey("ClientID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CtrlBox.Domain.Entities.Route", "Route")
                        .WithMany("RoutesClients")
                        .HasForeignKey("RouteID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CtrlBox.Domain.Entities.Sale", b =>
                {
                    b.HasOne("CtrlBox.Domain.Entities.Client", "Client")
                        .WithMany("Sales")
                        .HasForeignKey("ClientID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CtrlBox.Domain.Entities.Delivery", "Delivery")
                        .WithMany("Sales")
                        .HasForeignKey("DeliveryID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CtrlBox.Domain.Entities.SaleProduct", b =>
                {
                    b.HasOne("CtrlBox.Domain.Entities.Product", "Product")
                        .WithMany("SalesProducts")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CtrlBox.Domain.Entities.Sale", "Sale")
                        .WithMany("SalesProducts")
                        .HasForeignKey("SaleID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CtrlBox.Domain.Entities.StockProduct", b =>
                {
                    b.HasOne("CtrlBox.Domain.Entities.Product", "Product")
                        .WithMany("StocksProducts")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CtrlBox.Domain.Entities.Stock", "Stock")
                        .WithMany("StocksProducts")
                        .HasForeignKey("StockID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CtrlBox.Domain.Identity.ApplicationRoleClaim", b =>
                {
                    b.HasOne("CtrlBox.Domain.Identity.ApplicationRole", "Role")
                        .WithMany("RoleClaims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CtrlBox.Domain.Identity.ApplicationUserClaim", b =>
                {
                    b.HasOne("CtrlBox.Domain.Identity.ApplicationUser", "User")
                        .WithMany("UserClaims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CtrlBox.Domain.Identity.ApplicationUserLogin", b =>
                {
                    b.HasOne("CtrlBox.Domain.Identity.ApplicationUser", "User")
                        .WithMany("UserLogins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CtrlBox.Domain.Identity.ApplicationUserRole", b =>
                {
                    b.HasOne("CtrlBox.Domain.Identity.ApplicationRole", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CtrlBox.Domain.Identity.ApplicationUser", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CtrlBox.Domain.Identity.ApplicationUserToken", b =>
                {
                    b.HasOne("CtrlBox.Domain.Identity.ApplicationUser", "User")
                        .WithMany("UserTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
