namespace MvcTemplate.Services.Web
{
    using System.Web;

    public interface IUploadBookService
    {
        string UploadFile(HttpPostedFileBase file);
    }
}
