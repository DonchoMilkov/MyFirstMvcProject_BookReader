namespace MvcTemplate.Services.Data
{
    using System.Collections.Generic;
    using VersOne.Epub;

    public class EpubFileParserService : IFileParserService
    {
        private EpubBook epubBook;

        public EpubFileParserService(EpubBook epubBook)
        {
            this.epubBook = epubBook;
        }

        public string GetTitle()
        {
            return this.epubBook.Title;
        }

        public List<string> GetAuthorNames()
        {
            var authorList = this.epubBook.AuthorList;
            if (authorList != null)
            {
                return authorList;
            }
            else
            {
                return new List<string>() { this.epubBook.Author };
            }
        }

        public List<string> GetContentInListOfPages()
        {
            var pages = new List<string>();

            var filePages = this.epubBook.Content.Html.Values;

            foreach (var filePage in filePages)
            {
                pages.Add(filePage.Content);
            }

            return pages;
        }
    }
}
