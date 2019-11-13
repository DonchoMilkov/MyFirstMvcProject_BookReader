namespace MvcTemplate.Web.Controllers
{
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using MvcTemplate.Common;
    using MvcTemplate.Common.Mapping;
    using MvcTemplate.Services.Data;
    using MvcTemplate.Web.ViewModels.Home;

    public class UploadController : BaseController
    {
        private IUploadBookService uploads;
        private ICategoryService categories;

        public UploadController(
            IUploadBookService uploads,
            ICategoryService categories)
        {
            this.uploads = uploads;
            this.categories = categories;
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
            var allCategories = this.categories.GetAll().To<BookCategoryViewModel>().ToList();
            var viewModel = new UploadBookViewModel()
            {
                Categories = allCategories,
            };

            return this.View(viewModel);
        }
    }
}
