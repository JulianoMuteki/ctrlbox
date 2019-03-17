using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CtrlBox.Infra.Context.Mapping
{
    public class ClientProductMap : IEntityTypeConfiguration<CustomerProductValue>
    {
        public void Configure(EntityTypeBuilder<CustomerProductValue> builder)
        {
            builder.ToTable("ClientsProducts");

            builder.HasKey(t => new { t.ClientID, t.ProductID });

            builder.Property(e => e.Price)
                .IsRequired();

            builder.HasOne(tk => tk.Client)
                .WithMany(t => t.CustomersProductsValues)
                .HasForeignKey(tk => tk.ClientID);

            builder.HasOne(tk => tk.Product)
                .WithMany(k => k.CustomersProductsValues)
                .HasForeignKey(tk => tk.ProductID);
        }
    }
}
