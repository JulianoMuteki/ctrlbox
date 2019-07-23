using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CtrlBox.Infra.Context.Mapping
{
    public class TraceabilityMap : EntityConfiguration<Traceability>
    {
        protected override void Initialize(EntityTypeBuilder<Traceability> builder)
        {
            base.Initialize(builder);

            builder.ToTable("Traceabilities");
            builder.Property(x => x.Id).HasColumnName("TraceabilityID");
            builder.HasKey(b => b.Id).HasName("TraceabilityID");

            builder.HasOne(x => x.Box)
                .WithMany(x => x.Traceabilities)
                .HasForeignKey(x => x.BoxID);

            builder.HasOne(x => x.ProductItem)
                .WithMany(x => x.Traceabilities)
                .HasForeignKey(x => x.ProductItemID);

            builder.HasOne(x => x.TraceType)
                .WithMany(x => x.Traceabilities)
                .HasForeignKey(x => x.TraceTypeID);
        }
    }
}
