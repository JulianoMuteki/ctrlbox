using CtrlBox.Domain.Entities;
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


            builder.Property(e => e.Barcode)
                    .IsRequired()
                     .HasMaxLength(14);

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

            builder.Property(x => x.BoxChildID)
                .IsRequired(false);
         
        }
    }
}
