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

            builder.Property(e => e.Status)
                .HasConversion<int>();

            builder.OwnsOne(
                x => x.FlowStep,
                        flowStep =>
                        {
                            flowStep.Property(e => e.EFlowStep)
                                    .HasColumnName("EFlowStep")
                                    .HasConversion<int>();
                        });

            builder.Property(e => e.Description)
                    .IsRequired()
                     .HasMaxLength(250);

            builder.Property(e => e.PorcentFull)
                    .IsRequired();

            builder.HasOne(x => x.BoxType)
                .WithMany(x => x.Boxes)
                .HasForeignKey(x => x.BoxTypeID);

            builder.HasMany(x => x.BoxesChildren)
                .WithOne(x=>x.BoxParent)
                .HasForeignKey(x => x.BoxParentID)
                .IsRequired(false);

            builder.Ignore(x => x.CountQuantityProductItems);
        }
    }
}
