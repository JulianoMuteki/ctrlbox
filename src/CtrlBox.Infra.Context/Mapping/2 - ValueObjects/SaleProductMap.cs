using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CtrlBox.Infra.Context.Mapping
{
    public class SaleProductMap : IEntityTypeConfiguration<SaleProduct>
    {
        public void Configure(EntityTypeBuilder<SaleProduct> builder)
        {
            builder.ToTable("SalesProducts");

            builder.HasKey(t => new { t.ProductID, t.SaleID });

            builder.Property(e => e.ValueProductSale)
                 .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(e => e.TotalValue)
                 .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(e => e.Quantity)
                .IsRequired();

            builder.Property(e => e.DiscountValueSale)
                .IsRequired();

            builder.HasOne(tk => tk.Sale)
                .WithMany(t => t.SalesProducts)
                .HasForeignKey(tk => tk.SaleID);

            builder.HasOne(tk => tk.Product)
                .WithMany(k => k.SalesProducts)
                .HasForeignKey(tk => tk.ProductID);
        }
    }
}
