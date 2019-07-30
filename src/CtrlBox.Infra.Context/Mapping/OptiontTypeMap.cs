using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CtrlBox.Infra.Context.Mapping
{
    public class OptiontTypeMap : EntityConfiguration<OptiontType>
    {
        protected override void Initialize(EntityTypeBuilder<OptiontType> builder)
        {
            base.Initialize(builder);

            builder.ToTable("OptiontsTypes");
            builder.Property(x => x.Id).HasColumnName("OptiontTypeMapID");
            builder.HasKey(b => b.Id).HasName("OptiontTypeMapID");

            builder.Property(e => e.Name)
                .IsRequired()
                 .HasMaxLength(50);

            builder.Property(e => e.Description)
                .IsRequired()
                 .HasMaxLength(250);

            builder.Property(x => x.EClientType)
                .HasConversion<int>();
        }
    }
}
