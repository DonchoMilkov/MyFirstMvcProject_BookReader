namespace MvcTemplate.Web.ViewModels.Home
{
    using MvcTemplate.Data.Models;

    public class ReadViewModel
    {
        public HtmlPagingItem Page { get; set; }

        public BookReadViewModel Book { get; set; }

    }
}
