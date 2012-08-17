using System.Collections.Generic;

namespace Burr
{
    public class PagedList<T>
    {
        public PagedList(List<T> items, int page, int perPage)
        {
            Items = items;
            Page = page;
            PerPage = perPage;
            Total = items.Count + page*perPage;
        }

        public List<T> Items { get; private set; }
        public int Page { get; set; }
        public int PerPage { get; set; }
        public int Total { get; set; }
    }
}
