namespace MvcTemplate.Data.Models
{
    using System;
    using MvcTemplate.Data.Common.Models;

    public class Book : BaseModel<int>
    {
        public Book()
        {
            this.CreatedOn = DateTime.UtcNow;
        }

        public string Title { get; set; }

        public string Content { get; set; }

        public int CategoryId { get; set; }

        public virtual BookCategory Category { get; set; }
    }
}
