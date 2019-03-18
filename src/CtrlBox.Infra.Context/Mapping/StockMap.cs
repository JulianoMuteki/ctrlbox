using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CtrlBox.Infra.Context.Mapping
{
    public class StockMap : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.ToTable("Stocks");

            builder.HasKey(e => e.Id).HasName("Stock_ID");

            builder.Property(e => e.CreationDate)
                .IsRequired();

            builder.Property(e => e.DateModified)
                .IsRequired();

            builder.Property(e => e.AmountBoxes)
                .IsRequired();

        }
    }
}
