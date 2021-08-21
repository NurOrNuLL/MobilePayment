using System;
using FluentAssertions;
using MobilePayment.Application.Dtos;
using MobilePayment.Application.Exception;
using Xunit;

namespace MobilePayment.Unit.ModelValidationTest
{
    public class ValidPaymentRequestValidation
    {
        [Fact]
        public void Should_Throw_Exception_For_PhoneNumber_Field_IsNullOrEmpty()
        {
            Action act = () => ValidPayment.From(("", decimal.Zero));
            act.Should().Throw<InvalidFieldException>().WithMessage("phoneNumber");
        }

        [Fact]
        public void Should_Throw_Exception_For_PhoneNumber_Field_Lenght()
        {
            Action actionLess = () => ValidPayment.From(("12312", decimal.Zero));
            Action actionMore = () => ValidPayment.From(("123122312312313321", decimal.Zero));
            actionLess.Should().Throw<InvalidFieldException>().WithMessage("phoneNumber");
            actionMore.Should().Throw<InvalidFieldException>().WithMessage("phoneNumber");
        }

        [Fact]
        public void Should_NotThrow_Exception_For_PhoneNumber_Field()
        {
            Action action = () => ValidPayment.From(("7878799874", 220m));
            action.Should().NotThrow<InvalidFieldException>();
        }

        [Fact]
        public void Should_Throw_Exception_For_Amount_Field()
        {
            Action action = () => ValidPayment.From(("7878799874", 0));
            action.Should().Throw<InvalidFieldException>().WithMessage("amount");
        }

        [Fact]
        public void Should_NotThrowThrow_Exception_For_Amount_Field()
        {
            Action action = () => ValidPayment.From(("7878799874", 0.20m));
            action.Should().NotThrow<InvalidFieldException>();
        }

        [Fact]
        public void Should_Get_OperatorCode()
        {
            var request = ValidPayment.From(("7878799874", 0.20m));
            request.GetOperatorCode().Should().Be("787");
        }
    }
}