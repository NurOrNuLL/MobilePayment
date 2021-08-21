using System.Collections.Generic;
using MobilePayment.Domain.ValueObjects.Base;

namespace MobilePayment.Domain.ValueObjects
{
    public class OperatorInfo : ValueObject
    {
        public OperatorInfo(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }
    }
}