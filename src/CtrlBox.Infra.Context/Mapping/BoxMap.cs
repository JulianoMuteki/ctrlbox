﻿using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CtrlBox.Infra.Context.Mapping
{
    public class BoxMap : EntityConfiguration<Box>
    {
        protected override void Initialize(EntityTypeBuilder<Box> builder)
        {
            base.Initialize(builder);

            builder.ToTable("Boxes");
            builder.Property(x => x.Id).HasColumnName("BoxID");
            builder.HasKey(b => b.Id).HasName("BoxID");

            builder.Property(e => e.StatusBox)
                  .IsRequired();

            builder.Property(e => e.Description)
                    .IsRequired()
                     .HasMaxLength(250);

            builder.HasOne(x => x.BoxType)
                .WithMany(x => x.Boxes)
                .HasForeignKey(x => x.BoxTypeID);

            builder.HasOne(x => x.Product)
                .WithMany(x => x.Boxes)
                .HasForeignKey(x => x.ProductID)
                .IsRequired(false);

            builder.HasMany(x => x.BoxesChildren)
                .WithOne(x=>x.BoxParent)
                .HasForeignKey(x => x.BoxParentID)
                .IsRequired(false);
        }
    }
}