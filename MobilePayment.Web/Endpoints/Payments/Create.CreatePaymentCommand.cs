using System.ComponentModel.DataAnnotations;

namespace MobilePayment.Web.Endpoints.Payments
{
    public class CreatePaymentCommand
    {
        public const string Route = "/payment";

        [Required(ErrorMessage = "PhoneNumberRequired")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "InvalidPhoneNumber")]
        public string PhoneNumber { get; set; }

        [Range(1, 9999999999999999.99, ErrorMessage = "AmountNumberRequired")]
        public decimal Amount { get; set; }


        public override string ToString()
        {
            return $"PhoneNumber: {PhoneNumber}, Amount: {Amount}";
        }
    }
}