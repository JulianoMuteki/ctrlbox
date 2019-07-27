using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CtrlBox.Infra.Context.Mapping
{
    public class CategoryMap : EntityConfiguration<Category>
    {
        protected override void Initialize(EntityTypeBuilder<Category> builder)
        {
            base.Initialize(builder);

            builder.ToTable("Categories");
            builder.Property(x => x.Id).HasColumnName("CategoryID");
            builder.HasKey(b => b.Id).HasName("CategoryID");

            builder.Property(e => e.Name)
                .IsRequired()
                 .HasMaxLength(50);

            builder.Property(e => e.Description)
                .IsRequired()
                 .HasMaxLength(250);
        }
    }
}