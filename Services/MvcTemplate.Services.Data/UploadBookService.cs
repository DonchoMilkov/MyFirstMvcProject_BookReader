namespace MvcTemplate.Services.Data
{
    using System;
    using System.IO;
    using System.Web;
    using MvcTemplate.Data.Common;
    using MvcTemplate.Data.Models;
    using VersOne.Epub;

    public class UploadBookService : IUploadBookService
    {
        private IDbRepository<Book> books;
        private IFileParserService epubParser;
        private ICategoryService categories;

        public UploadBookService(
            IDbRepository<Book> books,
            IFileParserService epubParser,
            ICategoryService categories)
        {
            this.books = books;
            this.epubParser = epubParser;
            this.categories = categories;
        }

        public string UploadFile(HttpPostedFileBase file, string category, string language)
        {
            var epubBook = EpubReader.ReadBook(file.InputStream);
            var bookCategory = this.categories.EnsureCategory(category);
            var book = this.epubParser.ParseEpubBook(epubBook, bookCategory);
            book.Language = language;

            string resultMessage;

            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    MemoryStream target = new MemoryStream();
                    file.InputStream.CopyTo(target);
                    byte[] data = target.ToArray();

                    book.BackUpFile = data;

                    resultMessage = "File uploaded successfully";
                }
                catch (Exception ex)
                {
                    resultMessage = "ERROR:" + ex.Message.ToString();
                }
            }
            else
            {
                resultMessage = "You have not specified a file.";
            }

            this.books.Save();

            return resultMessage;
        }
    }
}
