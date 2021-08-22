using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobilePayment.Domain.Entities;
using MobilePayment.Domain.Entities.Enums;

namespace MobilePayment.Infrastructure.Configs.EfCore
{
    public class TransactionTypeConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transaction");
            builder.HasKey(t => t.Id);

            builder
                .HasOne(p => p.MobileOperator)
                .WithMany(b => b.Transactions);

            builder.OwnsOne(p => p.PhoneNumber)
                .Property(p => p.Value)
                .HasColumnName("PhoneNumber")
                .IsRequired()
                .HasMaxLength(10);

            builder.OwnsOne(a => a.Amount)
                .Property(a => a.Value)
                .HasColumnName("Amount")
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (TransactionStatus)Enum.Parse(typeof(TransactionStatus), v))
                .HasColumnName("Status")
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}