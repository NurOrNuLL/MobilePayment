using System.Threading.Tasks;
using MobilePayment.Application.Dtos;
using MobilePayment.Domain.Entities.Enums;

namespace MobilePayment.Application.Services.MobileTypeInspectorService.Interfaces
{
    public interface IOperatorTypeDetector
    {
        public Task<OperatorType> GetMobileTypeAsync(ValidPayment payment);
    }
}