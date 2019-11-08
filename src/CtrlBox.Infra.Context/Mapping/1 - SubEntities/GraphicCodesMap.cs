using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Infra.Context.Mapping
{
   public class GraphicCodesMap : EntityConfiguration<GraphicCodes>
    {
        protected override void Initialize(EntityTypeBuilder<GraphicCodes> builder)
        {
            base.Initialize(builder);

            builder.ToTable("GraphicsCodes");
            builder.Property(x => x.Id).HasColumnName("GraphicCodeID");
            builder.HasKey(b => b.Id).HasName("GraphicCodeID");

            builder.Property(e => e.BarcodeEAN13)
                    .IsRequired()
                     .HasMaxLength(13);

            builder.Property(e => e.BarcodeGS1_128)
                      .IsRequired()
                     .HasMaxLength(48);

            builder.Property(e => e.RFID)
                    .IsRequired()
                     .HasMaxLength(250);

            builder.HasMany(a => a.Boxes)
                   .WithOne(b => b.GraphicCodes)
                   .HasForeignKey(fk => fk.GraphicCodeID);

        }
    }
}