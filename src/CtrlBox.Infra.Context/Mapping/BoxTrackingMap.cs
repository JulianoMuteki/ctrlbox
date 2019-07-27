using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CtrlBox.Infra.Context.Mapping
{
    public class BoxTrackingMap : EntityConfiguration<BoxTracking>
    {
        protected override void Initialize(EntityTypeBuilder<BoxTracking> builder)
        {
            base.Initialize(builder);

            builder.ToTable("BoxesTrackings");
            builder.Property(x => x.Id).HasColumnName("BoxTrackingID");
            builder.HasKey(b => b.Id).HasName("BoxTrackingID");

            builder.HasOne(x => x.Box)
                .WithMany(x => x.Traceabilities)
                .HasForeignKey(x => x.BoxID);

            builder.HasOne(x => x.ProductItem)
                .WithMany(x => x.Traceabilities)
                .HasForeignKey(x => x.ProductItemID);

            builder.HasOne(x => x.TrackingType)
                .WithMany(x => x.BoxesTrackings)
                .HasForeignKey(x => x.TrackingTypeID);
        }
    }
}
