using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CtrlBox.Infra.Context.Mapping
{
    public class BoxCodeMap : EntityConfiguration<BoxCode>
    {
        protected override void Initialize(EntityTypeBuilder<BoxCode> builder)
        {
            base.Initialize(builder);

            builder.ToTable("BoxesCodes");
         
            //builder.HasKey(b => b.Id).HasName("BoxCodeID");
            builder.HasKey(t => new { t.Id, t.BoxID });
            builder.Property(x => x.Id).HasColumnName("BoxCodeID");

            builder.Property(e => e.BarcodeEAN13)
                    .IsRequired()
                     .HasMaxLength(13);

            builder.Property(e => e.BarcodeGS1_128)
                      .IsRequired()
                     .HasMaxLength(48);

            builder.Property(e => e.RFID)
                    .IsRequired()
                     .HasMaxLength(250);

            builder.HasOne(x => x.Box)
                    .WithOne(ad => ad.BoxCode)
                    .HasForeignKey<BoxCode>(ad => ad.BoxID);
        }
    }
}
