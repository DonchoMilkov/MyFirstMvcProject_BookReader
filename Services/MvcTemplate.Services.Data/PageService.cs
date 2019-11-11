namespace MvcTemplate.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using MvcTemplate.Data.Common;
    using MvcTemplate.Data.Models;

    public class PageService : IPageService
    {
        private IDbRepository<HtmlPagingItem> pages;
        private IDbRepository<NavigationItem> navItems;

        public PageService(
            IDbRepository<HtmlPagingItem> pages,
            IDbRepository<NavigationItem> navItems)
        {
            this.pages = pages;
            this.navItems = navItems;
        }

        public IEnumerable<HtmlPagingItem> GetAllPages(int bookId)
        {
            return this.pages.All().Where(x => x.BookContentBookId == bookId);
        }

        public int ChapterFirstPage(Book book, string chapter)
        {
            var chapterItem = this.navItems
                                .All()
                                .Where(x => x.BookContentBookId == book.Id)
                                .FirstOrDefault(x => x.Chapter == chapter);

            int pageId = chapterItem.HtmlPagingItemId;
            return pageId;
        }
    }
}
