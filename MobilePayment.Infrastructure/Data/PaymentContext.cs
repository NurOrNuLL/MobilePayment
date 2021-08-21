using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobilePayment.Domain.Entities;
using MobilePayment.Domain.ValueObjects;

namespace MobilePayment.Infrastructure.Data
{
    public class PaymentContext : DbContext
    {
        public PaymentContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<MobileOperator> Operators { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Transaction>(ConfigureTransaction);
            builder.Entity<MobileOperator>(ConfigureMobileOperator);
        }

        private void ConfigureTransaction(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transaction");
            builder.HasKey(t => t.Id);

            builder.OwnsOne(p => p.PhoneNumber)
                .Property(p => p.Number)
                .HasColumnName("PhoneNumber")
                .IsRequired()
                .HasMaxLength(10);

            // type decimal for Sqlite
            builder.OwnsOne(a => a.Amount)
                .Property(a => a.Money)
                .HasColumnName("Amount")
                .IsRequired()
                .HasColumnType("DECIMAL(10,5)");
        }

        private void ConfigureMobileOperator(EntityTypeBuilder<MobileOperator> builder)
        {
            builder.ToTable("MobileOperator");
            builder.HasKey(o => o.Id);

            builder.OwnsOne(p => p.OperatorInfo)
                .Property(p => p.Name)
                .HasColumnName("OperatorInfo")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.OperatorType)
                .HasConversion(
                    v => v.ToString(),
                    v => (OperatorType)Enum.Parse(typeof(OperatorType), v))
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}