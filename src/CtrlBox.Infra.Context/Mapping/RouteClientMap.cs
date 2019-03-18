using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CtrlBox.Infra.Context.Mapping
{
    public class RouteClientMap : IEntityTypeConfiguration<RouteClient>
    {
        public void Configure(EntityTypeBuilder<RouteClient> builder)
        {
            builder.ToTable("RoutesClients");

            builder.HasKey(t => new { t.ClientID, t.RouteID });

            builder.HasOne(tk => tk.Route)
                .WithMany(t => t.RoutesClients)
                .HasForeignKey(tk => tk.RouteID);

            builder.HasOne(tk => tk.Client)
                .WithMany(k => k.RoutesClients)
                .HasForeignKey(tk => tk.ClientID);
        }
    }
}
