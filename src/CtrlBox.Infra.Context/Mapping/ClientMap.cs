using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CtrlBox.Infra.Context.Mapping
{
    public class ClientMap : EntityConfiguration<Client>
    {
        protected override void Initialize(EntityTypeBuilder<Client> builder)
        {
            base.Initialize(builder);

            builder.ToTable("Clients");

            builder.HasKey(e => e.Id).HasName("ClientID");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(e => e.QuantityBoxes)
                .IsRequired();

            builder.Property(e => e.BalanceDue)
                 .HasColumnType("float")
                .IsRequired();

            builder.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(e => e.Contact)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(e => e.Phone)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(e => e.IsDelivery)
                .IsRequired();
        }
    }
}
