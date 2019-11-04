namespace MvcTemplate.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MvcTemplate.Data.Common;
    using MvcTemplate.Data.Models;
    using VersOne.Epub;

    public class EpubFileParserService : IFileParserService
    {
        private IAuthorService authors;
        private IDbRepository<BookContent> contents;
        private IDbRepository<NavigationItem> navigationItems;
        private IDbRepository<HtmlPagingItem> pagingItems;

        public EpubFileParserService(
            IAuthorService authors,
            IDbRepository<BookContent> contents,
            IDbRepository<NavigationItem> navigationItems,
            IDbRepository<HtmlPagingItem> pagingItems)
        {
            this.authors = authors;
            this.contents = contents;
            this.navigationItems = navigationItems;
            this.pagingItems = pagingItems;
        }

        public Book ParseEpubBook(EpubBook epubBook)
        {
            var book = new Book();

            this.ParseTitle(epubBook, book);

            this.ParseAuthorList(epubBook, book);

            this.ParseCoverImage(epubBook, book);

            this.ParseContent(epubBook, book);

            return book;
        }

        private void ParseTitle(EpubBook epubBook, Book book)
        {
            book.Title = epubBook.Title;
            return;
        }

        private void ParseAuthorList(EpubBook epubBook, Book book)
        {
            List<string> authorList = new List<string>();
            if (epubBook.AuthorList != null)
            {
                authorList.AddRange(epubBook.AuthorList);
            }
            else
            {
                var separators = new char[] { ',' };
                var authors = epubBook.Author.Split(separators, System.StringSplitOptions.RemoveEmptyEntries);
                authorList.AddRange(authors);
            }

            foreach (var author in authorList)
            {
                var bookAuthor = this.authors.EnsureAuthor(author, book);
            }

            return;
        }

        private void ParseCoverImage(EpubBook epubBook, Book book)
        {
            if (epubBook.CoverImage != null)
            {
                book.Cover = epubBook.CoverImage;
            }

            return;
        }

        private void ParseContent(EpubBook epubBook, Book book)
        {
            var epubContent = epubBook.Content;
            var content = new BookContent();

            content.StyleSheet = string.Join(Environment.NewLine, epubContent.Css.Values);

            var epubPages = epubBook.Content.Html;
            foreach (var epubPage in epubPages)
            {
                var newPage = new HtmlPagingItem() { PageKey = epubPage.Key, HtmlContent = epubPage.Value.Content };
                this.pagingItems.Add(newPage);
                content.Paging.Add(newPage);
            }

            this.pagingItems.Save();

            var epubNavigation = epubBook.Navigation;
            foreach (var epubNavItem in epubNavigation)
            {
                var newNavItem = new NavigationItem();
                newNavItem.Chapter = epubNavItem.Title;
                newNavItem.HtmlPagingItem = this.pagingItems.All().First(x => x.PageKey == epubNavItem.Link.ContentFileName);
                this.navigationItems.Add(newNavItem);
                content.Navigation.Add(newNavItem);
            }

            this.navigationItems.Save();

            this.contents.Add(content);

            book.BookContent = content;

            this.contents.Save();

            return;
        }
    }
}
