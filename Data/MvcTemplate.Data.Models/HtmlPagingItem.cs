namespace MvcTemplate.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using MvcTemplate.Data.Common.Models;

    public class HtmlPagingItem : IAuditInfo, IDeletableEntity
    {
        [Key]
        public int Id { get; set; }

        public string PageKey { get; set; }

        public string HtmlContent { get; set; }

        public int BookContentBookId { get; set; }

        public virtual BookContent BookContent { get; set; }

        public virtual NavigationItem NavigationItem { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
