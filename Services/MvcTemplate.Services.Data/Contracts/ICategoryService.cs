namespace MvcTemplate.Services.Data
{
    using System.Linq;
    using MvcTemplate.Data.Models;

    public interface ICategoryService
    {
        IQueryable<BookCategory> GetAll();

        BookCategory EnsureCategory(string categoryName);
    }
}
