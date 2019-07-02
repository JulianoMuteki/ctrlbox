using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CtrlBox.Infra.Context.Mapping
{
    public class PaymentMap : EntityConfiguration<Payment>
    {
        protected override void Initialize(EntityTypeBuilder<Payment> builder)
        {
            base.Initialize(builder);

            builder.ToTable("Payments");
            builder.Property(x => x.Id).HasColumnName("PaymentID");
            builder.HasKey(b => b.Id).HasName("PaymentID");

            builder.Property(e => e.TotalValueSale)
                 .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(e => e.RemainingValue)
                  .HasColumnType("decimal(10,2)")
                 .IsRequired();

            builder.Property(e => e.PaymentDate)
              .IsRequired();

            builder.Property(e => e.IsPaid)
                .IsRequired();

            builder.Property(e => e.IsCashPayment)
                .IsRequired();

            builder.Property(e => e.NumberParcels)
               .IsRequired();
        
            builder.HasMany(c => c.PaymentsSchedules)
                 .WithOne(e => e.Payment)
                 .HasForeignKey(s => s.PaymentID);

            builder.HasOne(sa => sa.Sale)
                  .WithOne(c => c.Payment)
                  .HasForeignKey<Payment>(k => k.SaleID);
        }
    }
} 