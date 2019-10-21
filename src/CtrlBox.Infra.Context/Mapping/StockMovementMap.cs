using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CtrlBox.Infra.Context.Mapping
{
    public class StockMovementMap : EntityConfiguration<StockMovement>
    {
        protected override void Initialize(EntityTypeBuilder<StockMovement> builder)
        {
            base.Initialize(builder);

            builder.ToTable("StocksMovements");
            builder.Property(x => x.Id).HasColumnName("StockMovementID");
            builder.HasKey(t => t.Id);

            builder.Property(e => e.UnitPrice)
                  .HasColumnType("decimal(10,2)")
                   .IsRequired();

            builder.Property(e => e.TotalValue)
                  .HasColumnType("decimal(10,2)")
                   .IsRequired();

            builder.Property(e => e.Amount)
                   .IsRequired();

            builder.Property(e => e.StockType)
                   .HasConversion<int>();


            builder.HasOne(x=>x.ClientSupplier)
                .WithMany(x=>x.StocksMovements)
                    .HasForeignKey(fk => fk.ClientSupplierID)
                    .OnDelete(DeleteBehavior.ClientSetNull);



        }
    }
}
