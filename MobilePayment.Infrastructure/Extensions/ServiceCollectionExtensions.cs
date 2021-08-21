using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MobilePayment.Domain.Repositories;
using MobilePayment.Domain.Repositories.Base;
using MobilePayment.Infrastructure.Data;
using MobilePayment.Infrastructure.Repository;
using MobilePayment.Infrastructure.Repository.Base;

namespace MobilePayment.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PaymentContext>(builder =>
                builder.UseSqlite(configuration.GetConnectionString("SqLite")));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IMobileRepository, MobileOperatorRepository>();
        }
    }
}