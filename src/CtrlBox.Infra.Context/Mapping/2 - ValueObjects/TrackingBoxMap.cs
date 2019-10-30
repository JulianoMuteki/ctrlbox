using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CtrlBox.Infra.Context.Mapping
{
    public class TrackingBoxMap : IEntityTypeConfiguration<TrackingBox>
    {
        public void Configure(EntityTypeBuilder<TrackingBox> builder)
        {
            builder.ToTable("TrackingsBoxes");
            builder.HasKey(t => new { t.BoxID, t.TrackingID });

            builder.HasOne(tk => tk.Box)
                .WithMany(t => t.TrackingsBoxes)
                .HasForeignKey(tk => tk.BoxID);

            builder.HasOne(tk => tk.Tracking)
                .WithMany(k => k.TrackingsBoxes)
                .HasForeignKey(tk => tk.TrackingID);
        }
    }
}
