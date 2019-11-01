namespace MvcTemplate.Web.ViewModels.Home
{
    using AutoMapper;
    using MvcTemplate.Common.Mapping;
    using MvcTemplate.Data.Models;

    public class AuthorViewModel : IMapFrom<BookAuthor>
    {
        public int Id { get; set; }

        public string Name { get; set; }

    }
}