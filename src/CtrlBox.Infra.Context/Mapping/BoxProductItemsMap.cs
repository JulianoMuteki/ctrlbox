using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CtrlBox.Infra.Context.Mapping
{
    public class BoxProductItemsMap : IEntityTypeConfiguration<BoxProductItems>
    {
        public void Configure(EntityTypeBuilder<BoxProductItems> builder)
        {
            builder.ToTable("BoxesProductsItemsMap");

            builder.HasKey(t => t.BoxID );

            builder.Property(e => e.TotalItems)
                      .IsRequired();
                 
            builder.HasOne(x => x.Box)
                    .WithOne(b => b.BoxProductItems)
                    .HasForeignKey<BoxProductItems>(ad => ad.BoxID);

            builder.HasOne(x => x.Product)
                   .WithMany(x => x.BoxesProductsItems)
                   .HasForeignKey(x => x.ProductID);
        }
    }
}
