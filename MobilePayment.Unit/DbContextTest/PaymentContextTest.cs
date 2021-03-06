using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MobilePayment.Domain.Entities.Enums;
using MobilePayment.Infrastructure.Data;
using MobilePayment.Unit.Fakes;
using MobilePayment.Unit.Helpers;
using Xunit;

namespace MobilePayment.Unit.DbContextTest
{
    public class PaymentContextTest
    {
        protected readonly PaymentContext DbContext;

        public PaymentContextTest()
        {
            var factory = new ConnectionFactory();
            DbContext = factory.CreateContextForInMemory();
        }

        [Fact]
        public async Task Should_UsingSqliteInMemoryProvider_Success()
        {
            var transactions = await DbContext.Transactions.ToListAsync();
            transactions.Should().HaveCount(0);

            var operators = await DbContext.Operators.ToListAsync();
            operators.Should().HaveCount(0);
        }

        [Fact]
        public async Task Should_Success_Add_Transaction_With_Key_MobileOperator()
        {
            var mobileOperator = await DbContext.Operators.AddAsync(FakeEntity.GetMobileOperator());

            var transaction = FakeEntity.GetTransaction();
            transaction.AddMobileOperator(mobileOperator.Entity);

            var addedTransaction = DbContext.Transactions.AddAsync(transaction);
            await DbContext.SaveChangesAsync();

            addedTransaction.IsCompleted.Should().BeTrue();
            addedTransaction.Result.Entity.Id.Should().Be(1);
            addedTransaction.Result.Entity.MobileOperator.Id.Should().Be(1);
        }

        [Fact]
        public async Task Should_Success_Add_MobileOperator_With_Key_OperatorPrefix()
        {
            var addedMobileOperator = DbContext.Operators.AddAsync(FakeEntity.GetMobileOperator());

            var mobilePrefix = FakeEntity.GetMobilePrefix();
            mobilePrefix.AddMobileOperator(addedMobileOperator.Result.Entity);

            var addedMobilePrefix = await DbContext.Prefixes.AddAsync(mobilePrefix);
            await DbContext.SaveChangesAsync();

            addedMobilePrefix.Entity.Operator.Id.Should().Be(1);
            addedMobilePrefix.Entity.Operator.OperatorType.Should().Be(OperatorType.Active);
            addedMobilePrefix.Entity.Id.Should().Be(1);
        }
    }
}