namespace MvcTemplate.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MvcTemplate.Data;
    using MvcTemplate.Data.Common;
    using MvcTemplate.Data.Models;
    using VersOne.Epub;

    public class EpubFileParserService : IFileParserService
    {
        private IDbRepository<Book> books;
        private IAuthorService authors;
        private IDbRepository<BookContent> contents;
        private IDbRepository<NavigationItem> navigationItems;
        private IDbRepository<HtmlPagingItem> pagingItems;
        private IDbRepository<BookCategory> categories;

        public EpubFileParserService(
            IDbRepository<Book> books,
            IAuthorService authors,
            IDbRepository<BookCategory> categories,
            IDbRepository<BookContent> contents,
            IDbRepository<NavigationItem> navigationItems,
            IDbRepository<HtmlPagingItem> pagingItems)
        {
            this.books = books;
            this.authors = authors;
            this.categories = categories;
            this.contents = contents;
            this.navigationItems = navigationItems;
            this.pagingItems = pagingItems;
        }

        public Book ParseEpubBook(EpubBook epubBook)
        {
            var authors = this.ParseAuthorList(epubBook);

            var book = new Book();

            foreach (var author in authors)
            {
                var bookAuthorBooks = new BookAuthorBooks()
                {
                    Author = author,
                    Book = book,
                };
                book.BookAuthorBooks.Add(bookAuthorBooks);
            }

            var category = new BookCategory()
            {
                Name = "нова категория",
            };
            book.Category = category;
            this.categories.Add(category);

            this.ParseTitle(epubBook, book);

            this.ParseCoverImage(epubBook, book);

            this.ParseContent(epubBook, book);

            return book;
        }

        private void ParseTitle(EpubBook epubBook, Book book)
        {
            book.Title = epubBook.Title;
            return;
        }

        private ICollection<BookAuthor> ParseAuthorList(EpubBook epubBook)
        {
            List<string> authorList = new List<string>();
            if (epubBook.AuthorList != null)
            {
                authorList.AddRange(epubBook.AuthorList);
            }
            else
            {
                var separators = new char[] { ',' };
                var authors = epubBook.Author.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                authorList.AddRange(authors);
            }

            var bookAuthorList = new List<BookAuthor>();
            foreach (var author in authorList)
            {
                bookAuthorList.Add(this.authors.EnsureAuthor(author));
            }

            return bookAuthorList;
        }

        private void ParseContent(EpubBook epubBook, Book book)
        {
            var content = new BookContent();
            content.Book = book;
            foreach (var css in epubBook.Content.Css.Values)
            {
                content.StyleSheet += css.Content + Environment.NewLine;
            }
            content.StyleSheet = "<style>" + content.StyleSheet + "</style>";

            book.BookContent = content;
            this.books.Add(book);

            content.BookId = book.Id;
            this.contents.Add(content);

            var pagings = new List<HtmlPagingItem>();
            foreach (var item in epubBook.Content.Html)
            {
                var key = item.Key.ToLower().Trim();
                string htmlContent;

                string inputHtml = item.Value.Content;

                string[] bodyTags = { "<body", "/body>" };

                var piecesHtml = inputHtml.Split(bodyTags, StringSplitOptions.RemoveEmptyEntries);

                string keyPart;

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

        private void ParseCoverImage(EpubBook epubBook, Book book)
        {
            if (epubBook.CoverImage != null)
            {
                book.Cover = epubBook.CoverImage;
            }

            return;
        }
    }
}
