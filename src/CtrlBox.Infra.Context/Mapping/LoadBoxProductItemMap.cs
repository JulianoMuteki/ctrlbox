using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CtrlBox.Infra.Context.Mapping
{
    public class LoadBoxProductItemMap : IEntityTypeConfiguration<LoadBoxProductItem>
    {
        public void Configure(EntityTypeBuilder<LoadBoxProductItem> builder)
        {
            builder.ToTable("LoadBoxesProductItems");

            builder.HasKey(t => new { t.LoadBoxID, t.ProductItemID });

            builder.HasOne(tk => tk.LoadBox)
                .WithMany(t => t.LoadBoxesProductItems)
                .HasForeignKey(tk => tk.LoadBoxID);

            builder.HasOne(tk => tk.ProductItem)
                .WithMany(k => k.LoadBoxesProductItems)
                .HasForeignKey(tk => tk.ProductItemID);
        }
    }
}
