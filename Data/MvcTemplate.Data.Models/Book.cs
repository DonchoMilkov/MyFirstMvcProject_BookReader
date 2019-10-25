namespace MvcTemplate.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using MvcTemplate.Data.Common.Models;

    public class Book : BaseModel<int>
    {
        public Book()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.Readers = new HashSet<ApplicationUser>();
        }

        public string Title { get; set; }

        public string Content { get; set; }

        public int CategoryId { get; set; }

        public int AuthorId { get; set; }

        public virtual BookCategory Category { get; set; }

        public virtual BookAuthor Author { get; set; }

        public virtual ICollection<ApplicationUser> Readers { get; set; }
    }
}
