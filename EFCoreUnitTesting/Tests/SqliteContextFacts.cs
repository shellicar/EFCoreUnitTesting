using System;
using EFCoreUnitTesting.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EFCoreUnitTesting.Tests
{
    [TestClass]
    public class SqliteContextFacts : ContextFactsBase, IDisposable
    {
        public SqliteContextFacts()
        {
            SqliteConnection = new SqliteConnection("DataSource=:memory:");
            SqliteConnection.Open();
        }


        protected override DbContextOptionsBuilder<BookDepositoryContext> Builder()
        {
            var builder = new DbContextOptionsBuilder<BookDepositoryContext>();
            builder.UseSqlite(SqliteConnection);
            return builder;
        }

        private SqliteConnection SqliteConnection { get; }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                SqliteConnection?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}