namespace MvcTemplate.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using MvcTemplate.Data.Common.Models;

    public class Book : BaseModel<int>
    {
        public Book()
        {
            this.ApplicationUserBooks = new HashSet<ApplicationUserBooks>();
            this.BookAuthorBooks = new HashSet<BookAuthorBooks>();
        }

        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        public int BookContentId { get; set; }

        public virtual BookContent BookContent { get; set; }

        [DefaultValue("English")]
        [StringLength(100)]
        public string Language { get; set; }

        [DefaultValue("0")]
        public double? Raiting { get; set; }

        public byte[] Cover { get; set; }

        public int CategoryId { get; set; }

        public virtual BookCategory Category { get; set; }

        public virtual ICollection<BookAuthorBooks> BookAuthorBooks { get; set; }

        public virtual ICollection<ApplicationUserBooks> ApplicationUserBooks { get; set; }
    }
}
