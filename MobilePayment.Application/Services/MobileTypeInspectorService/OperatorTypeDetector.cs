using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<OperatorTypeDetector> _logger;

        public OperatorTypeDetector(
            IOperatorPrefixRepository prefixRepository,
            ILogger<OperatorTypeDetector> logger)
        {
            _prefixRepository = prefixRepository;
            _logger = logger;
        }

        public async Task<OperatorType> GetMobileTypeAsync(ValidPayment payment)
        {
            var prefixes = await _prefixRepository.GetPrefixesAsync();
            var prefix = payment.GetOperatorCode();
            
            if (!prefixes.ContainsKey(prefix))
            {
                throw new EntityNotFound(prefix);
            }

            var type = prefixes[prefix];
            
            _logger.LogInformation("Search by type '{prefix}', founded: {@type} ", prefix, type.ToString());
            return type;
        }
    }
}