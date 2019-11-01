namespace MvcTemplate.Web.ViewModels.Home
{
    using AutoMapper;
    using MvcTemplate.Common.Mapping;
    using MvcTemplate.Data.Models;
    using MvcTemplate.Services.Web;
    using System.Collections.Generic;

    public class BookReadViewModel : IMapFrom<Book>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public List<AuthorViewModel> Authors { get; set; }

        public string Url
        {
            get
            {
                IIdentifierProvider identifier = new IdentifierProvider();
                return $"/Book/{identifier.EncodeId(this.Id)}";
            }
        }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<BookAuthorBooks, AuthorViewModel>()
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Author.Name));

            configuration.CreateMap<Book, BookReadViewModel>()
                .ForMember(x => x.Authors, opt => opt.MapFrom(x => x.BookAuthorBooks));
            //.ForMember(x => x.Author, opt => opt.MapFrom(x => x.Authors.Name));

            

        }
    }
}
