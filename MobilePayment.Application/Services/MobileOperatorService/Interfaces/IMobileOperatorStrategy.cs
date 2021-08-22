using System.Threading.Tasks;
using MobilePayment.Application.Dtos;
using MobilePayment.Domain.Entities.Enums;

namespace MobilePayment.Application.Services.MobileOperatorService.Interfaces
{
    public interface IMobileOperatorStrategy
    {
        Task<MobileOperatorResult> SendRequestAsync(ValidPayment validPayment, OperatorType type);
    }
}