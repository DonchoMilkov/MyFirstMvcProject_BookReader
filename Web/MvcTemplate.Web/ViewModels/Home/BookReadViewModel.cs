namespace MvcTemplate.Web.ViewModels.Home
{

    public class BookReadViewModel : BookBasicViewModel
    {
        public string Content { get; set; }

        public string Url
        {
            get
            {
                return $"/Book/{this.EncodedId}";
            }
        }
    }
}
