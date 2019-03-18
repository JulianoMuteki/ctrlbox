using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CtrlBox.Infra.Context.Mapping
{
    public class SaleMap : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sales");

            builder.HasKey(e => e.Id).HasName("Sale_ID");

            builder.Property(e => e.CreationDate)
                .IsRequired();

            builder.Property(e => e.DateModified)
                .IsRequired();

            builder.Property(e => e.ForwardValue)
                .IsRequired();

            builder.Property(e => e.ReceivedValue)
                .IsRequired();

            builder.Property(e => e.TotalReturnedBoxes)
                .IsRequired();
        }
    }
}
