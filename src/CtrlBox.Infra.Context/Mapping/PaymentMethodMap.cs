using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CtrlBox.Infra.Context.Mapping
{
    class PaymentMethodMap : EntityConfiguration<PaymentMethod>
    {
        protected override void Initialize(EntityTypeBuilder<PaymentMethod> builder)
        {
            base.Initialize(builder);

            builder.ToTable("PaymentsMethods");
            builder.Property(x => x.Id).HasColumnName("PaymentMethodID");
            builder.HasKey(b => b.Id).HasName("PaymentMethodID");

            builder.Property(e => e.Descrition)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.MethodName)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}