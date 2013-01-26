using System.Collections.Generic;

namespace Octopi
{
    public class PagedList<T>
    {
        public PagedList(IList<T> items, int page, int perPage)
        {
            Ensure.ArgumentNotNull(items, "items");

            Items = items;
            Page = page;
            PerPage = perPage;
        }

        public IList<T> Items { get; private set; }
        public int Page { get; set; }
        public int PerPage { get; set; }
    }
}
