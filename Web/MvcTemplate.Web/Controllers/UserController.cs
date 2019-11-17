namespace MvcTemplate.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using MvcTemplate.Common.Mapping;
    using MvcTemplate.Data.Models;
    using MvcTemplate.Services.Data;
    using MvcTemplate.Web.ViewModels.Home;

    [Authorize]
    public class UserController : Controller
    {
        private IUserService users;
        private IBookService books;

        public UserController(IUserService users, IBookService books)
        {
            this.users = users;
            this.books = books;
        }

        [HttpGet]
        public ActionResult MyShelf()
        {
            var currentUser = this.users.UserByName(this.User.Identity.Name);

            var viewModel = new BookShelfViewModel()
            {
                BooksOnShelf = this.users.BooksOnShelf(currentUser).To<BookViewModel>().ToList(),
            };

            return this.View(viewModel);
        }

        public ActionResult AddToMyShelf(string bookId)
        {
            var currentUser = this.users.UserByName(this.User.Identity.Name);
            var book = this.books.GetById(bookId);
            var check = this.users.IsBookOnShelf(book, currentUser);
            if (check)
            {
                this.ViewBag.Message = "Err! Book is already on shelf!";
            }
            else
            {
                this.users.AddToShelf(book, currentUser);
                this.ViewBag.Message = "Book successfully added to shelf!";
            }

            return this.RedirectToAction("MyShelf");
        }

        public ActionResult RemoveFromShelf(string bookId)
        {
            var currentUser = this.users.UserByName(this.User.Identity.Name);
            var book = this.books.GetById(bookId);
            var check = this.users.IsBookOnShelf(book, currentUser);

            if (check)
            {
                this.users.RemoveFromShelf(book, currentUser);
                this.ViewBag.Message = "Book is successfully removed from shelf!";
            }
            else
            {
                this.ViewBag.Message = "Err! Book is not on shelf!";
            }

            return this.RedirectToAction("MyShelf");
        }
    }
}
