using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CtrlBox.Infra.Context.Mapping
{
    public class DeliveryMap : IEntityTypeConfiguration<Delivery>
    {
        public void Configure(EntityTypeBuilder<Delivery> builder)
        {
            builder.ToTable("Deliveries");

            builder.HasKey(e => e.Id).HasName("Delivery_ID");

            builder.Property(e => e.CreatedBy)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(e => e.FinalizedBy)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(e => e.CreationDate)
                .IsRequired();

            builder.Property(e => e.DateModified)
                .IsRequired();

            builder.Property(e => e.DtStart)
                .IsRequired();

            builder.Property(e => e.DtEnd);

            builder.Property(e => e.IsFinalized)
                .IsRequired();

        }
    }
}
