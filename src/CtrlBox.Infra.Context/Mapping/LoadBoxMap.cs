using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CtrlBox.Infra.Context.Mapping
{
   public class LoadBoxMap : EntityConfiguration<LoadBox>
    {
        protected override void Initialize(EntityTypeBuilder<LoadBox> builder)
        {
            base.Initialize(builder);

            builder.ToTable("LoadBoxes");
            builder.Property(x => x.Id).HasColumnName("LoadBoxID");
            builder.HasKey(b => b.Id).HasName("LoadBoxID");

            builder.Property(e => e.Barcode)
                .IsRequired()
                 .HasMaxLength(14);

            builder.Property(e => e.Description)
                .IsRequired()
                 .HasMaxLength(250);

            builder.HasOne(x => x.Box)
                .WithMany(x => x.LoadBoxes)
                .HasForeignKey(x => x.BoxID);

            builder.Property(e => e.LoadBoxParentID)
                .IsRequired(false);

            builder
            .HasOne(x => x.Product)
            .WithMany(x => x.LoadBoxes)
            .HasForeignKey(x => x.ProductID)
            .IsRequired(false);
        }
    }
}
