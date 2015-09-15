using System;
using System.Threading.Tasks;
#if NET_45
using System.Collections.Generic;
#endif

namespace Octokit
{
    /// <summary>
    /// Used to paginate through API response results.
    /// </summary>
    /// <remarks>
    /// This is meant to be internal, but I factored it out so we can change our mind more easily later.
    /// </remarks>
    public interface IApiPagination
    {
        /// <summary>
        /// Paginate a request to asynchronous fetch the results until no more are returned
        /// </summary>
        /// <typeparam name="T">Type of the API resource to get.</typeparam>
        /// <param name="getFirstPage">A function which generates the first request</param>
        /// <param name="uri">The original URI (used only for raising an exception)</param>
        Task<IReadOnlyList<T>> GetAllPages<T>(Func<Task<IReadOnlyPagedCollection<T>>> getFirstPage, Uri uri);
    }
}