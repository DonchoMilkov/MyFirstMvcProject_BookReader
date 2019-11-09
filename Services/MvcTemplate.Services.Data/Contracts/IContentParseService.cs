namespace MvcTemplate.Services.Data
{
    using MvcTemplate.Data.Models;
    using VersOne.Epub;

    public interface IContentParseService
    {
        void ParseContent(EpubBook epubBook, Book book);
    }
}