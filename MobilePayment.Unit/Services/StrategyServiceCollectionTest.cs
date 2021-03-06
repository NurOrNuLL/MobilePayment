using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MobilePayment.Application.Dtos;
using MobilePayment.Application.Services.MobileOperatorService;
using MobilePayment.Application.Services.MobileOperatorService.Interfaces;
using MobilePayment.Application.Services.MobileOperatorService.Operators;
using MobilePayment.Domain.Entities.Enums;
using MobilePayment.Domain.Repositories;
using MobilePayment.Unit.Fakes;
using Moq;
using Xunit;

namespace MobilePayment.Unit.Services
{
    public class StrategyServiceCollectionTest
    {
        private readonly IMobileOperatorStrategy _operatorStrategy;
        private readonly Mock<ILogger<MobileOperatorStrategy>> _mockLogger = new();

        public StrategyServiceCollectionTest()
        {
            var services = new ServiceCollection();

            services.AddScoped<ITransactionRepository, FakeTransactionRepository>();
            services.AddScoped<IMobileOperatorStrategy, MobileOperatorStrategy>();
            services.AddScoped<IMobileOperator, Active>();
            services.AddScoped<IMobileOperator, Altel>();
            services.AddScoped<IMobileOperator, Beeline>();
            services.AddScoped<IMobileOperator, Tele2>();
            services.AddSingleton(_ => _mockLogger.Object);

            var provider = services.BuildServiceProvider();
            _operatorStrategy = (IMobileOperatorStrategy)provider.GetRequiredService(typeof(IMobileOperatorStrategy));
        }

        [Fact]
        public async Task Should_OperatorType_Active()
        {
            var res = await _operatorStrategy.SendRequestAsync(
                ValidPayment.From(("1111111111", 220m)),
                OperatorType.Active);

            res.Value.Type.Should().Be(OperatorType.Active);
            res.Value.Status.Should().Be(TransactionStatus.Success);
        }

        [Fact]
        public async Task Should_OperatorType_Altel()
        {
            var res = await _operatorStrategy.SendRequestAsync(
                ValidPayment.From(("1111111111", 220m)),
                OperatorType.Altel);

            res.Value.Type.Should().Be(OperatorType.Altel);
            res.Value.Status.Should().Be(TransactionStatus.Success);
        }

        [Fact]
        public async Task Should_OperatorType_Beeline()
        {
            var res = await _operatorStrategy.SendRequestAsync(
                ValidPayment.From(("1111111111", 220m)),
                OperatorType.Beeline);

            res.Value.Type.Should().Be(OperatorType.Beeline);
            res.Value.Status.Should().Be(TransactionStatus.Success);
        }

        [Fact]
        public async Task Should_OperatorType_Tele2()
        {
            var res = await _operatorStrategy.SendRequestAsync(
                ValidPayment.From(("1111111111", 220m)),
                OperatorType.Tele2);

            res.Value.Type.Should().Be(OperatorType.Tele2);
            res.Value.Status.Should().Be(TransactionStatus.Success);
        }
    }
}