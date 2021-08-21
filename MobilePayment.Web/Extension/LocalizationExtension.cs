using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace MobilePayment.Web.Extension
{
    public static class LocalizationExtension
    {
        public static void AddLocalizationService(this IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("ru-kz"),
                    new CultureInfo("kk-kz"),
                };

                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                
                options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(context =>
                {
                    var languages = context.Request.Headers["Accept-Language"].ToString();
                    var currentLanguage = languages.Split(',').FirstOrDefault();

                    var defaultLanguage = string.IsNullOrEmpty(currentLanguage) ? "ru-kz" : currentLanguage;

                    if (defaultLanguage != "kk-kz" && defaultLanguage != "ru-kz")
                    {
                        defaultLanguage = "ru-kz";
                    }

                    return Task.FromResult(new ProviderCultureResult(defaultLanguage, defaultLanguage));
                }));
            });
        }

        public static void UseLocalization(this IApplicationBuilder app)
        {
            var localizationOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>()?.Value;
            app.UseRequestLocalization(localizationOptions);
        }
    }
}