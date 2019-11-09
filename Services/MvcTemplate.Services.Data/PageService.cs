namespace MvcTemplate.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using MvcTemplate.Data.Common;
    using MvcTemplate.Data.Models;

    public class PageService : IPageService
    {
        private IDbRepository<HtmlPagingItem> pages;

        public PageService(
            IDbRepository<HtmlPagingItem> pages)
        {
            this.pages = pages;
        }

        public IEnumerable<HtmlPagingItem> GetAllPages(int bookId)
        {
            return this.pages.All().Where(x => x.BookContentBookId == bookId);
        }
    }
}
