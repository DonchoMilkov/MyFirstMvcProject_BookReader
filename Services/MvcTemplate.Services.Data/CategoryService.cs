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

        public BookCategory EnsureCategory(string categoryName)
        {
            var category = this.categories.All().FirstOrDefault(x => x.Name.ToLower().Equals(categoryName.ToLower()));
            if (category != null)
            {
                return category;
            }

            category = new BookCategory() { Name = categoryName };

            this.categories.Add(category);
            this.categories.Save();

            return category;
        }

        public IQueryable<BookCategory> GetAll()
        {
            return this.categories.All().OrderBy(x => x.Name);
        }
    }
}
