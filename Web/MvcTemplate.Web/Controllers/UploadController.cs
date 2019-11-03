namespace MvcTemplate.Web.Controllers
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using MvcTemplate.Common;
    using MvcTemplate.Services.Data;
    using VersOne.Epub;

    public class UploadController : BaseController
    {
        private IUploadBookService uploads;

        public UploadController(IUploadBookService uploads)
        {
            this.uploads = uploads;
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public ActionResult UploadBook(HttpPostedFileBase file)
        {
            var uploadStatus = this.uploads.UploadFile(file);

            this.ViewBag.Message = uploadStatus;
            return this.View();
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public ActionResult UploadBook()
        {
            return this.View();
        }

        public ActionResult Test()
        {
            string view = string.Empty;
            string path = @"C:\Users\donch\Desktop\The-Metronome.epub";
            EpubBook epubBook = EpubReader.ReadBook(path);
            var pageCss = epubBook.Content.Css.Values;
            foreach (var pagCss in pageCss)
            {
                view += pagCss.Content;
            }

            view = "<style>" + view + "</style>" + Environment.NewLine;
            var htmlPages = epubBook.Content.Html;
            var navigation = epubBook.Navigation;
            //foreach (var navItem in navigation)
            //{
            //    this.ViewBag.Message += navItem.Link.ContentFileName + "->" + navItem.Title +"<br/>";
            //}
            this.ViewBag.Message = view + htmlPages["index_split_009.html"].Content;
            //foreach (var page in htmlPages)
            //{
            //    this.ViewBag.Message += string.Format("{0} {1}", view, page.Key);
            //}

            return this.View();
        }

        //public ActionResult ParseBook()
        //{
        //    ApplicationDbContext db = new ApplicationDbContext();
        //    var parser = new EpubFileParserService(this.fileStream);

        //    var bookTitle = parser.GetTitle();

        //    return this.View();
        //}
    }
}