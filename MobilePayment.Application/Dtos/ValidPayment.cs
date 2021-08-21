using MobilePayment.Application.Exception;
using ValueOf;

namespace MobilePayment.Application.Dtos
{
    public class ValidPayment : ValueOf<(string phoneNumber, decimal amount), ValidPayment>
    {
        protected override void Validate()
        {
            if (string.IsNullOrEmpty(Value.phoneNumber))
            {
                throw new InvalidFieldException(nameof(Value.phoneNumber));
            }

            if (Value.phoneNumber.Length != 10)
            {
                throw new InvalidFieldException(nameof(Value.phoneNumber));
            }

            if (Value.amount == 0)
            {
                throw new InvalidFieldException(nameof(Value.amount));
            }
        }

        public string GetOperatorCode()
        {
            return Value.phoneNumber[..3];
        }
    }
}