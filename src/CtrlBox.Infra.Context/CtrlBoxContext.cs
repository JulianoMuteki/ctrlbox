﻿using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Infra.Context
{
    public class CtrlBoxContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<CustomerProductValue> CustomersProductsValues { get; set; }

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

            base.OnModelCreating(modelBuilder);
        }
    }
}
