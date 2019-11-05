namespace MvcTemplate.Services.Data
{
    using MvcTemplate.Data.Models;

    public interface IAuthorService
    {
        BookAuthor EnsureAuthor(string name);
    }
}
