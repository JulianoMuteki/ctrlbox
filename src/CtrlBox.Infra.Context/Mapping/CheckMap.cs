using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CtrlBox.Infra.Context.Mapping
{
    public class CheckMap : IEntityTypeConfiguration<Check>
    {
        public void Configure(EntityTypeBuilder<Check> builder)
        {
            builder.ToTable("Checks");

            builder.HasKey(e => e.Id).HasName("CheckID");

            builder.Property(e => e.CreationDate)
                .IsRequired();

            builder.Property(e => e.DateModified)
                .IsRequired();

            builder.Property(e => e.Number)
                .IsRequired();

            builder.Property(e => e.DtExpire);
                
            builder.Property(e => e.Value)
                .IsRequired();

        }
    }
}
