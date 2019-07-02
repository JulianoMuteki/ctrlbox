using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CtrlBox.Infra.Context.Mapping
{
    public class AddressMap : EntityConfiguration<Address>
    {
        protected override void Initialize(EntityTypeBuilder<Address> builder)
        {
            base.Initialize(builder);

            builder.ToTable("Addresses");
            builder.Property(x => x.Id).HasColumnName("AddressID");
            builder.HasKey(b => b.Id).HasName("AddressID");

            builder.Property(e => e.CEP)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(e => e.Street)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(e => e.Number)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(e => e.District)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(e => e.Estate)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Reference)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasMany(e => e.Clients)
               .WithOne(c => c.Address)
               .HasForeignKey(s => s.AddressID);
        }
    }
}
