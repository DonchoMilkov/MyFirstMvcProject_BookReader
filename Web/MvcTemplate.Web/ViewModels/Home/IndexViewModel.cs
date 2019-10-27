namespace MvcTemplate.Web.ViewModels.Home
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public IEnumerable<BookViewModel> TopRatedBooks { get; set; }

        public IEnumerable<BookViewModel> LatestUploadedBooks { get; set; }
    }
}
