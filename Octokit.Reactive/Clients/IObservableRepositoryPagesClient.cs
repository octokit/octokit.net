using System;
using System.Diagnostics.CodeAnalysis;

namespace Octokit.Reactive
{
    public interface IObservableRepositoryPagesClient
    {
        /// <summary>
        /// Gets the page metadata for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repositoryName">The name of the repository</param>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/pages/#get-information-about-a-pages-site">API documentation</a> for more information.
        /// </remarks>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
        IObservable<Page> Get(string owner, string repositoryName);
        /// <summary>
        /// Gets all build metadata for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repositoryName">The name of the repository</param>
        ///  <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/pages/#list-pages-builds">API documentation</a> for more information.
        /// </remarks>
        /// <returns></returns>
        IObservable<PagesBuild> GetAll(string owner, string repositoryName);
        /// <summary>
        /// Gets the build metadata for the last build for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repositoryName">The name of the repository</param>
        ///  <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/pages/#list-latest-pages-build">API documentation</a> for more information.
        /// </remarks>
        /// <returns></returns>
        IObservable<PagesBuild> GetLatest(string owner, string repositoryName);
    }
}
