using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CtrlBox.Infra.Context.Mapping
{
    public class BoxTrackingClientMap : IEntityTypeConfiguration<BoxTrackingClient>
    {
        public void Configure(EntityTypeBuilder<BoxTrackingClient> builder)
        {
            builder.ToTable("BoxsTrackingsClients");
            builder.HasKey(t => new { t.ClientID, t.BoxTrackingID });

            builder.HasOne(tk => tk.Client)
                .WithMany(t => t.TracesClients)
                .HasForeignKey(tk => tk.ClientID);

            builder.HasOne(tk => tk.BoxTracking)
                .WithMany(k => k.BoxesTrackingClients)
                .HasForeignKey(tk => tk.BoxTrackingID);
        }
    }
}
