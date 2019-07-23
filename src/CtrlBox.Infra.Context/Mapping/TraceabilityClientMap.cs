using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CtrlBox.Infra.Context.Mapping
{
    public class TraceabilityClientMap : IEntityTypeConfiguration<TraceabilityClient>
    {
        public void Configure(EntityTypeBuilder<TraceabilityClient> builder)
        {
            builder.ToTable("TracesClients");
            builder.HasKey(t => new { t.ClientID, t.TraceID });

            builder.HasOne(tk => tk.Client)
                .WithMany(t => t.TracesClients)
                .HasForeignKey(tk => tk.ClientID);

            builder.HasOne(tk => tk.Traceability)
                .WithMany(k => k.TraceabilitiesClients)
                .HasForeignKey(tk => tk.TraceID);
        }
    }
}
