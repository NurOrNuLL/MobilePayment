using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MobilePayment.Domain.Entities;
using MobilePayment.Domain.ValueObjects;

namespace MobilePayment.Infrastructure.Data
{
    public class PaymentContextSeed
    {
        public static async Task SeedAsync(PaymentContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                await context.Database.MigrateAsync();
                await context.Database.EnsureCreatedAsync();

                if (!context.Operators.Any())
                {
                    context.Operators.AddRange(GetOperators());
                    await context.SaveChangesAsync();
                }

                if (!context.Transactions.Any())
                {
                    context.Transactions.Add(GetTransaction());
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception exception)
            {
                var log = loggerFactory.CreateLogger<PaymentContextSeed>();
                log.LogError(exception, "Возникла ошибка при добавлении фикстур");
                throw;
            }
        }

        private static IEnumerable<MobileOperator> GetOperators()
        {
            return new List<MobileOperator>
            {
                new() { OperatorInfo = new OperatorInfo("АО «Кселл»"), OperatorType = OperatorType.Active },
                new()
                {
                    OperatorInfo = new OperatorInfo("ТОО «Мобайл Телеком-Сервис»"), OperatorType = OperatorType.Altel
                },
                new() { OperatorInfo = new OperatorInfo("ТОО «КаР-Тел»"), OperatorType = OperatorType.Beeline },
                new()
                {
                    OperatorInfo = new OperatorInfo("ТОО «Мобайл Телеком-Сервис»"), OperatorType = OperatorType.Tele2
                },
            };
        }

        private static Transaction GetTransaction()
        {
            return new Transaction(
                new PhoneNumber("7079239374"),
                new Amount(200.25m),
                DateTime.Now,
                1,
                TransactionStatus.Success);
        }
    }
}