using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MobileOperator.UnitTest.Helpers;
using MobilePayment.Domain.Entities;
using MobilePayment.Domain.ValueObjects;
using MobilePayment.Infrastructure.Data;
using MobilePayment.Infrastructure.Repository;
using Xunit;

namespace MobileOperator.UnitTest.Repository
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
            var list = await DbContext.Transactions.ToListAsync();
            list.Should().HaveCount(0);
        }

        [Fact]
        public async Task Should_Add_New_List_Transaction_To_TransactionRepository()
        {
            var repository = new TransactionRepository(DbContext);

            var addedTransaction = await repository.AddAsync(new Transaction
            {
                Amount = new Amount(200),
                PhoneNumber = new PhoneNumber("7079239374")
            });

            var transaction = await repository.GetByIdAsync(addedTransaction.Id);
            addedTransaction.Id.Should().Be(transaction.Id);
        }
    }
}