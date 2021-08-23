using System;
using System.Threading.Tasks;
using MobilePayment.Application.Dtos;
using MobilePayment.Application.Services.MobileOperatorService.Interfaces;
using MobilePayment.Domain.Entities.Enums;

namespace MobilePayment.Integration.Fakes
{
    public class FakeMobileOperatorFailure : IMobileOperator
    {
        public OperatorType OperatorType => OperatorType.Altel;
        public async Task<MobileOperatorResult> SendRequestAsync(ValidPayment validPayment)
        {
            await Task.Delay(TimeSpan.Zero);
            return await Task.FromResult(MobileOperatorResult.From((OperatorType.Altel, TransactionStatus.Failure)));
        }
    }
}