using MobilePayment.Domain.Entities.Base;
using MobilePayment.Domain.ValueObjects;

namespace MobilePayment.Domain.Entities
{
    public class Transaction : Entity
    {
        public PhoneNumber PhoneNumber { get; set; }
        public Amount Amount { get; set; }
    }
}