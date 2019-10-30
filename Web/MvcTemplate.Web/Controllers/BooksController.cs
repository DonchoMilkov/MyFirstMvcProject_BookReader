namespace MvcTemplate.Web.Controllers
{
    using System.Web;
    using System.Web.Mvc;
    using MvcTemplate.Common.Mapping;
    using MvcTemplate.Services.Data;
    using MvcTemplate.Services.Web;
    using MvcTemplate.Web.ViewModels.Home;

    public class BooksController : BaseController
    {
        private IBookService books;
        private IUploadBookService uploads;

        public BooksController(
            IBookService books,
            IUploadBookService uploads)
        {
            this.books = books;
            this.uploads = uploads;
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
        public ActionResult UploadBook()
        {
            return this.View();
        }

        public ActionResult Review(string id)
        {
            var book = this.books.GetById(id);
            var viewModel = AutoMapperConfig.Configuration.CreateMapper().Map<BookViewModel>(book);
            return this.View(viewModel);
        }
    }
}
