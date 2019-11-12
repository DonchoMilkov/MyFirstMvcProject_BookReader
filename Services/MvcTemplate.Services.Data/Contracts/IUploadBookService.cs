namespace MvcTemplate.Services.Data
{
    using System.Web;

    public interface IUploadBookService
    {
        string UploadFile(HttpPostedFileBase file, string category, string language);
    }
}
