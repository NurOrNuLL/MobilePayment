using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using MobilePayment.Application.Dtos;
using MobilePayment.Application.Exception;
using MobilePayment.Application.Services.MobileTypeInspectorService;
using MobilePayment.Domain.Entities.Enums;
using MobilePayment.Domain.Repositories;
using Moq;
using Xunit;

namespace MobilePayment.Unit.Services
{
    public class OperatorTypeDetectorTest
    {
        private readonly OperatorTypeDetector _typeDetector;
        private readonly Mock<IOperatorPrefixRepository> _repositoryMock = new();
        private readonly Mock<ILogger<OperatorTypeDetector>> _loggerMock = new();

        public OperatorTypeDetectorTest()
        {
            _typeDetector = new OperatorTypeDetector(_repositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Should_Return_Correct_Type()
        {
            _repositoryMock.Setup(repository => repository.GetPrefixesAsync())
                .ReturnsAsync(() => new Dictionary<string, OperatorType>
                {
                    { "707", OperatorType.Tele2 }
                });

            var type = await _typeDetector.GetMobileTypeAsync(ValidPayment.From(("7077777777", 220m)));

            type.Should().Be(OperatorType.Tele2);
        }

        [Fact]
        public async Task Should_Throw_OperatorTypeNotFound()
        {
            _repositoryMock.Setup(repository => repository.GetPrefixesAsync())
                .ReturnsAsync(() => new Dictionary<string, OperatorType>
                {
                    { "701", OperatorType.Tele2 }
                });

            Func<Task> act = async () =>
                await _typeDetector.GetMobileTypeAsync(ValidPayment.From(("7077777777", 220m)));

            await act.Should().ThrowAsync<EntityNotFound>().WithMessage("707");
        }
    }
}