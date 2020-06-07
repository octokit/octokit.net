using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Repository Pages API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/repos/pages/">Repository Pages API documentation</a> for more information.
    /// </remarks>
    public class RepositoryPagesClient : ApiClient, IRepositoryPagesClient
    {
        /// <summary>
        /// Initializes a new GitHub Repository Pages API client.
        /// </summary>
        /// <param name="apiConnection">An API connection.</param>
        public RepositoryPagesClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        /// <summary>
        /// Gets the page metadata for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/pages/#get-information-about-a-pages-site">API documentation</a> for more information.
        /// </remarks>
        [ManualRoute("GET", "/repos/{owner}/{repo}/pages")]
        public Task<Page> Get(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Get<Page>(ApiUrls.RepositoryPage(owner, name));
        }

        /// <summary>
        /// Gets the page metadata for a given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/pages/#get-information-about-a-pages-site">API documentation</a> for more information.
        /// </remarks>
        [ManualRoute("GET", "/repositories/{id}/pages")]
        public Task<Page> Get(long repositoryId)
        {
            return ApiConnection.Get<Page>(ApiUrls.RepositoryPage(repositoryId));
        }

        /// <summary>
        /// Gets all build metadata for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/pages/#list-pages-builds">API documentation</a> for more information.
        /// </remarks>
        [ManualRoute("GET", "/repos/{owner}/{repo}/pages/builds")]
        public Task<IReadOnlyList<PagesBuild>> GetAll(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAll(owner, name, ApiOptions.None);
        }

        /// <summary>
        /// Gets all build metadata for a given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/pages/#list-pages-builds">API documentation</a> for more information.
        /// </remarks>
        [ManualRoute("GET", "/repositories/{id}/pages/builds")]
        public Task<IReadOnlyList<PagesBuild>> GetAll(long repositoryId)
        {
            return GetAll(repositoryId, ApiOptions.None);
        }

        /// <summary>
        /// Gets all build metadata for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options to change the API response</param>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/pages/#list-pages-builds">API documentation</a> for more information.
        /// </remarks>
        [ManualRoute("GET", "/repos/{owner}/{repo}/pages/builds")]
        public Task<IReadOnlyList<PagesBuild>> GetAll(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            var endpoint = ApiUrls.RepositoryPageBuilds(owner, name);
            return ApiConnection.GetAll<PagesBuild>(endpoint, options);
        }

        /// <summary>
        /// Gets all build metadata for a given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options to change the API response</param>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/pages/#list-pages-builds">API documentation</a> for more information.
        /// </remarks>
        [ManualRoute("GET", "/repositories/{id}/pages/builds")]
        public Task<IReadOnlyList<PagesBuild>> GetAll(long repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            var endpoint = ApiUrls.RepositoryPageBuilds(repositoryId);
            return ApiConnection.GetAll<PagesBuild>(endpoint, options);
        }

        /// <summary>
        /// Gets the build metadata for the last build for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        ///  <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/pages/#list-latest-pages-build">API documentation</a> for more information.
        /// </remarks>
        [ManualRoute("GET", "/repos/{owner}/{repo}/pages/builds/latest")]
        public Task<PagesBuild> GetLatest(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Get<PagesBuild>(ApiUrls.RepositoryPageBuildsLatest(owner, name));
        }

        /// <summary>
        /// Gets the build metadata for the last build for a given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        ///  <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/pages/#list-latest-pages-build">API documentation</a> for more information.
        /// </remarks>
        [ManualRoute("GET", "/repositories/{id}/pages/builds/latest")]
        public Task<PagesBuild> GetLatest(long repositoryId)
        {
            return ApiConnection.Get<PagesBuild>(ApiUrls.RepositoryPageBuildsLatest(repositoryId));
        }

        /// <summary>
        /// Requests your site be built from the latest revision on the default branch for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        ///  <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/pages/#request-a-page-build">API documentation</a> for more information.
        /// </remarks>
        [ManualRoute("POST", "/repos/{owner}/{repo}/pages/builds")]
        public Task<PagesBuild> RequestPageBuild(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Post<PagesBuild>(ApiUrls.RepositoryPageBuilds(owner, name));
        }

        /// <summary>
        /// Requests your site be built from the latest revision on the default branch for a given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        ///  <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/pages/#request-a-page-build">API documentation</a> for more information.
        /// </remarks>
        [ManualRoute("POST", "/repositories/{id}/pages/builds")]
        public Task<PagesBuild> RequestPageBuild(long repositoryId)
        {
            return ApiConnection.Post<PagesBuild>(ApiUrls.RepositoryPageBuilds(repositoryId));
        }
    }
}
