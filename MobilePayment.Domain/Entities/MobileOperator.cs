using Microsoft.VisualBasic.CompilerServices;
using MobilePayment.Domain.Entities.Base;
using MobilePayment.Domain.ValueObjects;

namespace MobilePayment.Domain.Entities
{
    public class MobileOperator : Entity
    {
        public OperatorInfo OperatorInfo { get; set; }
        public Operators OperatorType { get; set; }
    }
}