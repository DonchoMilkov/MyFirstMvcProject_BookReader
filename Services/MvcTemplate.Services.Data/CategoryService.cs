namespace MvcTemplate.Services.Data
{
    using System.Linq;
    using MvcTemplate.Data.Common;
    using MvcTemplate.Data.Models;

    public class CategoryService : ICategoryService
    {
        private IDbRepository<BookCategory> categories;

        public CategoryService(IDbRepository<BookCategory> categories)
        {
            this.categories = categories;
        }

        public IQueryable<BookCategory> GetAll()
        {
            return this.categories.All().OrderBy(x => x.Name);
        }
    }
}
