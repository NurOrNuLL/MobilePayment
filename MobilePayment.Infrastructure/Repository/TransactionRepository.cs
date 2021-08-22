using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MobilePayment.Domain.Entities;
using MobilePayment.Domain.Repositories;
using MobilePayment.Infrastructure.Data;

namespace MobilePayment.Infrastructure.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly PaymentContext _paymentContext;

        public TransactionRepository(PaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<Transaction> AddAsync(Transaction entity)
        {
            var res = await _paymentContext.Transactions.AddAsync(entity);
            await _paymentContext.SaveChangesAsync();
            return res.Entity;
        }

        public async Task UpdateAsync(Transaction entity)
        {
            _paymentContext.Entry(entity).State = EntityState.Modified;
            await _paymentContext.SaveChangesAsync();
        }
    }
}