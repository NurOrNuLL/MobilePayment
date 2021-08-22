using System.Threading.Tasks;
using MobilePayment.Domain.Entities;

namespace MobilePayment.Domain.Repositories
{
    public interface ITransactionRepository
    {
        Task<Transaction> AddAsync(Transaction entity);
        Task UpdateAsync(Transaction entity);
    }
}