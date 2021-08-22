using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MobilePayment.Domain.Entities;
using MobilePayment.Domain.Entities.Enums;
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

                if (!context.Prefixes.Any())
                {
                    var beelineDictionary = await context.Operators.ToDictionaryAsync(k => k.OperatorType);
                    var operatorPrefixes = OperatorPrefixes(beelineDictionary);
                    await context.Prefixes.AddRangeAsync(operatorPrefixes);
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

        private static IEnumerable<OperatorPrefix> OperatorPrefixes(
            Dictionary<OperatorType, MobileOperator> beelineDictionary) 
        {
            var operatorPrefixes = new List<OperatorPrefix>();

            var active701 = new OperatorPrefix(Prefix.From("701"));
            active701.AddMobileOperator(beelineDictionary[OperatorType.Active]);
            operatorPrefixes.Add(active701);

            var beeline777 = new OperatorPrefix(Prefix.From("777"));
            beeline777.AddMobileOperator(beelineDictionary[OperatorType.Beeline]);
            operatorPrefixes.Add(beeline777);

            var beeline705 = new OperatorPrefix(Prefix.From("705"));
            beeline705.AddMobileOperator(beelineDictionary[OperatorType.Beeline]);
            operatorPrefixes.Add(beeline705);

            var tele707 = new OperatorPrefix(Prefix.From("707"));
            tele707.AddMobileOperator(beelineDictionary[OperatorType.Tele2]);
            operatorPrefixes.Add(tele707);

            var tele747 = new OperatorPrefix(Prefix.From("747"));
            tele747.AddMobileOperator(beelineDictionary[OperatorType.Tele2]);
            operatorPrefixes.Add(tele747);

            var altel700 = new OperatorPrefix(Prefix.From("700"));
            altel700.AddMobileOperator(beelineDictionary[OperatorType.Altel]);
            operatorPrefixes.Add(altel700);

            var altel708 = new OperatorPrefix(Prefix.From("708"));
            altel708.AddMobileOperator(beelineDictionary[OperatorType.Altel]);
            operatorPrefixes.Add(altel708);
            return operatorPrefixes;
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