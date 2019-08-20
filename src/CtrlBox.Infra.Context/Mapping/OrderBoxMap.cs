using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CtrlBox.Infra.Context.Mapping
{
    public class OrderBoxMap : IEntityTypeConfiguration<OrderBox>
    {
        public void Configure(EntityTypeBuilder<OrderBox> builder)
        {
            builder.ToTable("OrdersBoxes");

            builder.HasKey(t => new { t.OrderID, t.BoxID });

            builder.HasOne(tk => tk.Order)
                .WithMany(t => t.OrdersBoxes)
                .HasForeignKey(tk => tk.OrderID);

            builder.HasOne(tk => tk.Box)
                .WithMany(k => k.OrdersBoxes)
                .HasForeignKey(tk => tk.BoxID);
        }
    }
}
