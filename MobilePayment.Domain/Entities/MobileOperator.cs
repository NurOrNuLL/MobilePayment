using System.Collections.Generic;
using MobilePayment.Domain.Entities.Base;
using MobilePayment.Domain.ValueObjects;

namespace MobilePayment.Domain.Entities
{
    public class MobileOperator : Entity
    {
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

        public OperatorInfo OperatorInfo { get; init; }
        public OperatorType OperatorType { get; init; }

        public IList<Transaction> Transactions { get; set; }
    }
}