using System;
using MobilePayment.Domain.Entities.Base;
using MobilePayment.Domain.ValueObjects;

namespace MobilePayment.Domain.Entities
{
    public class Transaction : Entity
    {
        public PhoneNumber PhoneNumber { get; }
        public Amount Amount { get; }
        public DateTime CreationAt { get; }
        public TransactionStatus Status { get; }
        public int MobileOperatorId { get; init; }
        public MobileOperator MobileOperator { get; init; }

        public Transaction()
        {
        }

        public Transaction(
            PhoneNumber phoneNumber,
            Amount amount,
            int mobileOperatorId,
            DateTime creationAt,
            TransactionStatus status = TransactionStatus.None)
        {
            PhoneNumber = phoneNumber;
            Amount = amount;
            CreationAt = creationAt;
            Status = status;
            MobileOperatorId = mobileOperatorId;
        }
    }
}