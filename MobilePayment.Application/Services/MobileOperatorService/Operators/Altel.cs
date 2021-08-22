using System.Threading.Tasks;
using MobilePayment.Application.Dtos;
using MobilePayment.Application.Services.MobileOperatorService.Interfaces;
using MobilePayment.Domain.Entities.Enums;

namespace MobilePayment.Application.Services.MobileOperatorService.Operators
{
    public class Altel : IMobileOperator
    {
        public OperatorType OperatorType => OperatorType.Altel;

        public async Task<MobileOperatorResult> SendRequest(ValidPayment validPayment)
        {
            // return Failure if has error.
            return await Task.FromResult(MobileOperatorResult.From((OperatorType.Altel, TransactionStatus.Success)));
        }
    }
}