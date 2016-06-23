using System;
using System.Diagnostics.CodeAnalysis;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Repository Pages API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/repos/pages/">Repository Pages API documentation</a> for more information.
    /// </remarks>
    public interface IObservableRepositoryPagesClient
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
        IObservable<Page> Get(string owner, string name);

        /// <summary>
        /// Gets the page metadata for a given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/pages/#get-information-about-a-pages-site">API documentation</a> for more information.
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
        IObservable<Page> Get(long repositoryId);

        /// <summary>
        /// Gets all build metadata for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        ///  <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/pages/#list-pages-builds">API documentation</a> for more information.
        /// </remarks>
        IObservable<PagesBuild> GetAll(string owner, string name);

        /// <summary>
        /// Gets all build metadata for a given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        ///  <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/pages/#list-pages-builds">API documentation</a> for more information.
        /// </remarks>
        IObservable<PagesBuild> GetAll(long repositoryId);

        /// <summary>
        /// Gets all build metadata for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options to change the API response</param>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/pages/#list-pages-builds">API documentation</a> for more information.
        /// </remarks>
        IObservable<PagesBuild> GetAll(string owner, string name, ApiOptions options);

        /// <summary>
        /// Gets all build metadata for a given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options to change the API response</param>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/pages/#list-pages-builds">API documentation</a> for more information.
        /// </remarks>
        IObservable<PagesBuild> GetAll(long repositoryId, ApiOptions options);

        /// <summary>
        /// Gets the build metadata for the last build for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        ///  <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/pages/#list-latest-pages-build">API documentation</a> for more information.
        /// </remarks>
        IObservable<PagesBuild> GetLatest(string owner, string name);

        /// <summary>
        /// Gets the build metadata for the last build for a given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        ///  <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/pages/#list-latest-pages-build">API documentation</a> for more information.
        /// </remarks>
        IObservable<PagesBuild> GetLatest(long repositoryId);

        /// <summary>
        /// Requests your site be built from the latest revision on the default branch for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        ///  <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/pages/#request-a-page-build">API documentation</a> for more information.
        /// </remarks>
        IObservable<PagesBuild> RequestPageBuild(string owner, string name);

        /// <summary>
        /// Requests your site be built from the latest revision on the default branch for a given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        ///  <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/pages/#request-a-page-build">API documentation</a> for more information.
        /// </remarks>
        IObservable<PagesBuild> RequestPageBuild(long repositoryId);
    }
}
