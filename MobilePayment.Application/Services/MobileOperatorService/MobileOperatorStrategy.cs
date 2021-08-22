using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MobilePayment.Application.Dtos;
using MobilePayment.Application.Services.MobileOperatorService.Interfaces;
using MobilePayment.Domain.Entities.Enums;

namespace MobilePayment.Application.Services.MobileOperatorService
{
    public class MobileOperatorStrategy : IMobileOperatorStrategy
    {
        private readonly IEnumerable<IMobileOperator> _operators;

        public MobileOperatorStrategy(IEnumerable<IMobileOperator> operators)
        {
            _operators = operators;
        }

        public Task<MobileOperatorResult> SendRequestAsync(ValidPayment validPayment, OperatorType type)
        {
            return _operators.FirstOrDefault(x => x.OperatorType == type)?.SendRequest(validPayment);
        }
    }
}