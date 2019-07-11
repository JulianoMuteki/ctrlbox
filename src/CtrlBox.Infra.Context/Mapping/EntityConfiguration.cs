﻿using CtrlBox.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CtrlBox.Infra.Context.Mapping
{
    public abstract class EntityConfiguration<T> : IEntityTypeConfiguration<T> where T : EntityBase
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            Initialize(builder);
        }

        protected virtual void Initialize(EntityTypeBuilder<T> builder)
        {
            builder.Property(e => e.IsDelete)
              .IsRequired();

            builder.Property(e => e.IsDisable)
                    .IsRequired();

            builder.Property(e => e.CreationDate)
                    .IsRequired();

            builder.Property(e => e.DateModified)
                    .IsRequired();

            builder.Ignore(x => x.ComponentValidator);
        }
    }
}