using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CtrlBox.Infra.Context.Mapping
{
    public class TrackingClientMap : IEntityTypeConfiguration<TrackingClient>
    {
        public void Configure(EntityTypeBuilder<TrackingClient> builder)
        {
            builder.ToTable("TrackingsClients");
            builder.HasKey(t => new { t.ClientID, t.TrackingID });

            builder.HasOne(tk => tk.Client)
                .WithMany(t => t.TrackingsClients)
                .HasForeignKey(tk => tk.ClientID);

            builder.HasOne(tk => tk.Tracking)
                .WithMany(k => k.BoxesTrackingClients)
                .HasForeignKey(tk => tk.TrackingID);
        }
    }
}
