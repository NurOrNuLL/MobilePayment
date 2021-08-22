using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MobilePayment.Application.Exception;
using MobilePayment.Web.Dtos;
using MobilePayment.Web.Localize;

namespace MobilePayment.Web.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger;
        private readonly IStringLocalizer<Resource> _localize;

        public ExceptionFilter(ILogger<ExceptionFilter> logger, IStringLocalizer<Resource> localize)
        {
            _logger = logger;
            _localize = localize;
        }

        public void OnException(ExceptionContext context)
        {
            var response = context.HttpContext.Response;
            var exception = context.Exception;
            var id = Activity.Current?.Id ?? context.HttpContext.TraceIdentifier;

            _logger.LogWarning(exception, "Occured error {Name} by {@id}", exception.GetType().FullName, id);

            response.ContentType = "application/json";
            context.ExceptionHandled = true;

            if (exception is InvalidFieldException)
            {
                var badRequest = new ErrorResponse
                {
                    Id = id,
                    Code = (int)HttpStatusCode.BadRequest,
                    Title = _localize.GetString("ModelError").Value,
                    Errors = new[] { string.Format(_localize.GetString("InvalidField").Value, exception.Message) }
                };

                response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Result = new ObjectResult(badRequest);
            }


            if (exception is EntityNotFound)
            {
                var notFound = new ErrorResponse
                {
                    Id = id,
                    Code = (int)HttpStatusCode.NotFound,
                    Title = _localize.GetString("EntityNotFount").Value,
                    Errors = new[] { string.Format(_localize.GetString("EntityNotFoundElem").Value, exception.Message) }
                };

                response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Result = new ObjectResult(notFound);
            }


            var internalServerError = new ErrorResponse
            {
                Id = id,
                Code = (int)HttpStatusCode.InternalServerError,
                Title = _localize.GetString("InternalError").Value,
                Errors = new[] { _localize.GetString("InternalErrorDesc").Value }
            };

            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Result = new ObjectResult(internalServerError);
        }
    }
}