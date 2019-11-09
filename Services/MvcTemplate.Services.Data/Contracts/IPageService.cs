namespace MvcTemplate.Services.Data
{
    using MvcTemplate.Data.Models;
    using System.Collections.Generic;

    public interface IPageService
    {
        IEnumerable<HtmlPagingItem> GetAllPages(int bookId);
    }
}