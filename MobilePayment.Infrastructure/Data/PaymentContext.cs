using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MobilePayment.Domain.Entities;

namespace MobilePayment.Infrastructure.Data
{
    public class PaymentContext : DbContext
    {
        public PaymentContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<MobileOperator> Operators { get; set; }
        public DbSet<OperatorPrefix> Prefixes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}