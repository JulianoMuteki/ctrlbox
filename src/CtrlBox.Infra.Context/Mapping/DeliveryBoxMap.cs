﻿using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CtrlBox.Infra.Context.Mapping
{
    public class DeliveryBoxMap : IEntityTypeConfiguration<DeliveryBox>
    {
        public void Configure(EntityTypeBuilder<DeliveryBox> builder)
        {
            builder.ToTable("DeliveriesBoxes");

            builder.HasKey(t => new { t.DeliveryDetailID, t.BoxID });

            builder.HasOne(tk => tk.DeliveryDetail)
                .WithMany(t => t.DeliveriesBoxes)
                .HasForeignKey(tk => tk.DeliveryDetailID);

            builder.HasOne(tk => tk.Box)
                .WithMany(k => k.DeliveriesBoxes)
                .HasForeignKey(tk => tk.BoxID);
        }
    }
}
