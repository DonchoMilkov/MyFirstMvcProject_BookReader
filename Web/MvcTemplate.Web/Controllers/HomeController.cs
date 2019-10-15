namespace MvcTemplate.Web.Controllers
{
    using MvcTemplate.Common.Mapping;
    using MvcTemplate.Data.Common;
    using MvcTemplate.Data.Models;
    using MvcTemplate.Web.ViewModels.Home;
    using System.Linq;
    using System.Web.Mvc;

    public class HomeController : BaseController
    {
        private IDbRepository<Book> books;
        private IDbRepository<BookCategory> bookCategories;

        public HomeController(
            IDbRepository<Book> books,
            IDbRepository<BookCategory> bookCategories)
        {
            this.books = books;
            this.bookCategories = bookCategories;
        }

        public ActionResult Index()
        {
            var books = this.books.All()
                .Take(3)
                .To<BookViewModel>()
                .ToList();
            return this.View(books);
        }
    }
}
