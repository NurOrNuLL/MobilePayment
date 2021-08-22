using System.Threading.Tasks;
using MobilePayment.Application.Dtos;
using MobilePayment.Application.Services.MobileOperatorService.Interfaces;
using MobilePayment.Domain.Entities.Enums;

namespace MobilePayment.Application.Services.MobileOperatorService.Operators
{
    public class Altel : IMobileOperator
    {
        public OperatorType OperatorType => OperatorType.Altel;

        public async Task<MobileOperatorResult> SendRequestAsync(ValidPayment validPayment)
        {
            // залогировать response и результат, вернуть статус ошибка.
            return await Task.FromResult(MobileOperatorResult.From((OperatorType.Altel, TransactionStatus.Success)));        }
    }
}