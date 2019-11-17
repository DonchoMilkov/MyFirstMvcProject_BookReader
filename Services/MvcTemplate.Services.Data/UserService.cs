namespace MvcTemplate.Services.Data
{
    using System.Linq;
    using MvcTemplate.Data.Common;
    using MvcTemplate.Data.Models;

    public class UserService : IUserService
    {
        private IDbRepository<ApplicationUser> users;
        private IDbRepository<ApplicationUserBooks> userBooks;

        public UserService(
            IDbRepository<ApplicationUser> users,
            IDbRepository<ApplicationUserBooks> userBooks)
        {
            this.users = users;
            this.userBooks = userBooks;
        }

        public void AddToShelf(Book book, ApplicationUser user)
        {
            var userBook = new ApplicationUserBooks()
            {
                BookId = book.Id,
                ApplicationUserId = user.Id,
            };

            this.userBooks.Add(userBook);
            this.userBooks.Save();
        }

        public IQueryable<Book> BooksOnShelf(ApplicationUser user)
        {
            var userBooks = this.userBooks.All().Where(x => x.ApplicationUserId == user.Id).Select(x => x.Book);
            return userBooks;
        }

        public bool IsBookOnShelf(Book book, ApplicationUser user)
        {
           return this.userBooks.All().Any(x => x.BookId == book.Id && x.ApplicationUserId == user.Id);
        }

        public void RemoveFromShelf(Book book, ApplicationUser user)
        {
            var relation = this.userBooks.All().First(x => x.BookId == book.Id && x.ApplicationUserId == user.Id);
            this.userBooks.Delete(relation);
            this.userBooks.Save();
        }

        public ApplicationUser UserByName(string userName)
        {
            // TODO: handle possible exceptions
            var user = this.users.All().First(x => x.UserName.ToLower() == userName.ToLower());
            return user;
        }
    }
}
