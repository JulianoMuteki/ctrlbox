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
            builder.HasKey(b => b.Id).HasName("StockID");

            builder.Property(e => e.AmountBoxes)
                .IsRequired();

        }
    }
}
