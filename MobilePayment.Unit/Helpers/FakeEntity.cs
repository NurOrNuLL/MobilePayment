using System;
using MobilePayment.Domain.Entities;
using MobilePayment.Domain.ValueObjects;

namespace MobilePayment.Unit.Helpers
{
    public static class FakeEntity
    {
        public static Transaction GetTransaction()
        {
            return new Transaction(
                PhoneNumber.From("7079239374"),
                Amount.From(220.20m),
                DateTime.Now,
                TransactionStatus.Success);
        }

        public static MobileOperator GetMobileOperator()
        {
            return new MobileOperator(OperatorInfo.From("test"), OperatorType.Active);
        }
    }
}