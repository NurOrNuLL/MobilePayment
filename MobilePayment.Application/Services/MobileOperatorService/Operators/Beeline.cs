using System.Threading.Tasks;
using MobilePayment.Application.Dtos;
using MobilePayment.Application.Services.MobileOperatorService.Interfaces;
using MobilePayment.Domain.Entities.Enums;

namespace MobilePayment.Application.Services.MobileOperatorService.Operators
{
    public class Beeline : IMobileOperator
    {
        public OperatorType OperatorType => OperatorType.Beeline;

        public async Task<MobileOperatorResult> SendRequest(ValidPayment validPayment)
        {
            return await Task.FromResult(MobileOperatorResult.From(OperatorType.Beeline));
        }
    }
}