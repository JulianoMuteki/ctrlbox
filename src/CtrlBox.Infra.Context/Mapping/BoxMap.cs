using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CtrlBox.Infra.Context.Mapping
{
    public class BoxMap : EntityConfiguration<Box>
    {
        protected override void Initialize(EntityTypeBuilder<Box> builder)
        {
            base.Initialize(builder);

            builder.ToTable("Boxes");
            builder.Property(x => x.Id).HasColumnName("BoxID");
            builder.HasKey(b => b.Id).HasName("BoxID");

            builder.Property(e => e.Name)
                .IsRequired()
                 .HasMaxLength(50);

            builder.Property(e => e.Name)
                .IsRequired()
                 .HasMaxLength(250);

            builder.Property(e => e.IsProductBox)
                .IsRequired();
        }
    }
}
