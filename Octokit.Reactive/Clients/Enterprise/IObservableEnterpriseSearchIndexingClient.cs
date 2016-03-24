using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Enterprise Search Indexing API
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/enterprise/search_indexing/">Enterprise Search Indexing API documentation</a> for more information.
    ///</remarks>
    public interface IObservableEnterpriseSearchIndexingClient
    {
        /// <summary>
        /// Queue an indexing job for a user or organization account (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/search_indexing/#queue-an-indexing-job
        /// </remarks>
        /// <param name="owner">A user or organization account</param>
        /// <returns>The <see cref="SearchIndexingResponse"/> message.</returns>
        IObservable<SearchIndexingResponse> Queue(string owner);

        /// <summary>
        /// Queue an indexing job for a repository (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/search_indexing/#queue-an-indexing-job
        /// </remarks>
        /// <param name="owner">A user or organization account</param>
        /// <param name="repository">A repository</param>
        /// <returns>The <see cref="SearchIndexingResponse"/> message.</returns>
        IObservable<SearchIndexingResponse> Queue(string owner, string repository);

        /// <summary>
        /// Queue an indexing job for all of a user or organization's repositories (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/search_indexing/#queue-an-indexing-job
        /// </remarks>
        /// <param name="owner">A user or organization account</param>
        /// <returns>The <see cref="SearchIndexingResponse"/> message.</returns>
        IObservable<SearchIndexingResponse> QueueAll(string owner);

        /// <summary>
        /// Queue an indexing job for all the issues in a repository (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/search_indexing/#queue-an-indexing-job
        /// </remarks>
        /// <param name="owner">A user or organization account</param>
        /// <param name="repository">A repository</param>
        /// <returns>The <see cref="SearchIndexingResponse"/> message.</returns>
        IObservable<SearchIndexingResponse> QueueAllIssues(string owner, string repository);

        /// <summary>
        /// Queue an indexing job for all the issues in all of a user or organization's repositories (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/search_indexing/#queue-an-indexing-job
        /// </remarks>
        /// <param name="owner">A user or organization account</param>
        /// <returns>The <see cref="SearchIndexingResponse"/> message.</returns>
        IObservable<SearchIndexingResponse> QueueAllIssues(string owner);

        /// <summary>
        /// Queue an indexing job for all the source code in a repository (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/search_indexing/#queue-an-indexing-job
        /// </remarks>
        /// <param name="owner">A user or organization account</param>
        /// <param name="repository">A repository</param>
        /// <returns>The <see cref="SearchIndexingResponse"/> message.</returns>
        IObservable<SearchIndexingResponse> QueueAllCode(string owner, string repository);

        /// <summary>
        /// Queue an indexing job for all the source code in all of a user or organization's repositories (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/search_indexing/#queue-an-indexing-job
        /// </remarks>
        /// <param name="owner">A user or organization account</param>
        /// <returns>The <see cref="SearchIndexingResponse"/> message.</returns>
        IObservable<SearchIndexingResponse> QueueAllCode(string owner);
    }
}
