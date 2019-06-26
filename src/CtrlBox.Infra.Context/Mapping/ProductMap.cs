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

            builder.Property(e => e.Weight)
                 .HasColumnType("float")
                .IsRequired();

            builder.Property(e => e.UnitMeasure)
              .IsRequired()
              .HasMaxLength(50);
        }
    }
}