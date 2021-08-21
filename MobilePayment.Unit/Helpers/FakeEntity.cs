using System;
using MobilePayment.Domain.Entities;
using MobilePayment.Domain.ValueObjects;

namespace MobilePayment.Unit.Helpers
{
    public static class FakeEntity
    {
        public static Transaction GetTransaction(int mobileOperatorId)
        {
            return new Transaction
            {
                Amount = new Amount(200),
                PhoneNumber = new PhoneNumber("7079239374"),
                Status = TransactionStatus.Success,
                CreationAt = DateTime.Now,
                MobileOperatorId = mobileOperatorId
            };
        }

        public static Domain.Entities.MobileOperator GetMobileOperator()
        {
            return new Domain.Entities.MobileOperator
            {
                OperatorInfo = new OperatorInfo("test"),
                OperatorType = OperatorType.Active
            };
        }
    }
}