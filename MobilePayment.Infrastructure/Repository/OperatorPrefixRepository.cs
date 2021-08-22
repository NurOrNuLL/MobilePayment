using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MobilePayment.Domain.Entities.Enums;
using MobilePayment.Domain.Repositories;
using MobilePayment.Infrastructure.Data;

namespace MobilePayment.Infrastructure.Repository
{
    public class OperatorPrefixRepository : IOperatorPrefixRepository
    {
        private readonly PaymentContext _context;

        public OperatorPrefixRepository(PaymentContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<string, OperatorType>> GetPrefixesAsync()
        {
            return await _context.Prefixes.Include(p => p.Operator)
                .Select(p => new { p.Prefix.Value, p.Operator.OperatorType })
                .ToDictionaryAsync(k => k.Value, v => v.OperatorType);
        }
    }
}