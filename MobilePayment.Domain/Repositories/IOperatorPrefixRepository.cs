using System.Collections.Generic;
using System.Threading.Tasks;
using MobilePayment.Domain.Entities.Enums;

namespace MobilePayment.Domain.Repositories
{
    public interface IOperatorPrefixRepository
    {
        Task<Dictionary<string, OperatorType>> GetPrefixesAsync();
    }
}