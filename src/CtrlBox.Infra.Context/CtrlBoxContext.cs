﻿using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Identity;
using CtrlBox.Infra.Context.Mapping;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace CtrlBox.Infra.Context
{
    public class CtrlBoxContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, ApplicationUserClaim,
                                                        ApplicationUserRole, ApplicationUserLogin,
                                                        ApplicationRoleClaim, ApplicationUserToken>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientProductValue> CustomersProductsValues { get; set; }

        public DbSet<Check> Checks { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<DeliveryProduct> DeliveriesProducts { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StockProduct> StocksProducts { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleProduct> SalesProducts { get; set; }
        public DbSet<RouteClient> RoutesClients { get; set; }

        public CtrlBoxContext()
        {
           // ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public CtrlBoxContext(DbContextOptions<CtrlBoxContext> options)
             : base(options)
        {
          //  ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           // optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClientMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new ClientProductMap());

            modelBuilder.ApplyConfiguration(new CheckMap());
            modelBuilder.ApplyConfiguration(new ExpenseMap());
            modelBuilder.ApplyConfiguration(new DeliveryMap());
            modelBuilder.ApplyConfiguration(new DeliveryProductMap());
            modelBuilder.ApplyConfiguration(new StockMap());
            modelBuilder.ApplyConfiguration(new RouteClientMap());
            modelBuilder.ApplyConfiguration(new SaleProductMap());
            modelBuilder.ApplyConfiguration(new RouteMap());
            modelBuilder.ApplyConfiguration(new SaleMap());
            modelBuilder.ApplyConfiguration(new StockProductMap());

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
