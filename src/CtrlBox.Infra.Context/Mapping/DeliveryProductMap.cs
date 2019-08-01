using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CtrlBox.Infra.Context.Mapping
{
    public class DeliveryProductMap : IEntityTypeConfiguration<DeliveryProduct>
    {
        public void Configure(EntityTypeBuilder<DeliveryProduct> builder)
        {
            builder.ToTable("DeliveriesProducts");

            builder.HasKey(t => new { t.DeliveryID, t.ProductID });

            builder.Property(e => e.QuantityProductItem)
                .IsRequired();

            builder.HasOne(tk => tk.Delivery)
                .WithMany(t => t.DeliveriesProducts)
                .HasForeignKey(tk => tk.DeliveryID);

            builder.HasOne(tk => tk.Product)
                .WithMany(k => k.DeliveriesProducts)
                .HasForeignKey(tk => tk.ProductID);
        }
    }
}
