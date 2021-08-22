using System.Collections.Generic;
using MobilePayment.Domain.Entities.Base;
using MobilePayment.Domain.ValueObjects;

namespace MobilePayment.Domain.Entities
{
    public class MobileOperator : Entity
    {
        public OperatorInfo OperatorInfo { get; }
        public OperatorType OperatorType { get; }

        public IEnumerable<Transaction> Transactions { get; } = new List<Transaction>();

        public MobileOperator()
        {
        }

        public MobileOperator(
            OperatorInfo operatorInfo,
            OperatorType operatorType)
        {
            OperatorInfo = operatorInfo;
            OperatorType = operatorType;
        }
    }
}