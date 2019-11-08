using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CtrlBox.Infra.Context.Mapping
{
    public class BoxBarcodeMap : EntityConfiguration<BoxBarcode>
    {
        protected override void Initialize(EntityTypeBuilder<BoxBarcode> builder)
        {
            base.Initialize(builder);

            builder.ToTable("BoxesBarcodes");
         
            builder.HasKey(t => new { t.Id, t.BoxID });
            builder.Property(x => x.Id).HasColumnName("BoxBarcodeID");

            builder.Property(e => e.BarcodeEAN13)
                    .IsRequired()
                     .HasMaxLength(13);

            builder.Property(e => e.BarcodeGS1_128)
                      .IsRequired()
                     .HasMaxLength(48);

            builder.Property(e => e.RFID)
                    .IsRequired()
                     .HasMaxLength(250);

            //builder.HasOne(x => x.Box)
            //        .WithOne(ad => ad.BoxBarcode)
            //        .HasForeignKey<BoxBarcode>(ad => ad.BoxID);
        }
    }
}
