namespace MvcTemplate.Web.ViewModels.Home
{
    using AutoMapper;
    using MvcTemplate.Common.Mapping;
    using MvcTemplate.Data.Models;

    public class BookCategoryViewModel : IMapFrom<BookCategory>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
