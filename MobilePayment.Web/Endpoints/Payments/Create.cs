using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MobilePayment.Application.Dtos;
using MobilePayment.Application.Services.MobileOperatorService.Interfaces;
using MobilePayment.Application.Services.MobileTypeInspectorService.Interfaces;

namespace MobilePayment.Web.Endpoints.Payments
{
    public class Create : BaseAsyncEndpoint.WithRequest<CreatePaymentCommand>.WithResponse<CreatePaymentResult>
    {
        private readonly IOperatorTypeDetector _operatorTypeDetector;
        private readonly IMobileOperatorStrategy _operatorStrategy;
        private readonly ILogger<Create> _logger;

        public Create(
            IOperatorTypeDetector operatorTypeDetector,
            IMobileOperatorStrategy operatorStrategy,
            ILogger<Create> logger)
        {
            _operatorTypeDetector = operatorTypeDetector;
            _operatorStrategy = operatorStrategy;
            _logger = logger;
        }

        [HttpPost(CreatePaymentCommand.Route)]
        public override async Task<ActionResult<CreatePaymentResult>> HandleAsync(
            [FromBody] CreatePaymentCommand request,
            CancellationToken cancellationToken = new())
        {
            _logger.LogInformation("Request: {@Body}", request);

            var validPayment = ValidPayment.From((request.PhoneNumber, request.Amount));
            var operatorType = await _operatorTypeDetector.GetMobileTypeAsync(validPayment);
            var result = await _operatorStrategy.SendRequestAsync(validPayment, operatorType);

            return Ok(new CreatePaymentResult
            {
                Status = result.Value.Status.ToString(),
                OperatorName = result.Value.Type.ToString()
            });
        }
    }
}