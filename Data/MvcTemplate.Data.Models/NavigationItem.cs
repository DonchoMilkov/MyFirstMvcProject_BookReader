namespace MvcTemplate.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class NavigationItem
    {
        [ForeignKey("HtmlPagingItem")]
        public int Id { get; set; }

        public string Chapter { get; set; }

        public virtual HtmlPagingItem HtmlPagingItem { get; set; }
    }
}
