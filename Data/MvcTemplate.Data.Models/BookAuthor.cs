namespace MvcTemplate.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using MvcTemplate.Data.Common.Models;

    public class BookAuthor : IAuditInfo, IDeletableEntity
    {
        public BookAuthor()
        {
            this.BookAuthorBooks = new HashSet<BookAuthorBooks>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        public virtual ICollection<BookAuthorBooks> BookAuthorBooks { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
