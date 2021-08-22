using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using MobilePayment.Infrastructure.Data;
using MobilePayment.Infrastructure.Repository;
using MobilePayment.Unit.Helpers;
using Xunit;

namespace MobilePayment.Unit.DbContextTest
{
    public class OperatorPrefixRepositoryTest
    {
        protected readonly PaymentContext DbContext;

        public OperatorPrefixRepositoryTest()
        {
            var factory = new ConnectionFactory();
            DbContext = factory.CreateContextForInMemory();

            DbContext.Operators.AddRange(PaymentContextSeed.GetOperators());
            DbContext.SaveChanges();

            var beelineDictionary = DbContext.Operators.ToDictionary(k => k.OperatorType);
            var operatorPrefixes = PaymentContextSeed.OperatorPrefixes(beelineDictionary);
            DbContext.Prefixes.AddRangeAsync(operatorPrefixes);
            DbContext.SaveChanges();
        }

        [Fact]
        public async Task Should_Return_Correct_Prefixes_Dictionary()
        {
            var repository = new OperatorPrefixRepository(DbContext);
            var dictionary = await repository.GetPrefixesAsync();

            dictionary.Should().NotBeNull();
            dictionary.Keys.Should().HaveCount(7);
        }
    }
}