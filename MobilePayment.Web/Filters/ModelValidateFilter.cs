using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MobilePayment.Web.Dtos;
using MobilePayment.Web.Localize;

namespace MobilePayment.Web.Filters
{
    public class ModelValidateFilter : ActionFilterAttribute
    {
        private readonly IStringLocalizer<Resource> _localize;
        private readonly ILogger<ModelValidateFilter> _logger;

        public ModelValidateFilter(
            IStringLocalizer<Resource> localize,
            ILogger<ModelValidateFilter> logger)
        {
            _localize = localize;
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.SelectMany(modelState => modelState.Errors,
                    (_, error) => Regex.IsMatch(error.ErrorMessage, "JSON value", RegexOptions.IgnoreCase)
                        ? _localize.GetString("AmountNumberString").Value
                        : error.ErrorMessage).ToList();

                var response = new ErrorResponse
                {
                    Id = Activity.Current?.Id ?? context.HttpContext.TraceIdentifier,
                    Code = (int)HttpStatusCode.BadRequest,
                    Title = _localize.GetString("ModelError"),
                    Errors = errors
                };

                _logger.LogInformation("Model validation error: {@Response}", response);
                context.Result = new BadRequestObjectResult(response);
            }

            base.OnActionExecuting(context);
        }
    }
}