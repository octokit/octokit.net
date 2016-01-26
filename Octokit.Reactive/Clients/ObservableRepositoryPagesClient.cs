using Octokit.Reactive.Internal;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    public class ObservableRepositoryPagesClient : IObservableRepositoryPagesClient
    {
        readonly IRepositoryPagesClient _client;
        readonly IConnection _connection;

        public ObservableRepositoryPagesClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.Repository.Page;
            _connection = client.Connection;
        }

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
        public IObservable<Page> Get(string owner, string repositoryName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repositoryName, "repositoryName");

            return _client.Get(owner, repositoryName).ToObservable();
        }

        /// <summary>
        /// Gets all build metadata for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repositoryName">The name of the repository</param>
        ///  <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/pages/#list-pages-builds">API documentation</a> for more information.
        /// </remarks>
        /// <returns></returns>
        public IObservable<PagesBuild> GetAll(string owner, string repositoryName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repositoryName, "repositoryName");

            return _connection.GetAndFlattenAllPages<PagesBuild>(ApiUrls.RepositoryPageBuilds(owner, repositoryName));
        }

        /// <summary>
        /// Gets the build metadata for the last build for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repositoryName">The name of the repository</param>
        ///  <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/pages/#list-latest-pages-build">API documentation</a> for more information.
        /// </remarks>
        /// <returns></returns>
        public IObservable<PagesBuild> GetLatest(string owner, string repositoryName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repositoryName, "repositoryName");

            return _client.GetLatest(owner, repositoryName).ToObservable();
        }
    }
}
