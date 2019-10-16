namespace MvcTemplate.Data.Models
{
    using System.Collections.Generic;
    using MvcTemplate.Data.Common.Models;

    public class BookCategory : BaseModel<int>
    {
        public BookCategory()
        {
            this.Books = new HashSet<Book>();
        }

        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
