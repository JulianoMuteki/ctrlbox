using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CtrlBox.Infra.Context.Mapping
{
    public class ProductMap : EntityConfiguration<Product>
    {
        protected override void Initialize(EntityTypeBuilder<Product> builder)
        {
            base.Initialize(builder);

            builder.ToTable("Products");
            builder.Property(x => x.Id).HasColumnName("ProductID");
            builder.HasKey(b => b.Id).HasName("ProductID");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(e => e.Package)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.Capacity)
                 .HasColumnType("float")
                .IsRequired();

            builder.Property(e => e.UnitMeasure)
              .IsRequired()
              .HasMaxLength(50);

            builder.Property(e => e.Weight)
                 .HasColumnType("float")
                .IsRequired();

            builder.Property(e => e.MassUnitWeight)
              .IsRequired()
              .HasMaxLength(50);

            builder.HasOne(tk => tk.Picture)
                    .WithMany(t => t.Products)
                    .HasForeignKey(tk => tk.PictureID)
                    .IsRequired(false);

            builder.Ignore(x => x.OptionsMassUnit);
            builder.Ignore(x => x.OptionsVolumeUnit);
        }
    }
}