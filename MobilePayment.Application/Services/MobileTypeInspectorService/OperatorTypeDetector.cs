using System.Threading.Tasks;
using MobilePayment.Application.Dtos;
using MobilePayment.Application.Exception;
using MobilePayment.Application.Services.MobileTypeInspectorService.Interfaces;
using MobilePayment.Domain.Entities.Enums;
using MobilePayment.Domain.Repositories;

namespace MobilePayment.Application.Services.MobileTypeInspectorService
{
    public class OperatorTypeDetector : IOperatorTypeDetector
    {
        private readonly IOperatorPrefixRepository _prefixRepository;

        public OperatorTypeDetector(IOperatorPrefixRepository prefixRepository)
        {
            _prefixRepository = prefixRepository;
        }

        public async Task<OperatorType> GetMobileType(ValidPayment payment)
        {
            var prefixes = await _prefixRepository.GetPrefixesAsync();
            var prefix = payment.GetOperatorCode();

            if (prefixes.ContainsKey(prefix))
            {
                return prefixes[prefix];
            }

            throw new OperatorTypeNotFound(prefix);
        }
    }
}