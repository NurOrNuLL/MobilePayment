using MobilePayment.Domain.Entities;
using MobilePayment.Domain.Repositories;
using MobilePayment.Infrastructure.Data;
using MobilePayment.Infrastructure.Repository.Base;

namespace MobilePayment.Infrastructure.Repository
{
    public class MobileOperatorRepository : Repository<MobileOperator>, IMobileRepository
    {
        protected MobileOperatorRepository(PaymentContext dbContext) : base(dbContext)
        {
        }
    }
}