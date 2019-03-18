using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CtrlBox.Infra.Context.Mapping
{
    public class StockProductMap : IEntityTypeConfiguration<StockProduct>
    {
        public void Configure(EntityTypeBuilder<StockProduct> builder)
        {
            builder.ToTable("StocksProducts");

            builder.HasKey(t => new { t.ProductID, t.StockID });

            builder.Property(e => e.Amount)
                .IsRequired();

            builder.HasOne(tk => tk.Stock)
                .WithMany(t => t.StocksProducts)
                .HasForeignKey(tk => tk.StockID);

            builder.HasOne(tk => tk.Product)
                .WithMany(k => k.StocksProducts)
                .HasForeignKey(tk => tk.ProductID);
        }
    }
}
