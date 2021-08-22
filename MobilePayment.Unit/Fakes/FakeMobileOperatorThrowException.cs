using System;
using System.Threading.Tasks;
using MobilePayment.Application.Dtos;
using MobilePayment.Application.Exception;
using MobilePayment.Application.Services.MobileOperatorService.Interfaces;
using MobilePayment.Domain.Entities.Enums;

namespace MobilePayment.Unit.Fakes
{
    public class FakeMobileOperatorThrowException : IMobileOperator
    {
        public OperatorType OperatorType => OperatorType.Active;

        public async Task<MobileOperatorResult> SendRequestAsync(ValidPayment validPayment)
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            throw new MobileServerNotResponse(nameof(FakeMobileOperatorThrowException));
        }
    }
}