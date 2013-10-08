using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public interface IApiPagination<T>
    {
        Task<IReadOnlyList<T>> GetAllPages(Func<Task<IReadOnlyPagedCollection<T>>> getFirstPage);
    }
}