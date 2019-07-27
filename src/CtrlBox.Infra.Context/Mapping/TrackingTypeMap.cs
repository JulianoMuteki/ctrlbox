using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CtrlBox.Infra.Context.Mapping
{
    public class TrackingTypeMap : EntityConfiguration<TrackingType>
    {
        protected override void Initialize(EntityTypeBuilder<TrackingType> builder)
        {
            base.Initialize(builder);

            builder.ToTable("TrackingsTypes");
            builder.Property(x => x.Id).HasColumnName("TrackingTypeID");
            builder.HasKey(b => b.Id).HasName("TrackingTypeID");

            builder.Property(x=>x.Description)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(x => x.TrackType)
                .HasConversion<int>();

            builder.HasOne(tk => tk.Picture)
                .WithMany(t => t.TracesTypes)
                .HasForeignKey(tk => tk.PictureID)
                .IsRequired(false);
        }
    }
}