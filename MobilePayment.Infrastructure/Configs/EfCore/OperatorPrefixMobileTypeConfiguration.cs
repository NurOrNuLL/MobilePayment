using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobilePayment.Domain.Entities;

namespace MobilePayment.Infrastructure.Configs.EfCore
{
    public class OperatorPrefixMobileTypeConfiguration : IEntityTypeConfiguration<OperatorPrefix>
    {
        public void Configure(EntityTypeBuilder<OperatorPrefix> builder)
        {
            builder.ToTable("Prefixes");
            builder.HasKey(t => t.Id);

            builder.OwnsOne(p => p.Prefix)
                .Property(p => p.Value)
                .HasColumnName("Prefix")
                .IsRequired()
                .HasMaxLength(3);

            builder
                .HasOne(p => p.Operator)
                .WithMany(b => b.OperatorPrefixes);
        }
    }
}