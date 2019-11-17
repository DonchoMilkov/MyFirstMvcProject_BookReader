namespace MvcTemplate.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using MvcTemplate.Data.Common.Models;

    public class ApplicationUserBooks : IDeletableEntity, IAuditInfo
    {
        [Key]
        [Column(Order = 0)]
        public int BookId { get; set; }

        [Key]
        [Column(Order = 1)]
        public string ApplicationUserId { get; set; }

        public virtual Book Book { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public int? UpToPage { get; set; }

        public int? Rate { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
