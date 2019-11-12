namespace MvcTemplate.Services.Data
{
    using System;
    using System.Collections.Generic;
    using MvcTemplate.Data.Models;
    using VersOne.Epub;

    public class EpubFileParserService : IFileParserService
    {
        private IAuthorService authors;
        private IContentParseService contents;

        public EpubFileParserService(
            IAuthorService authors,
            IContentParseService contents)
        {
            this.authors = authors;
            this.contents = contents;
        }

        public Book ParseEpubBook(EpubBook epubBook, BookCategory category)
        {
            var book = new Book();

            this.ParseAuthorList(epubBook, book);
            this.ParseTitle(epubBook, book);
            this.ParseCoverImage(epubBook, book);
            book.Category = category;
            this.contents.ParseContent(epubBook, book);

            return book;
        }

        private ICollection<BookAuthor> ParseAuthorList(EpubBook epubBook, Book book)
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

            foreach (var author in bookAuthorList)
            {
                var bookAuthorBooks = new BookAuthorBooks()
                {
                    Author = author,
                    Book = book,
                };
                book.BookAuthorBooks.Add(bookAuthorBooks);
            }

            return bookAuthorList;
        }

        private void ParseTitle(EpubBook epubBook, Book book)
        {
            book.Title = epubBook.Title;
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
    }
}
