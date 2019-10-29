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
            builder.HasKey(t => t.Id);

            builder.Property(e => e.Minimum)
                .IsRequired();

            builder.Property(e => e.TotalStock)
                .IsRequired();

            builder.Property(e => e.DefaultPrice)
                  .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.HasOne(a => a.Product)
                    .WithMany(b => b.Stocks)
                    .HasForeignKey(b => b.ProductID);

            builder.HasOne(a => a.Client)
                    .WithMany(b => b.Stocks)
                    .HasForeignKey(b => b.ClientID);

            builder.HasMany(x => x.StocksMovements)
                .WithOne(y => y.Stock)
                .HasForeignKey(fk => fk.StockID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
