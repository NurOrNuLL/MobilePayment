using System.Collections.Generic;
using MobilePayment.Domain.ValueObjects.Base;

namespace MobilePayment.Domain.ValueObjects
{
    public class PhoneNumber : ValueObject
    {
        public PhoneNumber()
        {
        }

        public PhoneNumber(string number)
        {
            Number = number;
        }

        public string Number { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Number;
        }
    }
}