using System.Globalization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MobilePayment.Web.Filters
{
    public class LocalizationHeaderFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            context.HttpContext.Response.Headers.Add("Accept-Language", CultureInfo.CurrentCulture.Name.ToLower());
            base.OnActionExecuted(context);
        }
    }
}