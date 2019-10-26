
using MvcTemplate.Common.Mapping;
using MvcTemplate.Services.Data;
using MvcTemplate.Services.Web;
using MvcTemplate.Web.ViewModels.Home;
using System.Web.Mvc;

namespace MvcTemplate.Web.Controllers
{
    public class BooksController : BaseController
    {
        private IBookService books;
        private IIdentifierProvider identifierProvider;

        public BooksController(
            IIdentifierProvider identifierProvider,
            IBookService books)
        {
            this.books = books;
            this.identifierProvider = identifierProvider;
        }

        public ActionResult ById(string id)
        {
            var book = this.books.GetById(id);
            var viewModel = AutoMapperConfig.Configuration.CreateMapper().Map<BookViewModel>(book);
            return this.View(viewModel);
        }
    }
}