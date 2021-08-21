using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace MobilePayment.Web.Endpoints.Payments
{
    public class Create : BaseAsyncEndpoint.WithRequest<CreatePaymentCommand>.WithResponse<CreatePaymentResult>
    {
        [HttpPost(CreatePaymentCommand.Route)]
        public override async Task<ActionResult<CreatePaymentResult>> HandleAsync(
            [FromBody] CreatePaymentCommand request,
            CancellationToken cancellationToken = new())
        {
            var result = await Task.FromResult(new CreatePaymentResult());
            return Ok(request);
        }
    }
}