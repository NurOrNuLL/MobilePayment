using System.Threading.Tasks;
using FluentAssertions;
using MobilePayment.Domain.Entities.Enums;
using MobilePayment.Infrastructure.Data;
using MobilePayment.Infrastructure.Repository;
using MobilePayment.Unit.Fakes;
using MobilePayment.Unit.Helpers;
using Xunit;

namespace MobilePayment.Unit.DbContextTest
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
        public async Task Should_Added_Item_Success()
        {
            var repository = new TransactionRepository(DbContext);
            var transaction = await repository.AddAsync(FakeEntity.GetTransaction());
            transaction.Id.Should().Be(1);
        }

        [Fact]
        public async Task Should_Change_Status()
        {
            var repository = new TransactionRepository(DbContext);
            var transaction = await repository.AddAsync(FakeEntity.GetTransaction());
            transaction.ChangeStatus(TransactionStatus.Failure);

            var transactionNew = await DbContext.Transactions.FindAsync(transaction.Id);
            transactionNew.Status.Should().Be(TransactionStatus.Failure);
        }
    }
}