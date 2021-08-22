using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MobilePayment.Infrastructure.Data;

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
        }
    }
}