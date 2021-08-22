using Microsoft.Extensions.DependencyInjection;
using MobilePayment.Web.Filters;
using MobilePayment.Web.Localize;

namespace MobilePayment.Web.Extension
{
    public static class WebExtension
    {
        public static void AddWebControllers(this IServiceCollection services)
        {
            services.AddControllers(options =>
                {
                    options.Filters.Add(typeof(ExceptionFilter));
                    options.Filters.Add(typeof(ModelValidateFilter));
                    options.Filters.Add(typeof(LocalizationHeaderFilter));
                })
                .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true)
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (_, factory) => factory.Create(typeof(Resource));
                });

            services.AddScoped<ExceptionFilter>();
            services.AddScoped<ModelValidateFilter>();
            services.AddScoped<LocalizationHeaderFilter>();
        }
    }
}