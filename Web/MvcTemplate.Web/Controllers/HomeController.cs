namespace MvcTemplate.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using MvcTemplate.Common.Mapping;
    using MvcTemplate.Services.Data;
    using MvcTemplate.Web.ViewModels.Home;

    public class HomeController : BaseController
    {
        private IBookService books;

        public HomeController(IBookService books)
        {
            this.books = books;
        }

        public ActionResult Index()
        {
            var mostRatedBooks =
                this.Cache.Get("TopBooks", () => this.books.GetTopBooks(10).To<BookViewModel>().ToList(), 5 * 60);
            var latestUploadedBooks =
                this.Cache.Get("LatestUploadedBooks", () => this.books.GetLatestBooks(10).To<BookViewModel>().ToList(), 5 * 60);

            var viewModel = new IndexViewModel
            {
                TopRatedBooks = mostRatedBooks,
                LatestUploadedBooks = latestUploadedBooks,
            };

            return this.View(viewModel);
        }
    }
}
