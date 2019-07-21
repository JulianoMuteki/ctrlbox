using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CtrlBox.Infra.Context.Mapping
{
    public class TraceTypeMap : EntityConfiguration<TraceType>
    {
        protected override void Initialize(EntityTypeBuilder<TraceType> builder)
        {
            base.Initialize(builder);

            builder.ToTable("TracesTypes");
            builder.Property(x => x.Id).HasColumnName("TraceTypeID");
            builder.HasKey(b => b.Id).HasName("TraceTypeID");

            builder.Property(x=>x.Description)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(x => x.TypeTrace)
                .HasConversion<int>();

            builder.HasOne(tk => tk.Picture)
                .WithMany(t => t.TracesTypes)
                .HasForeignKey(tk => tk.PictureID)
                .IsRequired(false);
        }
    }
}