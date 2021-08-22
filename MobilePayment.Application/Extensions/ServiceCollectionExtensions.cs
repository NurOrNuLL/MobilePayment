using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MobilePayment.Application.Services.MobileOperatorService;
using MobilePayment.Application.Services.MobileOperatorService.Interfaces;
using MobilePayment.Application.Services.MobileOperatorService.Operators;
using MobilePayment.Application.Services.MobileTypeInspectorService;
using MobilePayment.Application.Services.MobileTypeInspectorService.Interfaces;

namespace MobilePayment.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IOperatorTypeDetector, OperatorTypeDetector>();

            services.AddScoped<IMobileOperatorStrategy, MobileOperatorStrategy>();
            services.AddScoped<IMobileOperator, Active>();
            services.AddScoped<IMobileOperator, Altel>();
            services.AddScoped<IMobileOperator, Beeline>();
            services.AddScoped<IMobileOperator, Tele2>();
        }
    }
}