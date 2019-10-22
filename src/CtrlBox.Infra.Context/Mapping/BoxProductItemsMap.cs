using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CtrlBox.Infra.Context.Mapping
{
    public class BoxProductItemsMap : IEntityTypeConfiguration<BoxProductItems>
    {
        public void Configure(EntityTypeBuilder<BoxProductItems> builder)
        {
            builder.ToTable("BoxesProductsItems");
            builder.HasKey(b => b.BoxID);

            builder.Property(e => e.TotalItems)
                      .IsRequired();

            builder.HasOne(a => a.Box)
                   .WithOne(b => b.BoxProductItems)
                   .HasForeignKey<BoxProductItems>(b => b.BoxID);


            builder.HasOne(x => x.Product)
                   .WithMany(x => x.BoxesProductsItems)
                   .HasForeignKey(x => x.ProductID);
        }
    }
}
