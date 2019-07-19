using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CtrlBox.Infra.Context.Mapping
{
    public class PictureMap : EntityConfiguration<Picture>
    {
        protected override void Initialize(EntityTypeBuilder<Picture> builder)
        {
            base.Initialize(builder);

            builder.ToTable("Pictures");
            builder.Property(x => x.Id).HasColumnName("PictureID");
            builder.HasKey(b => b.Id).HasName("PictureID");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Length)
                .IsRequired();

            builder.Property(e => e.Width)
                .IsRequired();

            builder.Property(e => e.Height)
                .IsRequired();

            builder.Property(e => e.Data)
                .HasColumnType("varbinary(max)")
                .IsRequired();

            builder.Property(e => e.ContentType)
                .IsRequired();

        }
    }
}