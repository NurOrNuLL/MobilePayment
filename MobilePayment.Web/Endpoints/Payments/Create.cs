using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using MobilePayment.Application.Dtos;
using MobilePayment.Application.Services.MobileOperatorService.Interfaces;
using MobilePayment.Application.Services.MobileTypeInspectorService.Interfaces;

namespace MobilePayment.Web.Endpoints.Payments
{
    public class Create : BaseAsyncEndpoint.WithRequest<CreatePaymentCommand>.WithResponse<CreatePaymentResult>
    {
        private readonly IOperatorTypeDetector _operatorTypeDetector;
        private readonly IMobileOperatorStrategy _operatorStrategy;

        public Create(
            IOperatorTypeDetector operatorTypeDetector,
            IMobileOperatorStrategy operatorStrategy)
        {
            _operatorTypeDetector = operatorTypeDetector;
            _operatorStrategy = operatorStrategy;
        }

        [HttpPost(CreatePaymentCommand.Route)]
        public override async Task<ActionResult<CreatePaymentResult>> HandleAsync(
            [FromBody] CreatePaymentCommand request,
            CancellationToken cancellationToken = new())
        {
            var validPayment = ValidPayment.From((request.PhoneNumber, request.Amount));
            var operatorType = await _operatorTypeDetector.GetMobileType(validPayment);
            var result = await _operatorStrategy.SendRequestAsync(validPayment, operatorType);

            return Ok(new CreatePaymentResult());
        }
    }
}