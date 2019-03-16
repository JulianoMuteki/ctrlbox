using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Infra.Context.Mapping
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => e.Id).HasName("Product_ID");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(e => e.CreationDate)
                .IsRequired();

            builder.Property(e => e.DateModified)
                .IsRequired();

            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(e => e.Weight)
                .IsRequired();


        }
    }
}
