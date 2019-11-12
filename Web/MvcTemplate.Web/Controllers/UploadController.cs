namespace MvcTemplate.Web.Controllers
{
    using System.Web;
    using System.Web.Mvc;
    using MvcTemplate.Common;
    using MvcTemplate.Services.Data;

    public class UploadController : BaseController
    {
        private IUploadBookService uploads;

        public UploadController(IUploadBookService uploads)
        {
            this.uploads = uploads;
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public ActionResult UploadBook(HttpPostedFileBase file, string category, string language)
        {
            var uploadStatus = this.uploads.UploadFile(file, category, language);

            this.ViewBag.Message = uploadStatus;
            return this.View();
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public ActionResult UploadBook()
        {
            return this.View();
        }
    }
}
