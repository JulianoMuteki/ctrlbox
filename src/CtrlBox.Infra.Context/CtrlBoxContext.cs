﻿using CtrlBox.Domain.Entities;
using CtrlBox.Infra.Context.Mapping;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CtrlBox.Infra.Context
{
    public class CtrlBoxContext : DbContext
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

        }

        public CtrlBoxContext(DbContextOptions<CtrlBoxContext> options)
             : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

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
        }
    }
}
