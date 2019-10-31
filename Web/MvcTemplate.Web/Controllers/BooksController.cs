namespace MvcTemplate.Web.Controllers
{
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using MvcTemplate.Common;
    using MvcTemplate.Common.Mapping;
    using MvcTemplate.Services.Data;
    using MvcTemplate.Services.Web;
    using MvcTemplate.Web.ViewModels.Home;

    [Authorize]
    public class BooksController : BaseController
    {
        private IBookService books;
        private IUploadBookService uploads;
        private ICategoryService bookCategories;

        public BooksController(
            IBookService books,
            IUploadBookService uploads,
            ICategoryService bookCategories)
        {
            this.books = books;
            this.uploads = uploads;
            this.bookCategories = bookCategories;
        }

        public ActionResult ById(string id)
        {
            var book = this.books.GetById(id);
            var viewModel = AutoMapperConfig.Configuration.CreateMapper().Map<BookReadViewModel>(book);
            return this.View(viewModel);
        }

        [HttpPost]
        public ActionResult UploadBook(HttpPostedFileBase file)
        {
            var uploadStatus = this.uploads.UploadFile(file);
            this.ViewBag.Message = uploadStatus;
            return this.View();
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public ActionResult UploadBook()
        {
            return this.View();
        }

        [AllowAnonymous]
        public ActionResult Review(string id)
        {
            var book = this.books.GetById(id);
            var viewModel = AutoMapperConfig.Configuration.CreateMapper().Map<BookViewModel>(book);
            return this.View(viewModel);
        }

        [AllowAnonymous]
        public ActionResult AllBooks()
        {
            var books = this.books.GetAllBooks().To<BookViewModel>().ToList();
            var categories =
                this.Cache.Get("categories", () => this.bookCategories.GetAll().To<BookCategoryViewModel>().ToList(), 30 * 60);

            var viewModel = new AllBooksViewModel
            {
                Books = books,
                BookCategories = categories,
            };

            return this.View(viewModel);
        }
    }
}
