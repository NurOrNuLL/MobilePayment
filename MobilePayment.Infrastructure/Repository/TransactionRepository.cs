﻿using MobilePayment.Domain.Entities;
using MobilePayment.Domain.Repositories;
using MobilePayment.Infrastructure.Data;
using MobilePayment.Infrastructure.Repository.Base;

namespace MobilePayment.Infrastructure.Repository
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(PaymentContext dbContext) : base(dbContext)
        {
        }
    }
}