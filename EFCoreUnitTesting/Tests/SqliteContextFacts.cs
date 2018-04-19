using System;
using EFCoreUnitTesting.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EFCoreUnitTesting.Tests
{
    /// <summary>
    /// Creates the context using an Sqlite connection.
    /// </summary>
    /// <seealso cref="ContextFactsBase" />
    /// <seealso cref="IDisposable" />
    [TestClass]
    public class SqliteContextFacts : ContextFactsBase, IDisposable
    {
        public SqliteContextFacts()
        {
            SqliteConnection = new SqliteConnection("DataSource=:memory:");
            SqliteConnection.Open();
        }

        private SqliteConnection SqliteConnection { get; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected override DbContextOptionsBuilder<BookDepositoryContext> Builder()
        {
            var builder = new DbContextOptionsBuilder<BookDepositoryContext>();
            builder.UseSqlite(SqliteConnection);
            return builder;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                SqliteConnection?.Dispose();
            }
        }
    }
}