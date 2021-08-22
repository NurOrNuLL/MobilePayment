using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MobilePayment.Application.Dtos;
using MobilePayment.Application.Exception;
using MobilePayment.Application.Services.MobileOperatorService;
using MobilePayment.Application.Services.MobileOperatorService.Interfaces;
using MobilePayment.Domain.Entities.Enums;
using MobilePayment.Unit.Helpers;
using Xunit;

namespace MobilePayment.Unit.Services
{
    public class MobileOperatorTest
    {
        private readonly IMobileOperatorStrategy _operatorStrategy;

        public MobileOperatorTest()
        {
            var services = new ServiceCollection();

            services.AddScoped<IMobileOperatorStrategy, MobileOperatorStrategy>();
            services.AddScoped<IMobileOperator, FakeMobileOperatorThrowException>();
            services.AddScoped<IMobileOperator, FakeMobileOperatorSuccess>();

            var provider = services.BuildServiceProvider();
            _operatorStrategy = (IMobileOperatorStrategy)provider.GetRequiredService(typeof(IMobileOperatorStrategy));
        }
        
        [Fact]
        public async Task Should_Get_Result()
        {
            var result = await _operatorStrategy.SendRequestAsync(ValidPayment.From(("1111111111", 200m)), OperatorType.Beeline);
            result.Value.Should().Be(OperatorType.Beeline);
        }

        [Fact]
        public async Task Should_Throw_MobileServerNotResponse_Exception()
        {
            Func<Task> act = async () => await _operatorStrategy.SendRequestAsync(ValidPayment.From(("1111111111", 200m)), OperatorType.Active);
            await act.Should().ThrowAsync<MobileServerNotResponse>().WithMessage(nameof(FakeMobileOperatorThrowException));
        }
    }
}