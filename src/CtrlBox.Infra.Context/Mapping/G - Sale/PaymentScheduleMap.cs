using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CtrlBox.Infra.Context.Mapping
{
    public class PaymentScheduleMap : EntityConfiguration<PaymentSchedule>
    {
        protected override void Initialize(EntityTypeBuilder<PaymentSchedule> builder)
        {
            base.Initialize(builder);


            builder.ToTable("PaymentSchedules");
            builder.Property(x => x.Id).HasColumnName("PaymentScheduleID");
            builder.HasKey(b => b.Id).HasName("PaymentScheduleID");

            builder.Property(e => e.BenefitValue)
                 .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(e => e.ExprireDate)
              .IsRequired();

            builder.Property(e => e.RealizedDate);

            builder.HasOne(e => e.Payment)
                   .WithMany(c => c.PaymentsSchedules)
                   .HasForeignKey(s => s.PaymentID);


            builder.HasOne(e => e.PaymentMethod)
                   .WithMany(c => c.PaymentsSchedules)
                   .HasForeignKey(s => s.PaymentMethodID);
        }
    }
}