using MobilePayment.Domain.Entities;
using MobilePayment.Domain.Repositories.Base;

namespace MobilePayment.Domain.Repositories
{
    public interface IMobileRepository : IRepository<MobileOperator>
    {
    }
}