namespace MvcTemplate.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using MvcTemplate.Data.Common.Models;

    public class NavigationItem : IAuditInfo, IDeletableEntity
    {

        [Key]
        [ForeignKey("HtmlPagingItem")]
        public int Id { get; set; }

        public int HtmlPagingItemId { get; set; }

        public int BookContentId { get; set; }

        public string Chapter { get; set; }

        public virtual BookContent BookContent { get; set; }

        public virtual HtmlPagingItem HtmlPagingItem { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
