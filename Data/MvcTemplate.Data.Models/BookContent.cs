namespace MvcTemplate.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class BookContent
    {
        public BookContent()
        {
            this.Paging = new HashSet<HtmlPagingItem>();
            this.Navigation = new HashSet<NavigationItem>();
        }

        [ForeignKey("Book")]
        public int Id { get; set; }

        public string StyleSheet { get; set; }

        public string StylePage { get; set; }

        public virtual Book Book { get; set; }

        public ICollection<HtmlPagingItem> Paging { get; set; }

        public ICollection<NavigationItem> Navigation { get; set; }
    }
}
