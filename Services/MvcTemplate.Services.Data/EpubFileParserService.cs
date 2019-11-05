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

            var author1 = this.authors.EnsureAuthor("MyAuthor12");

            var author2 = this.authors.EnsureAuthor("MyAuthor24");

            var category = new BookCategory()
            {
                Name = "MyCategory",
            };

            var content = new BookContent();

            var book = new Book()
            {
                Category = category,
                Title = "MyBook",
                BookContent = content,
            };

            var bookAuthorBooks1 = new BookAuthorBooks()
            {
                Author = author1,
                Book = book,
            };

            var bookAuthorBooks2 = new BookAuthorBooks()
            {
                Author = author2,
                Book = book,
            };

            book.BookAuthorBooks.Add(bookAuthorBooks1);
            book.BookAuthorBooks.Add(bookAuthorBooks2);

            this.categories.Add(category);
            this.categories.Save();
            this.books.Add(book);
            this.books.Save();

            return book;
        }
    }
}
            /*this.ParseTitle(epubBook, book);

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
            //TODO:            Clean code fix bugs

            var epubContent = epubBook.Content;
            var content = new BookContent();

            //content.StyleSheet = string.Join(Environment.NewLine, epubContent.Css.Values);

            var epubPages = epubBook.Content.Html;
            foreach (var epubPage in epubPages)
            {
                var newPage = new HtmlPagingItem() { PageKey = epubPage.Key, HtmlContent = epubPage.Value.Content };
                this.pagingItems.Add(newPage);
                content.Paging.Add(newPage);
            }

            this.contents.Add(content);
            this.contents.Save();
            this.pagingItems.Save();

            //var epubNavigation = epubBook.Navigation;
            //foreach (var epubNavItem in epubNavigation)
            //{
            //    var newNavItem = new NavigationItem();
            //    newNavItem.Chapter = epubNavItem.Title;
            //    newNavItem.HtmlPagingItem = this.pagingItems.All().First(x => x.PageKey == epubNavItem.Link.ContentFileName);
            //    this.navigationItems.Add(newNavItem);
            //    content.Navigation.Add(newNavItem);
            //}

            //this.navigationItems.Save();

            //this.contents.Add(content);

            //book.BookContent = content;

            //this.contents.Save();

            return;
        }
    }
}
*/