using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MobilePayment.Infrastructure.Data;
using MobilePayment.Infrastructure.Repository;
using MobilePayment.Unit.Helpers;
using Xunit;

namespace MobilePayment.Unit.Repository
{
    public class TransactionRepositoryTest
    {
        protected readonly PaymentContext DbContext;

        public TransactionRepositoryTest()
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
            var transactionRepository = new TransactionRepository(DbContext);
            var mobileRepository = new MobileOperatorRepository(DbContext);

            var addedOperator = await mobileRepository.AddAsync(FakeEntity.GetMobileOperator());
            var addedTransaction = await transactionRepository.AddAsync(FakeEntity.GetTransaction(addedOperator.Id));

            var transaction = await transactionRepository.GetByIdAsync(addedTransaction.Id);
            transaction.Should().BeEquivalentTo(addedTransaction);

            var mobileOperator = await mobileRepository.GetByIdAsync(addedOperator.Id);
            mobileOperator.Should().BeEquivalentTo(addedOperator);
        }
    }
}