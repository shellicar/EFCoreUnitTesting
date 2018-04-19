using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using EFCoreUnitTesting.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EFCoreUnitTesting.Tests
{
    [TestClass]
    public class InMemoryContextFacts : ContextFactsBase
    {
        protected override DbContextOptionsBuilder<BookDepositoryContext> Builder()
        {
            var builder = new DbContextOptionsBuilder<BookDepositoryContext>();
            var databaseName = TestMethod();
            Console.WriteLine($"InMemory Database: {databaseName}");
            builder.UseInMemoryDatabase(databaseName);
            return builder;
        }

        protected string TestMethod()
        {
            var trace = new StackTrace();
            var method = trace.GetFrames().Select(x => x.GetMethod()).First(HasTestAttribute);
            return method.Name;
        }

        private bool HasTestAttribute(MethodBase method)
        {
            return method.CustomAttributes.Any(x => x.AttributeType == typeof(TestMethodAttribute));
        }
    }
}