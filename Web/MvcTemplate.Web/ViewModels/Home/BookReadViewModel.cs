namespace MvcTemplate.Web.ViewModels.Home
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using MvcTemplate.Common.Mapping;
    using MvcTemplate.Data.Models;
    using MvcTemplate.Services.Web;

    public class BookReadViewModel : IMapFrom<Book>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public List<AuthorViewModel> Authors { get; set; }

        public BookContent BookContent { get; set; }

        public string UrlReading
        {
            get
            {
                return $"/Book/{this.EncodedId}";
            }
        }

        public string UrlReview
        {
            get
            {
                return $"/Books/Review/{this.EncodedId}";
            }
        }

        public string EncodedId
        {
            get
            {
                IIdentifierProvider identifier = new IdentifierProvider();
                return identifier.EncodeId(this.Id);
            }
        }

        public virtual void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Book, BookViewModel>()
                   .ForMember(x => x.Authors, opt => opt.MapFrom(x => x.BookAuthorBooks.Select(b => b.Author)));
        }
    }
}
