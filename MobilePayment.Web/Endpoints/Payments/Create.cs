using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using MobilePayment.Application.Dtos;

namespace MobilePayment.Web.Endpoints.Payments
{
    public class Create : BaseAsyncEndpoint.WithRequest<CreatePaymentCommand>.WithResponse<CreatePaymentResult>
    {
        [HttpPost(CreatePaymentCommand.Route)]
        public override async Task<ActionResult<CreatePaymentResult>> HandleAsync(
            [FromBody] CreatePaymentCommand request,
            CancellationToken cancellationToken = new())
        {
            var validPayment = ValidPayment.From((request.PhoneNumber, request.Amount));
            return Ok(request);
        }
    }
}