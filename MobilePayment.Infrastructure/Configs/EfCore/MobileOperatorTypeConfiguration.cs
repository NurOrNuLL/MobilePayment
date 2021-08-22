using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobilePayment.Domain.Entities;
using MobilePayment.Domain.Entities.Enums;

namespace MobilePayment.Infrastructure.Configs.EfCore
{
    public class MobileOperatorTypeConfiguration : IEntityTypeConfiguration<MobileOperator>
    {
        public void Configure(EntityTypeBuilder<MobileOperator> builder)
        {
            builder.ToTable("MobileOperator");
            builder.HasKey(o => o.Id);

            builder.OwnsOne(p => p.OperatorInfo)
                .Property(p => p.Value)
                .HasColumnName("Name")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.OperatorType)
                .HasConversion(
                    v => v.ToString(),
                    v => (OperatorType)Enum.Parse(typeof(OperatorType), v))
                .HasColumnName("OperatorType")
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}