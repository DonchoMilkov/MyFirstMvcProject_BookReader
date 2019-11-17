namespace MvcTemplate.Web.ViewModels.Home
{
    using System.Collections.Generic;

    public class BookShelfViewModel
    {
        public IEnumerable<BookViewModel> BooksOnShelf { get; set; }
    }
}
