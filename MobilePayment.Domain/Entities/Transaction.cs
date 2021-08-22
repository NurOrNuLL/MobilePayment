using System;
using MobilePayment.Domain.Entities.Base;
using MobilePayment.Domain.Entities.Enums;
using MobilePayment.Domain.ValueObjects;

namespace MobilePayment.Domain.Entities
{
    public class Transaction : Entity
    {
        public PhoneNumber PhoneNumber { get; }
        public Amount Amount { get; }
        public DateTime CreationAt { get; }
        public TransactionStatus Status { get; private set; }


        // f-key
        public MobileOperator MobileOperator { get; private set; }

        private Transaction()
        {
        }

        public Transaction(
            PhoneNumber phoneNumber,
            Amount amount,
            DateTime creationAt,
            TransactionStatus status = TransactionStatus.None)
        {
            PhoneNumber = phoneNumber;
            Amount = amount;
            CreationAt = creationAt;
            Status = status;
        }

        public void AddMobileOperator(MobileOperator mobileOperator)
        {
            MobileOperator = mobileOperator;
        }

        public void ChangeStatus(TransactionStatus status)
        {
            Status = status;
        }
    }
}