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
                    var transaction = new Transaction(
                        PhoneNumber.From("7079239374"),
                        Amount.From(200.20m),
                        DateTime.Now,
                        TransactionStatus.Success);

                    var mobileOperator = await context.Operators.FindAsync(1);
                    transaction.AddMobileOperator(mobileOperator);

                    await context.Transactions.AddAsync(transaction);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception exception)
            {
                var log = loggerFactory.CreateLogger<PaymentContextSeed>();
                log.LogError(exception, "Возникла ошибка при добавлении данных в БД");
                throw;
            }
        }
        private static IEnumerable<MobileOperator> GetOperators()
        {
            return new List<MobileOperator>
            {
                new(OperatorInfo.From("АО «Кселл»"), OperatorType.Active),
                new(OperatorInfo.From("ТОО «Мобайл Телеком-Сервис»"), OperatorType.Beeline),
                new(OperatorInfo.From("ТОО «КаР-Тел»"), OperatorType.Altel),
                new(OperatorInfo.From("ТОО «Мобайл Телеком-Сервис»"), OperatorType.Tele2)
            };
        }
    }
}