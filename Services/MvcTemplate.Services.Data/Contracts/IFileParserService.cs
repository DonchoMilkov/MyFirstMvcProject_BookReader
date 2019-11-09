namespace MvcTemplate.Services.Data
{
    using MvcTemplate.Data.Models;
    using VersOne.Epub;

    public interface IFileParserService
    {
        Book ParseEpubBook(EpubBook epubBook);
    }
}
