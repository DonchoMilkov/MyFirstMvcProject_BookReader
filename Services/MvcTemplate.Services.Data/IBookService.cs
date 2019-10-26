namespace MvcTemplate.Services.Data
{
    using System.Linq;
    using MvcTemplate.Data.Models;

    public interface IBookService
    {
        IQueryable<Book> GetRandomBooks(int count);

        IQueryable<Book> GetAllBooks();

        Book GetById(string id);
    }
}
