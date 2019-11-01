namespace MvcTemplate.Services.Web
{
    using System;
    using System.IO;
    using System.Web;

    public class UploadBookService : IUploadBookService
    {
        public string UploadFile(HttpPostedFileBase file)
        {
            string resultMessage;

            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    string path = @"C:\donchos\BookReader\Files\" + Path.GetFileName(file.FileName);
                    file.SaveAs(path);
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

            return resultMessage;
        }
    }
}
