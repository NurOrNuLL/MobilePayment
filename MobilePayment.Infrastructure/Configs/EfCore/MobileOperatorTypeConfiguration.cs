using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobilePayment.Domain.Entities;

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
        }
    }
}