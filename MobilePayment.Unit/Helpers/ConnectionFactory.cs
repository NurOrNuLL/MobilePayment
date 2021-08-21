using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MobilePayment.Infrastructure.Data;

namespace MobileOperator.UnitTest.Helpers
{
    public class ConnectionFactory : IDisposable
    {
        private bool _disposedValue;
        public PaymentContext CreateContextForInMemory()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var option = new DbContextOptionsBuilder<PaymentContext>().UseSqlite(connection).Options;

            var context = new PaymentContext(option);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            return context;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (_disposedValue)
            {
                return;
            }

            _disposedValue = true;
        }
    }
}