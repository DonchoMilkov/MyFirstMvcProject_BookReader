namespace MvcTemplate.Web.ViewModels.Home
{
    using System.Web;

    public class UploadBookViewModel
    {
        public string Category { get; set; }

        public string Language { get; set; }

        public HttpPostedFileBase File { get; set; }

    }
}