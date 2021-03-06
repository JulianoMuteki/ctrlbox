﻿using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Identity;
using CtrlBox.Infra.Context.Mapping;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace CtrlBox.Infra.Context
{
    public class CtrlBoxContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, ApplicationUserClaim,
                                                        ApplicationUserRole, ApplicationUserLogin,
                                                        ApplicationRoleClaim, ApplicationUserToken>
    {
        public DbSet<Address> AddressesList { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientProductValue> CustomersProductsValues { get; set; }

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderBox> OrdersBoxes { get; set; }
        public DbSet<OrderProductItem> OrderProductItems { get; set; }

        public DbSet<DeliveryDetail> DeliveriesDetails { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleProduct> SalesProducts { get; set; }
        public DbSet<RouteClient> RoutesClients { get; set; }

        public DbSet<Tracking> Trackings { get; set; }
        public DbSet<TrackingType> TrackingsTypes { get; set; }
        public DbSet<TrackingClient> TrackingsClients { get; set; }
        public DbSet<TrackingBox> TrackingsBoxes { get; set; }

        public DbSet<OptiontType> OptiontsTypes { get; set; }
        public DbSet<ClientOptionType> ClientsOptionsTypes { get; set; }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StockMovement> StocksMovements { get; set; }

        public DbSet<BoxType> BoxesTypes { get; set; }
        public DbSet<ProductItem> ProductItems { get; set; }
        public DbSet<Box> Boxes { get; set; }
        public DbSet<BoxProductItem> BoxesProductItems { get; set; }
        public DbSet<BoxBarcode> BoxesCodes { get; set; }
        public DbSet<BoxProductItems> BoxesProductsItemsMap { get; set; }
        public DbSet<GraphicCodes> GraphicsCodes { get; set; }

        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<PaymentSchedule> PaymentSchedules { get; set; }

        public DbSet<SystemConfiguration> SystemConfigurations { get; set; }

        public CtrlBoxContext()
        {
            // ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public CtrlBoxContext(DbContextOptions<CtrlBoxContext> options)
             : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public void SetTrackAll()
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationFailure>();
            modelBuilder.Ignore<ValidationResult>();

            modelBuilder.ApplyConfiguration(new AddressMap());
            modelBuilder.ApplyConfiguration(new ClientMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new ClientProductMap());
            modelBuilder.ApplyConfiguration(new ProductItemMap());

            modelBuilder.ApplyConfiguration(new ExpenseMap());
            modelBuilder.ApplyConfiguration(new OrderMap());
            modelBuilder.ApplyConfiguration(new OrderBoxMap());
            modelBuilder.ApplyConfiguration(new OrderProductItemMap());

            modelBuilder.ApplyConfiguration(new DeliveryDetailMap());
            modelBuilder.ApplyConfiguration(new DeliveryBoxMap());

            modelBuilder.ApplyConfiguration(new RouteClientMap());
            modelBuilder.ApplyConfiguration(new SaleProductMap());
            modelBuilder.ApplyConfiguration(new RouteMap());
            modelBuilder.ApplyConfiguration(new SaleMap());

            modelBuilder.ApplyConfiguration(new TrackingTypeMap());
            modelBuilder.ApplyConfiguration(new TrackingMap());
            modelBuilder.ApplyConfiguration(new TrackingClientMap());
            modelBuilder.ApplyConfiguration(new TrackingBoxMap());

            modelBuilder.ApplyConfiguration(new StockMap());
            modelBuilder.ApplyConfiguration(new StockMovementMap());

            modelBuilder.ApplyConfiguration(new OptiontTypeMap());
            modelBuilder.ApplyConfiguration(new ClientOptionTypeMap());

            modelBuilder.ApplyConfiguration(new BoxProductItemsMap());
            modelBuilder.ApplyConfiguration(new BoxBarcodeMap());
            modelBuilder.ApplyConfiguration(new BoxMap());
            modelBuilder.ApplyConfiguration(new BoxTypeMap());
            modelBuilder.ApplyConfiguration(new BoxProductItemMap());
            modelBuilder.ApplyConfiguration(new PictureMap());
            modelBuilder.ApplyConfiguration(new GraphicCodesMap());

            modelBuilder.ApplyConfiguration(new PaymentMap());
            modelBuilder.ApplyConfiguration(new PaymentMethodMap());
            modelBuilder.ApplyConfiguration(new PaymentScheduleMap());

            modelBuilder.ApplyConfiguration(new SystemConfigurationMap());

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>(b =>
            {
                // Each User can have many UserClaims
                b.HasMany(e => e.UserClaims)
                    .WithOne(e => e.User)
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                // Each User can have many UserLogins
                b.HasMany(e => e.UserLogins)
                    .WithOne(e => e.User)
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                // Each User can have many UserTokens
                b.HasMany(e => e.UserTokens)
                    .WithOne(e => e.User)
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();

                b.HasMany(e => e.Deliveries)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserID)
                    .IsRequired();
            });

            modelBuilder.Entity<ApplicationRole>(b =>
            {
                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                // Each Role can have many associated RoleClaims
                b.HasMany(e => e.RoleClaims)
                    .WithOne(e => e.Role)
                    .HasForeignKey(rc => rc.RoleId)
                    .IsRequired();
            });
        }
    }
}
