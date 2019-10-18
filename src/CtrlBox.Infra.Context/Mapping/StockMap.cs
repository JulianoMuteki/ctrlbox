using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

            builder.HasOne(a => a.Product)
                    .WithOne(b => b.Stock)
                    .HasForeignKey<Stock>(b => b.ProductID);

            builder.HasOne(a => a.StorageLocation)
                    .WithOne(b => b.Stock)
                    .HasForeignKey<Stock>(b => b.StorageLocationID);
        }
    }
}
