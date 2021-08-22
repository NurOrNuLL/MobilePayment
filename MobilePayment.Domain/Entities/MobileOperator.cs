using System.Collections.Generic;
using MobilePayment.Domain.Entities.Base;
using MobilePayment.Domain.Entities.Enums;
using MobilePayment.Domain.ValueObjects;

namespace MobilePayment.Domain.Entities
{
    public class MobileOperator : Entity
    {
        public OperatorInfo OperatorInfo { get; }
        public OperatorType OperatorType { get; }

        // f-key
        public IEnumerable<OperatorPrefix> OperatorPrefixes { get; } = new List<OperatorPrefix>();
        public IEnumerable<Transaction> Transactions { get; } = new List<Transaction>();

        private MobileOperator()
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