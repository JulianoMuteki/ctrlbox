using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CtrlBox.Infra.Context.Mapping
{
    class ClientCategoryMap : IEntityTypeConfiguration<ClientCategory>
    {
        public void Configure(EntityTypeBuilder<ClientCategory> builder)
        {
            builder.ToTable("ClientsCategories");

            builder.HasKey(t => new { t.ClientID, t.CategoryID });

            builder.HasOne(tk => tk.Category)
                .WithMany(t => t.ClientsCategories)
                .HasForeignKey(tk => tk.CategoryID);

            builder.HasOne(tk => tk.Client)
                .WithMany(k => k.ClientsCategories)
                .HasForeignKey(tk => tk.ClientID);
        }
    }
}