using FluentAssertions;
using MobilePayment.Unit.Helpers;
using MobilePayment.Web.Endpoints.Payments;
using Xunit;

namespace MobilePayment.Unit.ViewModelValidationTest
{
    public class CreatePaymentCommandValidation
    {
        [Fact]
        public void Should_Return_Invalid_Message_For_Phone_And_Amount_Fields()
        {
            var subject = new CreatePaymentCommand();
            var collection = Validation.ValidateModel(subject);
            collection.Should().NotBeEmpty().And.HaveCount(2);
        }

        [Fact]
        public void Should_Return_Success_Message_For_Phone_And_Amount_Fields()
        {
            var subject = new CreatePaymentCommand
            {
                Amount = 220.20m,
                PhoneNumber = "7079239374"
            };

            var collection = Validation.ValidateModel(subject);
            collection.Should().BeEmpty();
        }

        [Fact]
        public void Should_Return_Invalid_Message_For_Phone_Field()
        {
            var subject = new CreatePaymentCommand
            {
                Amount = 200,
                PhoneNumber = "sds"
            };

            var collection = Validation.ValidateModel(subject);
            collection.Should().NotBeEmpty().And.HaveCount(1);
            collection[0].ErrorMessage.Should().Be("InvalidPhoneNumber");
        }

        [Fact]
        public void Should_Return_Required_Message_For_Phone_Field()
        {
            var subject = new CreatePaymentCommand
            {
                Amount = 200,
            };

            var collection = Validation.ValidateModel(subject);
            collection.Should().NotBeEmpty().And.HaveCount(1);
            collection[0].ErrorMessage.Should().Be("PhoneNumberRequired");
        }

        [Fact]
        public void Should_Return_Required_Message_For_Amount_Field()
        {
            var subject = new CreatePaymentCommand
            {
                PhoneNumber = "7079239374"
            };

            var collection = Validation.ValidateModel(subject);
            collection.Should().NotBeEmpty().And.HaveCount(1);
            collection[0].ErrorMessage.Should().Be("AmountNumberRequired");
        }

        [Fact]
        public void Should_Return_Required_Message_For_Amount_Field_Value_Zero()
        {
            var subject = new CreatePaymentCommand
            {
                PhoneNumber = "7079239374",
                Amount = decimal.MinValue
            };

            var collection = Validation.ValidateModel(subject);
            collection.Should().NotBeEmpty().And.HaveCount(1);
            collection[0].ErrorMessage.Should().Be("AmountNumberRequired");
        }
    }
}