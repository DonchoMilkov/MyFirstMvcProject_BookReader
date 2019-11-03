using System.ComponentModel.DataAnnotations.Schema;

namespace MvcTemplate.Data.Models
{
    public class HtmlPagingItem
    {
        public int Id { get; set; }

        public string PageKey { get; set; }

        public string HtmlContent { get; set; }

        public int NavigationItemId { get; set; }

        public int BookContentId { get; set; }

        public virtual BookContent BookContent { get; set; }

        public virtual NavigationItem NavigationItem { get; set; }
    }
}
