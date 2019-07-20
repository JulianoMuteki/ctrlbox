using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CtrlBox.Infra.Context.Mapping
{
    public class BoxProductItemMap : IEntityTypeConfiguration<BoxProductItem>
    {
        public void Configure(EntityTypeBuilder<BoxProductItem> builder)
        {
            builder.ToTable("BoxesProductItems");

            builder.HasKey(t => new { t.BoxID, t.ProductItemID });

            builder.Property(x => x.IsDelivered)
                .IsRequired();

            builder.HasOne(tk => tk.Delivery)
                .WithMany(t => t.BoxesProductItems)
                .HasForeignKey(tk => tk.DeliveryID)
                .IsRequired(false);

            builder.HasOne(tk => tk.Box)
                .WithMany(t => t.BoxesProductItems)
                .HasForeignKey(tk => tk.BoxID);

            builder.HasOne(tk => tk.ProductItem)
                .WithMany(k => k.LoadBoxesProductItems)
                .HasForeignKey(tk => tk.ProductItemID);
        }
    }
}
