namespace MvcTemplate.Services.Data
{
    using System;
    using System.Collections.Generic;
    using MvcTemplate.Data.Common;
    using MvcTemplate.Data.Models;
    using VersOne.Epub;

    public class ContentParseService : IContentParseService
    {
        private IDbRepository<BookContent> contents;
        private IDbRepository<NavigationItem> navigationItems;
        private IDbRepository<HtmlPagingItem> pagingItems;

        public ContentParseService(
            IDbRepository<BookContent> contents,
            IDbRepository<NavigationItem> navigationItems,
            IDbRepository<HtmlPagingItem> pagingItems)
        {
            this.contents = contents;
            this.navigationItems = navigationItems;
            this.pagingItems = pagingItems;
        }

        public void ParseContent(EpubBook epubBook, Book book)
        {
            var content = new BookContent();
            content.Book = book;
            content.BookId = book.Id;
            content.StyleSheet = this.GetStyles(epubBook);
            this.contents.Add(content);

            var pagings = this.ParsePages(epubBook, content);

            this.ParseNavigation(epubBook, content, pagings);

            book.BookContent = content;

            return;
        }

        private string GetStyles(EpubBook epubBook)
        {
            string innerStyleTag = string.Empty;
            foreach (var css in epubBook.Content.Css.Values)
            {
                innerStyleTag = css.Content + Environment.NewLine;
            }

            var styleTag = string.Format("{0}{1}{2}", "<style>", innerStyleTag, "</style>");
            return styleTag;
        }

        private List<HtmlPagingItem> ParsePages(EpubBook epubBook, BookContent content)
        {
            var pagings = new List<HtmlPagingItem>();
            foreach (var item in epubBook.Content.Html)
            {
                var key = item.Key.ToLower().Trim();
                string htmlContent, keyPart;

                string inputHtml = item.Value.Content;
                string[] bodyTags = { "<body", "/body>" };
                var piecesHtml = inputHtml.Split(bodyTags, StringSplitOptions.RemoveEmptyEntries);

                if (piecesHtml[1] != null)
                {
                    keyPart = piecesHtml[1];
                }
                else
                {
                    keyPart = piecesHtml[0];
                }

                string resultHtml = string.Format("{0}{1}{2}", "<div", keyPart, "</div>");

                htmlContent = resultHtml;

                var newPage = new HtmlPagingItem()
                {
                    PageKey = key,
                    HtmlContent = htmlContent,
                    BookContent = content,
                };
                pagings.Add(newPage);
                this.pagingItems.Add(newPage);
                this.pagingItems.Save();
            }

            return pagings;
        }

        private void ParseNavigation(EpubBook epubBook, BookContent content, List<HtmlPagingItem> pagings)
        {
            foreach (var navItem in epubBook.Navigation)
            {
                var chapter = navItem.Title;
                var navKey = navItem.Link.ContentFileName.ToLower().Trim();
                var htmlItem = pagings.Find(x => x.PageKey == navKey);
                var newNavItem = new NavigationItem()
                {
                    HtmlPagingItemId = htmlItem.Id,
                    Chapter = chapter,
                    HtmlPagingItem = htmlItem,
                    BookContent = content,
                };
                this.navigationItems.Add(newNavItem);
                this.navigationItems.Save();
            }
        }
    }
}
