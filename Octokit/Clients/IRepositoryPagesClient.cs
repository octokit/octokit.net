using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Repository Pages API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/repos/pages/">Repository Pages API documentation</a> for more information.
    /// </remarks>
    public interface IRepositoryPagesClient
    {
        /// <summary>
        /// Gets the page metadata for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/pages/#get-information-about-a-pages-site">API documentation</a> for more information.
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
        Task<Page> Get(string owner, string name);

        /// <summary>
        /// Gets the page metadata for a given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/pages/#get-information-about-a-pages-site">API documentation</a> for more information.
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
        Task<Page> Get(long repositoryId);

        /// <summary>
        /// Gets all build metadata for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/pages/#list-pages-builds">API documentation</a> for more information.
        /// </remarks>
        Task<IReadOnlyList<PagesBuild>> GetAll(string owner, string name);

        /// <summary>
        /// Gets all build metadata for a given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/pages/#list-pages-builds">API documentation</a> for more information.
        /// </remarks>
        Task<IReadOnlyList<PagesBuild>> GetAll(long repositoryId);

        /// <summary>
        /// Gets all build metadata for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options to change the API response</param>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/pages/#list-pages-builds">API documentation</a> for more information.
        /// </remarks>
        Task<IReadOnlyList<PagesBuild>> GetAll(string owner, string name, ApiOptions options);

        /// <summary>
        /// Gets all build metadata for a given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options to change the API response</param>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/pages/#list-pages-builds">API documentation</a> for more information.
        /// </remarks>
        Task<IReadOnlyList<PagesBuild>> GetAll(long repositoryId, ApiOptions options);

        /// <summary>
        /// Gets the build metadata for the last build for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        ///  <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/pages/#list-latest-pages-build">API documentation</a> for more information.
        /// </remarks>
        Task<PagesBuild> GetLatest(string owner, string name);

        /// <summary>
        /// Gets the build metadata for the last build for a given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        ///  <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/pages/#list-latest-pages-build">API documentation</a> for more information.
        /// </remarks>
        Task<PagesBuild> GetLatest(long repositoryId);

        /// <summary>
        /// Requests your site be built from the latest revision on the default branch for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        ///  <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/pages/#request-a-page-build">API documentation</a> for more information.
        /// </remarks>
        Task<PagesBuild> RequestPageBuild(string owner, string name);

        /// <summary>
        /// Requests your site be built from the latest revision on the default branch for a given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        ///  <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/pages/#request-a-page-build">API documentation</a> for more information.
        /// </remarks>
        Task<PagesBuild> RequestPageBuild(long repositoryId);
    }
}
