using EFCoreUnitTesting.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreUnitTesting.Models
{
    public class BookDepositoryContext : DbContext
    {
        public BookDepositoryContext(DbContextOptions<BookDepositoryContext> options) : base(options)
        {

        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
    }
}