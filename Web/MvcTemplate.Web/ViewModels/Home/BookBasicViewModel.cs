namespace MvcTemplate.Web.ViewModels.Home
{
    using System.Collections.Generic;
    using AutoMapper;
    using MvcTemplate.Common.Mapping;
    using MvcTemplate.Data.Models;
    using MvcTemplate.Services.Web;

    public class BookBasicViewModel : IMapFrom<Book>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public List<AuthorViewModel> Authors { get; set; }

        public string UrlReview
        {
            get
            {
                return $"/Books/Review/{this.EncodedId}";
            }
        }

        protected string EncodedId
        {
            get
            {
                IIdentifierProvider identifier = new IdentifierProvider();
                return identifier.EncodeId(this.Id);
            }
        }

        public virtual void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<BookAuthorBooks, AuthorViewModel>()
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Author.Name));

            configuration.CreateMap<Book, BookReadViewModel>()
                .ForMember(x => x.Authors, opt => opt.MapFrom(x => x.BookAuthorBooks));
        }
    }
}
