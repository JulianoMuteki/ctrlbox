using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CtrlBox.Infra.Context.Mapping
{
    public class ProductItemMap : EntityConfiguration<ProductItem>
    {
        protected override void Initialize(EntityTypeBuilder<ProductItem> builder)
        {
            base.Initialize(builder);

            builder.ToTable("ProductItems");
            builder.Property(x => x.Id).HasColumnName("ProductItemID");
            builder.HasKey(b => b.Id).HasName("ProductItemID");

            builder.Property(e => e.Barcode)
                .IsRequired()
                 .HasMaxLength(14);

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
        }
    }
}
