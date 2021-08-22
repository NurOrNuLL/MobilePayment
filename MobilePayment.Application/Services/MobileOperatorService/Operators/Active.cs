using System.Threading.Tasks;
using MobilePayment.Application.Dtos;
using MobilePayment.Application.Services.MobileOperatorService.Interfaces;
using MobilePayment.Domain.Entities.Enums;

namespace MobilePayment.Application.Services.MobileOperatorService.Operators
{
    public class Active : IMobileOperator
    {
        public OperatorType OperatorType => OperatorType.Active;

        public async Task<MobileOperatorResult> SendRequest(ValidPayment validPayment)
        {
            // залогировать ошибку, вернуть статус ошибка.
            
            return await Task.FromResult(MobileOperatorResult.From((OperatorType.Active, TransactionStatus.Success)));
        }
    }
}