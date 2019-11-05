namespace MvcTemplate.Services.Data
{
    using System.Linq;
    using MvcTemplate.Data.Common;
    using MvcTemplate.Data.Models;

    public class AuthorService : IAuthorService
    {
        private IDbRepository<BookAuthor> authors;

        public AuthorService(
            IDbRepository<BookAuthor> authors)
        {
            this.authors = authors;
        }

        public BookAuthor EnsureAuthor(string name)
        {
            var author = this.authors.All().FirstOrDefault(x => x.Name.ToLower().Equals(name.ToLower()));
            if (author != null)
            {
                return author;
            }

            author = new BookAuthor() { Name = name };

            this.authors.Add(author);
            this.authors.Save();

            return author;
        }
    }
}
