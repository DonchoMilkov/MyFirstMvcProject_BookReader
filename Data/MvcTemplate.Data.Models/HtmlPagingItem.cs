using System;
using MvcTemplate.Data.Common.Models;

namespace MvcTemplate.Data.Models
{
    public class HtmlPagingItem : IAuditInfo, IDeletableEntity
    {
        public int Id { get; set; }

        public string PageKey { get; set; }

        public string HtmlContent { get; set; }

        public int NavigationItemId { get; set; }

        public int BookContentId { get; set; }

        public virtual BookContent BookContent { get; set; }

        public virtual NavigationItem NavigationItem { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
