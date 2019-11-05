namespace MvcTemplate.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using MvcTemplate.Data.Common.Models;

    public class BookContent : IAuditInfo, IDeletableEntity
    {
        public BookContent()
        {
            this.Paging = new HashSet<HtmlPagingItem>();
            this.Navigation = new HashSet<NavigationItem>();
        }

        [Key]
        [ForeignKey("Book")]
        public int Id { get; set; }

        public string StyleSheet { get; set; }

        public string StylePage { get; set; }

        public virtual Book Book { get; set; }

        public virtual ICollection<HtmlPagingItem> Paging { get; set; }

        public virtual ICollection<NavigationItem> Navigation { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

    }
}
