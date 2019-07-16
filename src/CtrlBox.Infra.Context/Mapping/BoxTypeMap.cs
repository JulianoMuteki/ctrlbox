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
        }
    }
}
