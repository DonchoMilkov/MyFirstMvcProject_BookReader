namespace MvcTemplate.Web.ViewModels.Home
{
    using AutoMapper;
    using MvcTemplate.Common.Mapping;
    using MvcTemplate.Data.Models;

    public class BookViewModel : IMapFrom<Book>, IHaveCustomMappings
    {
        public string Title { get; set; }

        public string Category { get; set; }

        public string Author { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Book, BookViewModel>()
                .ForMember(x => x.Category, opt => opt.MapFrom(x => x.Category.Name))
                .ForMember(x => x.Author, opt => opt.MapFrom(x => x.Author.Name));
        }
    }
}
