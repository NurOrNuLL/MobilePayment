using System;
using System.Threading.Tasks;
using MobilePayment.Application.Dtos;
using MobilePayment.Application.Services.MobileOperatorService.Interfaces;
using MobilePayment.Domain.Entities.Enums;

namespace MobilePayment.Unit.Fakes
{
    public class FakeMobileOperatorSuccess : IMobileOperator
    {
        public OperatorType OperatorType => OperatorType.Beeline;

        public async Task<MobileOperatorResult> SendRequest(ValidPayment validPayment)
        {
            await Task.Delay(TimeSpan.Zero);
            return await Task.FromResult(MobileOperatorResult.From((OperatorType.Beeline, TransactionStatus.Success)));
        }
    }
}