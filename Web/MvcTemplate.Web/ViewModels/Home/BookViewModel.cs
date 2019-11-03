namespace MvcTemplate.Web.ViewModels.Home
{
    using System;
    using System.Linq;
    using AutoMapper;
    using MvcTemplate.Data.Models;

    public class BookViewModel : BookBasicViewModel
    {

        public string Category { get; set; }

        public string Content { get; set; }

        public double Raiting { get; set; }

        public string Language { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UrlReading
        {
            get
            {
                return $"/Book/{this.EncodedId}";
            }
        }


        public override void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<BookAuthorBooks, AuthorViewModel>()
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Author.Name));

            configuration.CreateMap<Book, BookViewModel>()
                .ForMember(x => x.Category, opt => opt.MapFrom(x => x.Category.Name))
                .ForMember(x => x.Authors, opt => opt.MapFrom(x => x.BookAuthorBooks.Select(b => b.Author)));
        }
    }
}
