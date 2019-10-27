namespace MvcTemplate.Services.Data
{
    using System.Linq;
    using MvcTemplate.Data.Models;

    public interface IBookService
    {
        IQueryable<Book> GetTopBooks(int count);

        IQueryable<Book> GetLatestBooks(int count);

        IQueryable<Book> GetAllBooks();

        Book GetById(string id);

    }
}
