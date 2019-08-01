using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CtrlBox.Infra.Context.Mapping
{
    public class OrderMap : EntityConfiguration<Order>
    {
        protected override void Initialize(EntityTypeBuilder<Order> builder)
        {
            base.Initialize(builder);

            builder.ToTable("Deliveries");
            builder.Property(x => x.Id).HasColumnName("DeliveryID");
            builder.HasKey(b => b.Id).HasName("DeliveryID");

            builder.Property(e => e.CreatedBy)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(e => e.FinalizedBy)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(e => e.DtStart)
                .IsRequired();

            builder.Property(e => e.DtEnd);

            builder.Property(e => e.IsFinalized)
                .IsRequired();

        }
    }
}
