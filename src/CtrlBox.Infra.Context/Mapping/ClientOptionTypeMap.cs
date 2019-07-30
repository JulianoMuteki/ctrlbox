using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CtrlBox.Infra.Context.Mapping
{
    public class ClientOptionTypeMap : IEntityTypeConfiguration<ClientOptionType>
    {
        public void Configure(EntityTypeBuilder<ClientOptionType> builder)
        {
            builder.ToTable("ClientsOptionsTypes");

            builder.HasKey(t => new { t.ClientID, t.OptiontTypeID });

            builder.HasOne(tk => tk.OptiontType)
                .WithMany(t => t.ClientsOptionsTypes)
                .HasForeignKey(tk => tk.OptiontTypeID);

            builder.HasOne(tk => tk.Client)
                .WithMany(k => k.ClientsOptionsTypes)
                .HasForeignKey(tk => tk.OptiontTypeID);
        }
    }
}