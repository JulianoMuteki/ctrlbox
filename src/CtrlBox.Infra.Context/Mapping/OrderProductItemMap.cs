using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CtrlBox.Infra.Context.Mapping
{
  public  class OrderProductItemMap : IEntityTypeConfiguration<OrderProductItem>
    {
        public void Configure(EntityTypeBuilder<OrderProductItem> builder)
        {
            builder.ToTable("OrdersProductItems");

            builder.HasKey(t => new { t.OrderID, t.ProductItemID });

            builder.HasOne(tk => tk.Order)
                .WithMany(t => t.OrderProductItems)
                .HasForeignKey(tk => tk.OrderID);

            builder.HasOne(tk => tk.ProductItem)
                .WithMany(k => k.OrderProductItems)
                .HasForeignKey(tk => tk.ProductItemID);

            builder.Property(e => e.IsFinalized)
                .IsRequired();
        }
    }
}
