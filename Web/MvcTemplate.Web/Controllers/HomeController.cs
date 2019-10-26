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
        private ICategoryService bookCategories;

        public HomeController(
        IBookService books,
        ICategoryService bookCategories)
        {
            this.books = books;
            this.bookCategories = bookCategories;
        }

        public ActionResult Index()
        {
            var books = this.books.GetAllBooks().To<BookViewModel>().ToList();
            var categories =
                this.Cache.Get("categories", () => this.bookCategories.GetAll().To<BookCategoryViewModel>().ToList(), 30 * 60);

            var viewModel = new IndexViewModel
            {
               Books = books,
               BookCategories = categories,
            };

            return this.View(viewModel);
        }
    }
}
