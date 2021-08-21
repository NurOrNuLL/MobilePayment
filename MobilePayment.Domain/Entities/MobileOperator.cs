﻿using System.Collections.Generic;
using MobilePayment.Domain.Entities.Base;
using MobilePayment.Domain.ValueObjects;

namespace MobilePayment.Domain.Entities
{
    public class MobileOperator : Entity
    {
        public OperatorInfo OperatorInfo { get; }
        public OperatorType OperatorType { get; }

        public IList<Transaction> Transactions { get; init; }
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