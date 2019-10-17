using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Infra.Context.Mapping
{
    public class StockMap : EntityConfiguration<Stock>
    {
        protected override void Initialize(EntityTypeBuilder<Stock> builder)
        {
            base.Initialize(builder);

            builder.ToTable("Stocks");
            builder.Property(x => x.Id).HasColumnName("StockID");
            builder.HasKey(t => new { t.Id, t.ProductID });

            builder.Property(e => e.Minimum)
                .IsRequired();

            builder.Property(e => e.TotalStock)
                .IsRequired();

            builder.Property(e => e.DefaultPrice)
                  .HasColumnType("decimal(10,2)")
                .IsRequired();

        }
    }
}
