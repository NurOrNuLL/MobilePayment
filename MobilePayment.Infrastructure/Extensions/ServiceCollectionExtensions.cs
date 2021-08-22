using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MobilePayment.Domain.Repositories;
using MobilePayment.Infrastructure.Data;
using MobilePayment.Infrastructure.Repository;

namespace MobilePayment.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            // postgres
            services.AddDbContext<PaymentContext>(builder =>
                builder.UseNpgsql(configuration.GetConnectionString("Postgres"),
                    optionsBuilder => optionsBuilder.MigrationsAssembly("MobilePayment.Infrastructure")));


            services.AddScoped<IOperatorPrefixRepository, OperatorPrefixRepository>();
        }
    }
}