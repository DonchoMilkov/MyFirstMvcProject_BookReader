namespace MvcTemplate.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using MvcTemplate.Data.Common.Models;

    public class BookAuthorBooks : IDeletableEntity
    {
        [Key]
        [Column(Order = 0)]
        public int BookId { get; set; }

        [Key]
        [Column(Order = 1)]
        public int AuthorId { get; set; }

        public virtual Book Book { get; set; }

        public virtual BookAuthor Author { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
