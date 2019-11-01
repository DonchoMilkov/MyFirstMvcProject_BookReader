namespace MvcTemplate.Web.ViewModels.Home
{
    using System.Collections.Generic;

    public class AllBooksViewModel
    {
        public IEnumerable<BookViewModel> Books { get; set; }

        public IEnumerable<BookCategoryViewModel> BookCategories { get; set; }
    }
}
