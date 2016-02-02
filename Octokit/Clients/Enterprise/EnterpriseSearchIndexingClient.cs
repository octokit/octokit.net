using System.Globalization;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Enterprise Search Indexing API
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/enterprise/search_indexing/">Enterprise Search Indexing API documentation</a> for more information.
    ///</remarks>
    public class EnterpriseSearchIndexingClient : ApiClient, IEnterpriseSearchIndexingClient
    {
        public EnterpriseSearchIndexingClient(IApiConnection apiConnection)
            : base(apiConnection)
        { }

        /// <summary>
        /// Queue an indexing job for a user or organization account (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/search_indexing/#queue-an-indexing-job
        /// </remarks>
        /// <param name="owner">A user or organization account</param>
        /// <returns>The <see cref="SearchIndexingResponse"/> message.</returns>
        public async Task<SearchIndexingResponse> Queue(string owner)
        {
            Ensure.ArgumentNotNull(owner, "owner");

            var endpoint = ApiUrls.EnterpriseSearchIndexing();
            var target = new SearchIndexTarget(string.Format(CultureInfo.InvariantCulture, "{0}", owner));

            return await ApiConnection.Post<SearchIndexingResponse>(endpoint, target)
                                                    .ConfigureAwait(false);
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
        public async Task<SearchIndexingResponse> Queue(string owner, string repository)
        {
            Ensure.ArgumentNotNull(owner, "owner");
            Ensure.ArgumentNotNull(repository, "repository");

            var endpoint = ApiUrls.EnterpriseSearchIndexing();
            var target = new SearchIndexTarget(string.Format(CultureInfo.InvariantCulture, "{0}/{1}", owner, repository));

            return await ApiConnection.Post<SearchIndexingResponse>(endpoint, target)
                                                    .ConfigureAwait(false);
        }

        /// <summary>
        /// Queue an indexing job for all of a user or organization's repositories (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/search_indexing/#queue-an-indexing-job
        /// </remarks>
        /// <param name="owner">A user or organization account</param>
        /// <returns>The <see cref="SearchIndexingResponse"/> message.</returns>
        public async Task<SearchIndexingResponse> QueueAll(string owner)
        {
            Ensure.ArgumentNotNull(owner, "owner");

            var endpoint = ApiUrls.EnterpriseSearchIndexing();
            var target = new SearchIndexTarget(string.Format(CultureInfo.InvariantCulture, "{0}/*", owner));

            return await ApiConnection.Post<SearchIndexingResponse>(endpoint, target)
                                                    .ConfigureAwait(false);
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
        public async Task<SearchIndexingResponse> QueueAllIssues(string owner, string repository)
        {
            Ensure.ArgumentNotNull(owner, "owner");
            Ensure.ArgumentNotNull(repository, "repository");

            var endpoint = ApiUrls.EnterpriseSearchIndexing();
            var target = new SearchIndexTarget(string.Format(CultureInfo.InvariantCulture, "{0}/{1}/issues", owner, repository));

            return await ApiConnection.Post<SearchIndexingResponse>(endpoint, target)
                                                    .ConfigureAwait(false);
        }

        /// <summary>
        /// Queue an indexing job for all the issues in all of a user or organization's repositories (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/search_indexing/#queue-an-indexing-job
        /// </remarks>
        /// <param name="owner">A user or organization account</param>
        /// <returns>The <see cref="SearchIndexingResponse"/> message.</returns>
        public async Task<SearchIndexingResponse> QueueAllIssues(string owner)
        {
            Ensure.ArgumentNotNull(owner, "owner");

            var endpoint = ApiUrls.EnterpriseSearchIndexing();
            var target = new SearchIndexTarget(string.Format(CultureInfo.InvariantCulture, "{0}/*/issues", owner));

            return await ApiConnection.Post<SearchIndexingResponse>(endpoint, target)
                                                    .ConfigureAwait(false);
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
        public async Task<SearchIndexingResponse> QueueAllCode(string owner, string repository)
        {
            Ensure.ArgumentNotNull(owner, "owner");
            Ensure.ArgumentNotNull(repository, "repository");

            var endpoint = ApiUrls.EnterpriseSearchIndexing();
            var target = new SearchIndexTarget(string.Format(CultureInfo.InvariantCulture, "{0}/{1}/code", owner, repository));

            return await ApiConnection.Post<SearchIndexingResponse>(endpoint, target)
                                                    .ConfigureAwait(false);
        }

        /// <summary>
        /// Queue an indexing job for all the source code in all of a user or organization's repositories (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/search_indexing/#queue-an-indexing-job
        /// </remarks>
        /// <param name="owner">A user or organization account</param>
        /// <returns>The <see cref="SearchIndexingResponse"/> message.</returns>
        public async Task<SearchIndexingResponse> QueueAllCode(string owner)
        {
            Ensure.ArgumentNotNull(owner, "owner");
            
            var endpoint = ApiUrls.EnterpriseSearchIndexing();
            var target = new SearchIndexTarget(string.Format(CultureInfo.InvariantCulture, "{0}/*/code", owner));

            return await ApiConnection.Post<SearchIndexingResponse>(endpoint, target)
                                                    .ConfigureAwait(false);
        }
    }
}
