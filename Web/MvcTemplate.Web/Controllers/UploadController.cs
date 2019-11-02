namespace MvcTemplate.Web.Controllers
{
    using System.IO;
    using System.Web;
    using System.Web.Mvc;
    using MvcTemplate.Common;
    using MvcTemplate.Data;
    using MvcTemplate.Services.Data;
    using MvcTemplate.Services.Web;

    public class UploadController : BaseController
    {
        private IUploadBookService uploads;
        private IFileParserService epubParser;
        private Stream fileStream;

        public UploadController(
            IUploadBookService uploads,
            IFileParserService epubParser)
        {
            this.uploads = uploads;
            this.epubParser = epubParser;
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public ActionResult UploadBook(HttpPostedFileBase file)
        {
            this.fileStream = file.InputStream;
            var uploadStatus = this.uploads.UploadFile();

            this.ViewBag.Message = uploadStatus;
            return this.View();
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public ActionResult UploadBook()
        {
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