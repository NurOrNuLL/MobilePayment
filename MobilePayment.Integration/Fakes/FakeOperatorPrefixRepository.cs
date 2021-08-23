using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MobilePayment.Domain.Entities.Enums;
using MobilePayment.Domain.Repositories;

namespace MobilePayment.Integration.Fakes
{
    public class FakeOperatorPrefixRepository : IOperatorPrefixRepository
    {
        public async Task<Dictionary<string, OperatorType>> GetPrefixesAsync()
        {
            await Task.Delay(TimeSpan.Zero);
            return new Dictionary<string, OperatorType>
            {
                { "505", OperatorType.Altel },
                { "506", OperatorType.Beeline },
                { "507", OperatorType.Active },
            };
        }
    }
}