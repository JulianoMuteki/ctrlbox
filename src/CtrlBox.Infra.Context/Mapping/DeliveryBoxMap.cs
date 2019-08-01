using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CtrlBox.Infra.Context.Mapping
{
    public class DeliveryBoxMap : IEntityTypeConfiguration<DeliveryBox>
    {
        public void Configure(EntityTypeBuilder<DeliveryBox> builder)
        {
            builder.ToTable("DeliveriesBoxes");

            builder.HasKey(t => new { t.DeliveryID, t.BoxID });

            builder.HasOne(tk => tk.Delivery)
                .WithMany(t => t.DeliveriesBoxes)
                .HasForeignKey(tk => tk.DeliveryID)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(tk => tk.Box)
                .WithMany(k => k.DeliveriesBoxes)
                .HasForeignKey(tk => tk.BoxID)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(tk => tk.Client)
                .WithMany(k => k.DeliveriesBoxes)
                .HasForeignKey(tk => tk.ClientID)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
