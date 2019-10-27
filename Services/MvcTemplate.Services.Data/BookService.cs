namespace MvcTemplate.Services.Data
{
    using System;
    using System.Linq;
    using MvcTemplate.Data.Common;
    using MvcTemplate.Data.Models;
    using MvcTemplate.Services.Web;

    public class BookService : IBookService
    {
        private IDbRepository<Book> books;
        private IIdentifierProvider identifierProvider;

        public BookService(
            IDbRepository<Book> books,
            IIdentifierProvider identifierProvider)
        {
            this.books = books;
            this.identifierProvider = identifierProvider;
        }

        public IQueryable<Book> GetAllBooks()
        {
            return this.books.All()
                .OrderBy(x => x.Title);
        }

        public Book GetById(string id)
        {
            var intId = this.identifierProvider.DecodeId(id);
            var book = this.books.GetById(intId);
            return book;
        }

        public IQueryable<Book> GetLatestBooks(int count)
        {
            return this.books.All()
                .OrderByDescending(x => x.CreatedOn)
                .Take(count);
        }

        public IQueryable<Book> GetTopBooks(int count)
        {
            return this.books.All()
                .OrderByDescending(x => x.Raiting)
                .Take(count);
        }

        public IQueryable<Book> GetRandomBooks(int count)
        {
            return this.books.All()
                 .OrderBy(x => Guid.NewGuid())
                 .Take(count);
        }

    }
}
