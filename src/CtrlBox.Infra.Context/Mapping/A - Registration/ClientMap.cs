﻿using CtrlBox.Domain.Entities;
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
            builder.Property(x => x.Id).HasColumnName("ClientID");
            builder.HasKey(b => b.Id).HasName("ClientID");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(e => e.Contact)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(e => e.Phone)
                .IsRequired()
                .HasMaxLength(250);

            builder.HasOne(z => z.Address)
                .WithMany(y => y.Clients)
                .HasForeignKey(z => z.AddressID);

            builder.HasMany(c=>c.Sales)
                .WithOne(c=>c.Client)
                .HasForeignKey(s => s.ClientID);

        }
    }
}
