using System;
using System.Collections.Generic;
using System.Linq;
using EFCoreUnitTesting.Models;
using EFCoreUnitTesting.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EFCoreUnitTesting.Tests
{
    public abstract class ContextFactsBase
    {
        protected BookDepositoryContext Context()
        {
            var options = Options();
            var context = new BookDepositoryContext(options);
            context.Database.EnsureCreated();
            return context;
        }

        private DbContextOptions<BookDepositoryContext> Options()
        {
            return Builder().Options;
        }

        protected abstract DbContextOptionsBuilder<BookDepositoryContext> Builder();

        /// <summary>
        /// Expected: error when inserting into database without title.<br />
        /// InMemoryResult: fails test, not relational.<br />
        /// Sqlite: passes test, relational
        /// </summary>
        [TestMethod]
        public void Cant_insert_book_without_title()
        {
            using (var context = Context())
            {
                context.Books.Add(new Book());

                void When()
                {
                    context.SaveChanges();
                }

                Assert.ThrowsException<DbUpdateException>((Action)When);
            }
        }

        /// <summary>
        /// Expected: contains 2 BookAuthors, but Author is null in both.<br />
        /// Results: both InMemory and Sqlite pass this test.
        /// </summary>
        [TestMethod]
        public void Includes_bookauthors_when_including()
        {
            using (var context = Context())
            {
                GivenBookByTwoAuthors(context);
            }

            using (var context = Context())
            {
                var source = context.Books.Include(x => x.BookAuthors);

                var book = source.Single();
                Assert.IsNull(book.BookAuthors.First().Author);
            }
        }

        /// <summary>
        /// Expected: using the same context means all navigation properties are set if that's how the entities were created.
        /// Results: both InMemory and Sqlite pass this test. This is generally not the desired result for testing however.
        /// </summary>
        [TestMethod]
        public void Includes_everything_when_using_same_context()
        {
            using (var context = Context())
            {
                GivenBookByTwoAuthors(context);

                var source = context.Books;

                var book = source.Single();
                Assert.IsNotNull(book.BookAuthors.First().Author);
            }
        }

        /// <summary>
        /// Expected: have to include both BookAuthors table and other relational entity to populate the navigation properties.<br />
        /// Results: both InMemory and Sqlite pass this test.
        /// </summary>
        [TestMethod]
        public void Includes_author_when_specifying()
        {
            using (var context = Context())
            {
                GivenBookByTwoAuthors(context);
            }

            using (var context = Context())
            {
                var source = context.Books.Include(x => x.BookAuthors).ThenInclude(x => x.Author);

                var book = source.Single();
                Assert.IsNotNull(book.BookAuthors.First().Author);
            }
        }

        /// <summary>
        /// Expected: if not including BookAuthors, then the property is null.<br />
        /// Results: both InMemory and Sqlite pass this test.
        /// </summary>
        [TestMethod]
        public void Doesnt_include_author_by_default()
        {
            using (var context = Context())
            {
                GivenBookByTwoAuthors(context);
            }

            using (var context = Context())
            {
                var source = context.Books;

                var book = source.Single();
                Assert.IsNull(book.BookAuthors);
            }
        }

        /// <summary>
        /// Creates a book written by two authors and inserts it into the context.
        /// </summary>
        /// <param name="context">The context.</param>
        private static void GivenBookByTwoAuthors(BookDepositoryContext context)
        {
            var book = new Book
            {
                Title = "Daughter of the Empire"
            };
            var author1 = new Author
            {
                Name = "Janny Wurts"
            };
            var author2 = new Author
            {
                Name = "Raymond E. Feist"
            };

            book.BookAuthors = new List<BookAuthor>
            {
                new BookAuthor {Book = book, Author = author1},
                new BookAuthor {Book = book, Author = author2},
            };

            context.Books.Add(book);
            context.SaveChanges();
        }
    }
}
