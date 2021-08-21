using System.Collections.Generic;
using MobilePayment.Domain.ValueObjects.Base;

namespace MobilePayment.Domain.ValueObjects
{
    public class Amount : ValueObject
    {
        public Amount(decimal money)
        {
            Money = money;
        }

        public decimal Money { get; private set; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Money;
        }
    }
}