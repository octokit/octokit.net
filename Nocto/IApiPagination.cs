using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octopi
{
    public interface IApiPagination<T>
    {
        Task<IReadOnlyCollection<T>> GetAllPages(Func<Task<IReadOnlyPagedCollection<T>>> getFirstPage);
    }
}