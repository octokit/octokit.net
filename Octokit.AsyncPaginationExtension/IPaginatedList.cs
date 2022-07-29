using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit.AsyncPaginationExtension
{
    /// <summary>
    /// <cref cref="IAsyncEnumerable{T}"/> additionally allowing for random access.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Intended to abstract from a series of a API calls requiring pagination.
    /// </para>
    /// <para>
    /// Intended to be implemented supporting caching, making repeated enumerations not require any API calls.
    /// </para>
    /// </remarks>
    /// <typeparam name="T">The type of values to access.</typeparam>
    public interface IPaginatedList<T> : IAsyncEnumerable<T>
    {
        /// <summary>
        /// Gets a value at the specified index.
        /// </summary>
        /// <param name="index">The index at which to fetch the value.</param>
        /// <returns>The value at the specified index or null if it is outside of the range.</returns>
        public Task<T?> this[int index] { get; }
    }
}
