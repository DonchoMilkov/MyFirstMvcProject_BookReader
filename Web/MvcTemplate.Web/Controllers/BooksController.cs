namespace MvcTemplate.Web.Controllers
{
    using System.Web.Mvc;
    using MvcTemplate.Common.Mapping;
    using MvcTemplate.Services.Data;
    using MvcTemplate.Web.ViewModels.Home;

    public class BooksController : BaseController
    {
        private IBookService books;

        public BooksController(
            IBookService books)
        {
            this.books = books;
        }

        public ActionResult ById(string id)
        {
            var book = this.books.GetById(id);
            var viewModel = AutoMapperConfig.Configuration.CreateMapper().Map<BookViewModel>(book);
            return this.View(viewModel);
        }
    }
}
