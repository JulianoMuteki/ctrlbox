using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CtrlBox.Infra.Context.Mapping
{
    public class RouteMap : EntityConfiguration<Route>
    {
        protected override void Initialize(EntityTypeBuilder<Route> builder)
        {
            base.Initialize(builder);

            builder.ToTable("Routes");
            builder.Property(x => x.Id).HasColumnName("RouteID");
            builder.HasKey(b => b.Id).HasName("RouteID");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(e => e.KmDistance)
                .IsRequired();

            builder.Property(e => e.Truck)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(e => e.HasOpenDelivery)
                .IsRequired();
        }
    }
}
