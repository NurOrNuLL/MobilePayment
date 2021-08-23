using System;
using System.Threading.Tasks;
using MobilePayment.Domain.Entities;
using MobilePayment.Domain.Repositories;

namespace MobilePayment.Integration.Fakes
{
    public class FakeTransactionRepository : ITransactionRepository
    {
        public async Task<Transaction> AddAsync(Transaction entity)
        {
            await Task.Delay(TimeSpan.Zero);
            return entity;
        }

        public async Task UpdateAsync(Transaction entity)
        {
            await Task.Delay(TimeSpan.Zero);
        }
    }
}