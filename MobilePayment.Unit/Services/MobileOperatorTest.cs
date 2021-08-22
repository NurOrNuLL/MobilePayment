using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using MobilePayment.Application.Dtos;
using MobilePayment.Application.Exception;
using MobilePayment.Application.Services.MobileOperatorService;
using MobilePayment.Application.Services.MobileOperatorService.Interfaces;
using MobilePayment.Domain.Entities.Enums;
using MobilePayment.Unit.Fakes;
using Moq;
using Xunit;

namespace MobilePayment.Unit.Services
{
    public class MobileOperatorTest
    {
        private readonly IMobileOperatorStrategy _operatorStrategy;
        private readonly Mock<ILogger<MobileOperatorStrategy>> _mockLogger = new();

        public MobileOperatorTest()
        {
            _operatorStrategy = new MobileOperatorStrategy(new List<IMobileOperator>
            {
                new FakeMobileOperatorFailure(),
                new FakeMobileOperatorSuccess(),
                new FakeMobileOperatorThrowException()
            }, new FakeTransactionRepository(), _mockLogger.Object);
        }

        [Fact]
        public async Task Should_Get_Success_Result()
        {
            var result =
                await _operatorStrategy.SendRequestAsync(ValidPayment.From(("1111111111", 200m)), OperatorType.Beeline);
            result.Value.Type.Should().Be(OperatorType.Beeline);
            result.Value.Status.Should().Be(TransactionStatus.Success);
        }

        [Fact]
        public async Task Should_Get_Failure_Result()
        {
            var result =
                await _operatorStrategy.SendRequestAsync(ValidPayment.From(("1111111111", 200m)), OperatorType.Altel);
            result.Value.Type.Should().Be(OperatorType.Altel);
            result.Value.Status.Should().Be(TransactionStatus.Failure);
        }

        [Fact]
        public async Task Should_Throw_MobileServerNotResponse_Exception()
        {
            Func<Task> act = async () =>
                await _operatorStrategy.SendRequestAsync(ValidPayment.From(("1111111111", 200m)), OperatorType.Active);
            await act.Should().ThrowAsync<MobileServerNotResponse>()
                .WithMessage(nameof(FakeMobileOperatorThrowException));
        }
    }
}