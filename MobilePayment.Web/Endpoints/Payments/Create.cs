using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using MobilePayment.Application.Dtos;
using MobilePayment.Application.Services.MobileTypeInspectorService.Interfaces;

namespace MobilePayment.Web.Endpoints.Payments
{
    public class Create : BaseAsyncEndpoint.WithRequest<CreatePaymentCommand>.WithResponse<CreatePaymentResult>
    {
        private readonly IOperatorTypeDetector _operatorTypeDetector;

        public Create(IOperatorTypeDetector operatorTypeDetector)
        {
            _operatorTypeDetector = operatorTypeDetector;
        }

        [HttpPost(CreatePaymentCommand.Route)]
        public override async Task<ActionResult<CreatePaymentResult>> HandleAsync(
            [FromBody] CreatePaymentCommand request,
            CancellationToken cancellationToken = new())
        {
            var validPayment = ValidPayment.From((request.PhoneNumber, request.Amount));
            var operatorType = await _operatorTypeDetector.GetMobileType(validPayment);
            return Ok(new CreatePaymentResult());
        }
    }
}