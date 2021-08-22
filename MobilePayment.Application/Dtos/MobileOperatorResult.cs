using MobilePayment.Domain.Entities.Enums;
using ValueOf;

namespace MobilePayment.Application.Dtos
{
    public class MobileOperatorResult : ValueOf<(OperatorType, TransactionStatus), MobileOperatorResult>
    {
    }
}