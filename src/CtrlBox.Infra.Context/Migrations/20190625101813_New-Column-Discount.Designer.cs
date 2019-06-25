﻿// <auto-generated />
using System;
using CtrlBox.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CtrlBox.Infra.Context.Migrations
{
    [DbContext(typeof(CtrlBoxContext))]
    [Migration("20190625101813_New-Column-Discount")]
    partial class NewColumnDiscount
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CtrlBox.Domain.Entities.Check", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<DateTime>("DateModified");

                    b.Property<DateTime?>("DtExpire");

                    b.Property<bool>("IsDelete");

                    b.Property<bool>("IsDisable");

                    b.Property<int>("Number");

                    b.Property<Guid>("SaleID");

                    b.Property<float>("Value");

                    b.HasKey("Id");

                    b.HasIndex("SaleID");

                    b.ToTable("Checks");
                });

            modelBuilder.Entity("CtrlBox.Domain.Entities.Client", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<float>("BalanceDue");

                    b.Property<string>("Contact");

                    b.Property<DateTime>("CreationDate");

                    b.Property<DateTime>("DateModified");

                    b.Property<bool>("IsDelete");

                    b.Property<bool>("IsDelivery");

                    b.Property<bool>("IsDisable");

                    b.Property<string>("Name");

                    b.Property<string>("Phone");

                    b.Property<int>("QuantityBoxes");

                    b.HasKey("Id");

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
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreationDate");

                    b.Property<DateTime>("DateModified");

                    b.Property<DateTime?>("DtEnd");

                    b.Property<DateTime>("DtStart");

                    b.Property<string>("FinalizedBy");

                    b.Property<bool>("IsDelete");

                    b.Property<bool>("IsDisable");

                    b.Property<bool>("IsFinalized");

                    b.Property<Guid>("RouteID");

                    b.Property<Guid>("UserID");

                    b.HasKey("Id");

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
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<DateTime>("DateModified");

                    b.Property<Guid>("DeliveryID");

                    b.Property<string>("Description");

                    b.Property<bool>("IsDelete");

                    b.Property<bool>("IsDisable");

                    b.Property<double>("Value");

                    b.HasKey("Id");

                    b.HasIndex("DeliveryID");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("CtrlBox.Domain.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("Description");

                    b.Property<bool>("IsDelete");

                    b.Property<bool>("IsDisable");

                    b.Property<string>("Name");

                    b.Property<float>("Weight");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("CtrlBox.Domain.Entities.Route", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<DateTime>("DateModified");

                    b.Property<bool>("HasOpenDelivery");

                    b.Property<bool>("IsDelete");

                    b.Property<bool>("IsDisable");

                    b.Property<int>("KmDistance");

                    b.Property<string>("Name");

                    b.Property<string>("Truck");

                    b.HasKey("Id");

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
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ClientID");

                    b.Property<DateTime>("CreationDate");

                    b.Property<DateTime>("DateModified");

                    b.Property<Guid>("DeliveryID");

                    b.Property<decimal>("ForwardValue");

                    b.Property<bool>("IsDelete");

                    b.Property<bool>("IsDisable");

                    b.Property<bool>("IsFinished");

                    b.Property<decimal>("ReceivedValue");

                    b.HasKey("Id");

                    b.HasIndex("ClientID");

                    b.HasIndex("DeliveryID");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("CtrlBox.Domain.Entities.SaleProduct", b =>
                {
                    b.Property<Guid>("ProductID");

                    b.Property<Guid>("SaleID");

                    b.Property<int>("Amount");

                    b.Property<int>("DiscountAmount");

                    b.Property<decimal>("SaleValue")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("ProductID", "SaleID");

                    b.HasIndex("SaleID");

                    b.ToTable("SalesProducts");
                });

            modelBuilder.Entity("CtrlBox.Domain.Entities.Stock", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AmountBoxes");

                    b.Property<DateTime>("CreationDate");

                    b.Property<DateTime>("DateModified");

                    b.Property<bool>("IsDelete");

                    b.Property<bool>("IsDisable");

                    b.HasKey("Id");

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

            modelBuilder.Entity("CtrlBox.Domain.Entities.Check", b =>
                {
                    b.HasOne("CtrlBox.Domain.Entities.Sale", "Sale")
                        .WithMany("Checks")
                        .HasForeignKey("SaleID")
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
