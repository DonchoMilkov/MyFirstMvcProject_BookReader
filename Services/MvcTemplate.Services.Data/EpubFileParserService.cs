namespace MvcTemplate.Services.Data
{
    using System.Collections.Generic;
    using MvcTemplate.Data.Models;
    using VersOne.Epub;

    public class EpubFileParserService : IFileParserService
    {
        public Book ParseEpubBook(EpubBook epubBook)
        {
            var book = new Book();
            List<string> authorList = new List<string>();
            book.Title = epubBook.Title;
            book.Cover = epubBook.CoverImage;
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
                var bookAuthor = new BookAuthor() { Name = author };
                book.BookAuthorBooks.Add(new BookAuthorBooks() { Book = book, Author = bookAuthor });
            }

            var pages = new List<string>();

            var filePages = epubBook.Content.Html.Values;

            foreach (var filePage in filePages)
            {
                pages.Add(filePage.Content);
            }

            return book;
        }
    }
}
