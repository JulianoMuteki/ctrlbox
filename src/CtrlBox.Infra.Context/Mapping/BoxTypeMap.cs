using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CtrlBox.Infra.Context.Mapping
{
    public class BoxTypeMap : EntityConfiguration<BoxType>
    {
        protected override void Initialize(EntityTypeBuilder<BoxType> builder)
        {
            base.Initialize(builder);

            builder.ToTable("BoxesTypes");
            builder.Property(x => x.Id).HasColumnName("BoxTypeID");
            builder.HasKey(b => b.Id).HasName("BoxTypeID");

            builder.Property(e => e.Name)
                .IsRequired()
                 .HasMaxLength(50);

            builder.Property(e => e.Description)
                .IsRequired()
                 .HasMaxLength(250);

            builder.Property(e => e.Lenght)
                    .HasColumnType("numeric(18,3)")
                    .IsRequired();

            builder.Property(e => e.Height)
                    .HasColumnType("numeric(18,3)")
                    .IsRequired();

            builder.Property(e => e.Width)
                    .HasColumnType("numeric(18,3)")
                    .IsRequired();

            builder.Property(e => e.MaxProductsItems)
                    .IsRequired();

            builder.HasOne(tk => tk.Picture)
                    .WithMany(t => t.BoxesTypes)
                    .HasForeignKey(tk => tk.PictureID)
                    .IsRequired(false);
        }
    }
}
