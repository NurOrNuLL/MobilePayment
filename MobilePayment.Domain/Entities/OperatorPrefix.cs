using MobilePayment.Domain.Entities.Base;
using MobilePayment.Domain.ValueObjects;

namespace MobilePayment.Domain.Entities
{
    public class OperatorPrefix : Entity
    {
        public Prefix Prefix { get; }
        public MobileOperator Operator { get; private set; }

        private OperatorPrefix()
        {
        }

        public OperatorPrefix(Prefix prefix)
        {
            Prefix = prefix;
        }

        public void AddMobileOperator(MobileOperator mobileOperator)
        {
            Operator = mobileOperator;
        }
    }
}