using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CtrlBox.Infra.Context.Mapping
{
    public class DeliveryDetailMap : EntityConfiguration<DeliveryDetail>
    {
        protected override void Initialize(EntityTypeBuilder<DeliveryDetail> builder)
        {
            base.Initialize(builder);

            builder.ToTable("DeliveriesDetails");
            builder.Property(x => x.Id).HasColumnName("DeliveryDetailID");
            builder.HasKey(b => b.Id).HasName("DeliveryDetailID");

            builder.Property(e => e.QuantityProductItem)
                .IsRequired();

            builder.HasOne(tk => tk.Order)
                .WithMany(t => t.DeliveriesDetails)
                .HasForeignKey(tk => tk.OrderID)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(tk => tk.Product)
                .WithMany(k => k.DeliveriesDetails)
                .HasForeignKey(tk => tk.ProductID);

            builder.HasOne(tk => tk.Client)
                .WithMany(k => k.DeliveriesDetails)
                .HasForeignKey(tk => tk.ClientID);
        }
    }
}
