namespace MvcTemplate.Services.Data
{
    using System.Linq;
    using MvcTemplate.Data.Common;
    using MvcTemplate.Data.Models;

    public class AuthorService : IAuthorService
    {
        private IDbRepository<BookAuthor> authors;
        private IDbRepository<BookAuthorBooks> bookAuthorBooks;

        public AuthorService(
            IDbRepository<BookAuthor> authors,
            IDbRepository<BookAuthorBooks> bookAuthorBooks)
        {
            this.authors = authors;
            this.bookAuthorBooks = bookAuthorBooks;
        }

        public BookAuthor EnsureAuthor(string name, Book book)
        {
            var author = this.authors.All().FirstOrDefault(x => x.Name.ToLower().Equals(name.ToLower()));
            if (author != null)
            {
                return author;
            }

            author = new BookAuthor() { Name = name };
            var bookAuthorRelation = new BookAuthorBooks() { Book = book, Author = author };
            this.bookAuthorBooks.Add(bookAuthorRelation);
            this.bookAuthorBooks.Save();

            author.BookAuthorBooks.Add(bookAuthorRelation);
            book.BookAuthorBooks.Add(bookAuthorRelation);
            this.authors.Add(author);
            this.authors.Save();
            return author;
        }
    }
}
