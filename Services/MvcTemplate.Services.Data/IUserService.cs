namespace MvcTemplate.Services.Data
{
    using System.Linq;
    using MvcTemplate.Data.Models;

    public interface IUserService
    {
        void AddToShelf(Book book, ApplicationUser user);

        IQueryable<Book> BooksOnShelf(ApplicationUser user);

        bool IsBookOnShelf(Book book, ApplicationUser user);

        void RemoveFromShelf(Book book, ApplicationUser user);

        ApplicationUser UserByName(string userName);
    }
}
