﻿using CtrlBox.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CtrlBox.Infra.Context.Mapping
{
    public class ExpenseMap : EntityConfiguration<Expense>
    {
        protected override void Initialize(EntityTypeBuilder<Expense> builder)
        {
            base.Initialize(builder);

            builder.ToTable("Expenses");
            builder.Property(x => x.Id).HasColumnName("ExpenseID");
            builder.HasKey(b => b.Id).HasName("ExpenseID");

            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(e => e.Value)
                .IsRequired();
        }
    }
}
