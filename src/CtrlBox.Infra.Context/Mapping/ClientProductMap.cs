using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Infra.Context.Mapping
{
    public class ClientProductMap : IEntityTypeConfiguration<CustomerProductValue>
    {
        public void Configure(EntityTypeBuilder<CustomerProductValue> builder)
        {
            builder.HasKey(t => new { t.ClientID, t.ProductID });

            builder.HasOne(tk => tk.Client).WithMany(t => t.CustomersProductsValues).HasForeignKey(tk => tk.ClientID);
            builder.HasOne(tk => tk.Product).WithMany(k => k.CustomersProductsValues).HasForeignKey(tk => tk.ProductID);

            builder.Property(e => e.Product)
                .IsRequired();
        }

    }
}
