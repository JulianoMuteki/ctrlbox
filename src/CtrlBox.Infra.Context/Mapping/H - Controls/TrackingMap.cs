using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CtrlBox.Infra.Context.Mapping
{
    public class TrackingMap : EntityConfiguration<Tracking>
    {
        protected override void Initialize(EntityTypeBuilder<Tracking> builder)
        {
            base.Initialize(builder);

            builder.ToTable("Trackings");
            builder.Property(x => x.Id).HasColumnName("TrackingID");
            builder.HasKey(b => b.Id).HasName("TrackingID");

            builder.Property(x => x.IsLastTrack)
                .IsRequired();

            builder.HasOne(x => x.Box)
                .WithMany(x => x.Trackings)
                .HasForeignKey(x => x.BoxID);

            builder.HasOne(x => x.ProductItem)
                .WithMany(x => x.Trackings)
                .HasForeignKey(x => x.ProductItemID);

            builder.HasOne(x => x.TrackingType)
                .WithMany(x => x.Trackings)
                .HasForeignKey(x => x.TrackingTypeID);
        }
    }
}
