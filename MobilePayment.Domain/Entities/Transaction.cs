using System;
using MobilePayment.Domain.Entities.Base;
using MobilePayment.Domain.ValueObjects;

namespace MobilePayment.Domain.Entities
{
    public class Transaction : Entity
    {
        public PhoneNumber PhoneNumber { get; init; }
        public Amount Amount { get; init; }
        public DateTime CreationAt { get; init; }
        public TransactionStatus Status { get; init; }
        public int MobileOperatorId { get; init; }
        public MobileOperator MobileOperator { get; set; }

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