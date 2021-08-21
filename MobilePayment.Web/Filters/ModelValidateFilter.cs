using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using MobilePayment.Web.Localize;

namespace MobilePayment.Web.Filters
{
    public class ModelValidateFilter : ActionFilterAttribute
    {
        private readonly IStringLocalizer<Resource> _localize;

        public ModelValidateFilter(IStringLocalizer<Resource> localize)
        {
            _localize = localize;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var list = context.ModelState.Values.SelectMany(modelState => modelState.Errors,
                    (_, error) => Regex.IsMatch(error.ErrorMessage, "JSON value", RegexOptions.IgnoreCase)
                        ? _localize.GetString("AmountNumberString").Value
                        : error.ErrorMessage).ToList();

                context.Result = new BadRequestObjectResult(new
                    { Title = _localize.GetString("ModelError").Value, Errors = list });
            }

            base.OnActionExecuting(context);
        }
    }
}