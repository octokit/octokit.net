using System.Collections.Generic;
using Nocto.Helpers;

namespace Nocto
{
    public class PagedList<T>
    {
        public PagedList(List<T> items, int page, int perPage)
        {
            Ensure.ArgumentNotNull(items, "items");

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
