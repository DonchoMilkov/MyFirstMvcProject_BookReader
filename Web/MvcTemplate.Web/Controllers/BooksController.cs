namespace MvcTemplate.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using MvcTemplate.Common.Mapping;
    using MvcTemplate.Services.Data;
    using MvcTemplate.Web.ViewModels.Home;
    using X.PagedList;

    [Authorize]
    public class BooksController : BaseController
    {
        private IBookService books;
        private ICategoryService bookCategories;
        private IPageService pages;

        public BooksController(
            IBookService books,
            ICategoryService bookCategories,
            IPageService pages)
        {
            this.books = books;
            this.bookCategories = bookCategories;
            this.pages = pages;
        }

        public ActionResult ById(string id, int? page, int? absolutePageNumber)
        {
            var book = this.books.GetById(id);
            var bookViewModel = AutoMapperConfig.Configuration.CreateMapper().Map<BookReadViewModel>(book);

            var viewModel = new ReadViewModel();
            viewModel.Book = bookViewModel;
            var allPages = this.pages.GetAllPages(book.Id);
            var firstPageId = allPages.First().Id;

            var products = allPages.Select(x => x.HtmlContent); //returns IQueryable<Product> representing an unknown number of products. a thousand maybe?

            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            var onePageOfProducts = products.ToPagedList(pageNumber, 1); // will only contain 25 products max because of the pageSize
            if (absolutePageNumber != null)
            {
                viewModel.Page = allPages.First(x => x.Id == absolutePageNumber);
            }
            else
            {
                viewModel.Page = allPages.First(x => x.Id == firstPageId + pageNumber - 1);
            }

            this.ViewBag.OnePageOfProducts = onePageOfProducts;

            return this.View(viewModel);
        }

        [AllowAnonymous]
        public ActionResult Review(string id)
        {
            var book = this.books.GetById(id);
            var viewModel = AutoMapperConfig.Configuration.CreateMapper().Map<BookViewModel>(book);
            return this.View(viewModel);
        }

        [AllowAnonymous]
        public ActionResult AllBooks(string categoryName, string search)
        {
            var bookList = new List<BookViewModel>();
            if (categoryName != null)
            {
                bookList = this.books.GetAllBooks().Where(x => x.Category.Name == categoryName).To<BookViewModel>().ToList();
            }
            else if (search != null)
            {
                bookList = this.books.GetAllBooks().Where(x => x.Title.ToLower().Contains(search.ToLower())).To<BookViewModel>().ToList();
            }
            else
            {
                bookList = this.books.GetAllBooks().To<BookViewModel>().ToList();
            }

            var categories =
                this.Cache.Get("categories", () => this.bookCategories.GetAll().To<BookCategoryViewModel>().ToList(), 30 * 60);

            var viewModel = new AllBooksViewModel
            {
                Books = bookList,
                BookCategories = categories,
            };

            return this.View(viewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetBookCoverImage(string id)
        {
            var book = this.books.GetById(id);
            var bookCover = book.Cover;
            var contentType = "image/jpeg";
            if (bookCover != null)
            {
                return this.File(bookCover, contentType);
            }
            else
            {
                return null;
            }
        }

        public ActionResult ByChapter(string bookId, string chapter)
        {
            var book = this.books.GetById(bookId);

            int navPage = this.pages.ChapterFirstPage(book, chapter);

            return this.RedirectToAction("ById", new { id = bookId, absolutePageNumber = navPage });
        }

    }
}
