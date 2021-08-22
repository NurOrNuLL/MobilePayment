using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MobilePayment.Application.Dtos;
using MobilePayment.Application.Services.MobileOperatorService;
using MobilePayment.Application.Services.MobileOperatorService.Interfaces;
using MobilePayment.Application.Services.MobileOperatorService.Operators;
using MobilePayment.Domain.Entities.Enums;
using Xunit;

namespace MobilePayment.Unit.Services
{
    public class MobileOperatorStrategyTest
    {
        private readonly IMobileOperatorStrategy _operatorStrategy;

        public MobileOperatorStrategyTest()
        {
            var services = new ServiceCollection();

            services.AddScoped<IMobileOperatorStrategy, MobileOperatorStrategy>();
            services.AddScoped<IMobileOperator, Active>();
            services.AddScoped<IMobileOperator, Altel>();
            services.AddScoped<IMobileOperator, Beeline>();
            services.AddScoped<IMobileOperator, Tele2>();

            var provider = services.BuildServiceProvider();
            _operatorStrategy = (IMobileOperatorStrategy)provider.GetRequiredService(typeof(IMobileOperatorStrategy));
        }

        [Fact]
        public async Task Should_OperatorType_Active()
        {
            var res = await _operatorStrategy.SendRequestAsync(
                ValidPayment.From(("1111111111", 220m)),
                OperatorType.Active);

            res.Value.Should().Be(OperatorType.Active);
        }

        [Fact]
        public async Task Should_OperatorType_Altel()
        {
            var res = await _operatorStrategy.SendRequestAsync(
                ValidPayment.From(("1111111111", 220m)),
                OperatorType.Altel);

            res.Value.Should().Be(OperatorType.Altel);
        }

        [Fact]
        public async Task Should_OperatorType_Beeline()
        {
            var res = await _operatorStrategy.SendRequestAsync(
                ValidPayment.From(("1111111111", 220m)),
                OperatorType.Beeline);

            res.Value.Should().Be(OperatorType.Beeline);
        }

        [Fact]
        public async Task Should_OperatorType_Tele2()
        {
            var res = await _operatorStrategy.SendRequestAsync(
                ValidPayment.From(("1111111111", 220m)),
                OperatorType.Tele2);

            res.Value.Should().Be(OperatorType.Tele2);
        }
    }
}