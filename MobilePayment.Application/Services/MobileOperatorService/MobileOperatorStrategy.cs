using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MobilePayment.Application.Dtos;
using MobilePayment.Application.Exception;
using MobilePayment.Application.Services.MobileOperatorService.Interfaces;
using MobilePayment.Domain.Entities;
using MobilePayment.Domain.Entities.Enums;
using MobilePayment.Domain.Repositories;
using MobilePayment.Domain.ValueObjects;

namespace MobilePayment.Application.Services.MobileOperatorService
{
    public class MobileOperatorStrategy : IMobileOperatorStrategy
    {
        private readonly IEnumerable<IMobileOperator> _operators;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ILogger<MobileOperatorStrategy> _logger;

        public MobileOperatorStrategy(
            IEnumerable<IMobileOperator> operators,
            ITransactionRepository transactionRepository,
            ILogger<MobileOperatorStrategy> logger)
        {
            _operators = operators;
            _transactionRepository = transactionRepository;
            _logger = logger;
        }

        public async Task<MobileOperatorResult> SendRequestAsync(ValidPayment validPayment, OperatorType type)
        {
            var transaction = await _transactionRepository.AddAsync(new Transaction(
                PhoneNumber.From(validPayment.Value.phoneNumber), Amount.From(200.20m), DateTime.Now)
            );

            _logger.LogInformation("Transaction created {Date}: {@Transaction}", DateTime.Now, transaction);

            var result = await _operators.First(x => x.OperatorType == type).SendRequestAsync(validPayment);

            if (result is null)
            {
                throw new EntityNotFound(nameof(IMobileOperator));
            }

            if (result.Value.Status.Equals(TransactionStatus.Failure))
            {
                transaction.ChangeStatus(TransactionStatus.Failure);
                await _transactionRepository.UpdateAsync(transaction);
                _logger.LogInformation("Transaction failure {Date}: {@Transaction}", DateTime.Now, transaction);
                return result;
            }

            transaction.ChangeStatus(TransactionStatus.Success);
            await _transactionRepository.UpdateAsync(transaction);
            _logger.LogInformation("Transaction success {Date}: {@Transaction}", DateTime.Now, transaction);
            return result;
        }
    }
}