using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using MobilePayment.Web.Dtos;
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
                var errors = context.ModelState.Values.SelectMany(modelState => modelState.Errors,
                    (_, error) => Regex.IsMatch(error.ErrorMessage, "JSON value", RegexOptions.IgnoreCase)
                        ? _localize.GetString("AmountNumberString").Value
                        : error.ErrorMessage).ToList();

                context.Result = new BadRequestObjectResult(new ErrorResponse
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    Title = _localize.GetString("ModelError"),
                    Errors = errors
                });
            }

            base.OnActionExecuting(context);
        }
    }
}