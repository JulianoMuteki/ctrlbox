using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CtrlBox.Infra.Context.Mapping
{
    public class SystemConfigurationMap : EntityConfiguration<SystemConfiguration>
    {
        protected override void Initialize(EntityTypeBuilder<SystemConfiguration> builder)
        {
            base.Initialize(builder);

            builder.ToTable("SystemConfigurations");
            builder.Property(x => x.Id).HasColumnName("SystemID");
            builder.HasKey(b => b.Id).HasName("SystemID");

            builder.Property(e => e.CultureInfo)
             .IsRequired()
             .HasMaxLength(6);

            builder.Property(e => e.UnitProduct)
             .IsRequired()
             .HasMaxLength(50);
        }
    }
}
