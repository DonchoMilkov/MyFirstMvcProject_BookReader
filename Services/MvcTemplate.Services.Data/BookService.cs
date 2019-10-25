namespace MvcTemplate.Services.Data
{
    using System;
    using System.Linq;
    using MvcTemplate.Data.Common;
    using MvcTemplate.Data.Models;

    public class BookService : IBookService
    {
        private IDbRepository<Book> books;

        public BookService(IDbRepository<Book> books)
        {
            this.books = books;
        }

        public IQueryable<Book> GetAllBooks()
        {
            return this.books.All()
                .OrderBy(x => x.Title);
        }

        public IQueryable<Book> GetRandomBooks(int count)
        {
            return this.books.All()
                 .OrderBy(x => Guid.NewGuid())
                 .Take(count);
        }
    }
}
