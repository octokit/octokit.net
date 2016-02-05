using System;
using System.Reactive.Threading.Tasks;
using Octokit;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Enterprise Search Indexing API
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/enterprise/search_indexing/">Enterprise Search Indexing API documentation</a> for more information.
    ///</remarks>
    public class ObservableEnterpriseSearchIndexingClient : IObservableEnterpriseSearchIndexingClient
    {
        readonly IEnterpriseSearchIndexingClient _client;

        public ObservableEnterpriseSearchIndexingClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.Enterprise.SearchIndexing;
        }

        /// <summary>
        /// Queue an indexing job for a user or organization account (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/search_indexing/#queue-an-indexing-job
        /// </remarks>
        /// <param name="owner">A user or organization account</param>
        /// <returns>The <see cref="SearchIndexingResponse"/> message.</returns>
        public IObservable<SearchIndexingResponse> Queue(string owner)
        {
            return _client.Queue(owner).ToObservable();
        }

        /// <summary>
        /// Queue an indexing job for a repository (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/search_indexing/#queue-an-indexing-job
        /// </remarks>
        /// <param name="owner">A user or organization account</param>
        /// <param name="repository">A repository</param>
        /// <returns>The <see cref="SearchIndexingResponse"/> message.</returns>
        public IObservable<SearchIndexingResponse> Queue(string owner, string repository)
        {
            return _client.Queue(owner, repository).ToObservable();
        }

        /// <summary>
        /// Queue an indexing job for all of a user or organization's repositories (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/search_indexing/#queue-an-indexing-job
        /// </remarks>
        /// <param name="owner">A user or organization account</param>
        /// <returns>The <see cref="SearchIndexingResponse"/> message.</returns>
        public IObservable<SearchIndexingResponse> QueueAll(string owner)
        {
            return _client.QueueAll(owner).ToObservable();
        }

        /// <summary>
        /// Queue an indexing job for all the issues in a repository (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/search_indexing/#queue-an-indexing-job
        /// </remarks>
        /// <param name="owner">A user or organization account</param>
        /// <param name="repository">A repository</param>
        /// <returns>The <see cref="SearchIndexingResponse"/> message.</returns>
        public IObservable<SearchIndexingResponse> QueueAllIssues(string owner, string repository)
        {
            return _client.QueueAllIssues(owner, repository).ToObservable();
        }

        /// <summary>
        /// Queue an indexing job for all the issues in all of a user or organization's repositories (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/search_indexing/#queue-an-indexing-job
        /// </remarks>
        /// <param name="owner">A user or organization account</param>
        /// <returns>The <see cref="SearchIndexingResponse"/> message.</returns>
        public IObservable<SearchIndexingResponse> QueueAllIssues(string owner)
        {
            return _client.QueueAllIssues(owner).ToObservable();
        }

        /// <summary>
        /// Queue an indexing job for all the source code in a repository (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/search_indexing/#queue-an-indexing-job
        /// </remarks>
        /// <param name="owner">A user or organization account</param>
        /// <param name="repository">A repository</param>
        /// <returns>The <see cref="SearchIndexingResponse"/> message.</returns>
        public IObservable<SearchIndexingResponse> QueueAllCode(string owner, string repository)
        {
            return _client.QueueAllCode(owner, repository).ToObservable();
        }

        /// <summary>
        /// Queue an indexing job for all the source code in all of a user or organization's repositories (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/search_indexing/#queue-an-indexing-job
        /// </remarks>
        /// <param name="owner">A user or organization account</param>
        /// <returns>The <see cref="SearchIndexingResponse"/> message.</returns>
        public IObservable<SearchIndexingResponse> QueueAllCode(string owner)
        {
            return _client.QueueAllCode(owner).ToObservable();
        }
    }
}
