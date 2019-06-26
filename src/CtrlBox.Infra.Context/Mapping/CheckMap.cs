﻿using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CtrlBox.Infra.Context.Mapping
{
    public class CheckMap : EntityConfiguration<Check>
    {
        protected override void Initialize(EntityTypeBuilder<Check> builder)
        {
            base.Initialize(builder);

            builder.ToTable("Checks");
            builder.Property(x => x.Id).HasColumnName("CheckID");
            builder.HasKey(b => b.Id).HasName("CheckID");

            builder.Property(e => e.Number)
                .IsRequired();

            builder.Property(e => e.DtExpire);

            builder.Property(e => e.Value)
                .IsRequired();
        }
    }
}
