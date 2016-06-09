using System;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservableRepositoryForksClient : IObservableRepositoryForksClient
    {
        readonly IRepositoryForksClient _client;
        readonly IConnection _connection;

        /// <summary>
        /// Initializes a new GitHub Repos Fork API client.
        /// </summary>
        /// <param name="client"></param>
        public ObservableRepositoryForksClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.Repository.Forks;
            _connection = client.Connection;
        }

        /// <summary>
        /// Gets the list of forks defined for a repository
        /// </summary>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/forks/#list-forks">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        public IObservable<Repository> GetAll(string owner, string repositoryName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repositoryName, "repositoryName");

            return GetAll(owner, repositoryName, ApiOptions.None);
        }

        /// <summary>
        /// Gets the list of forks defined for a repository
        /// </summary>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/forks/#list-forks">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        public IObservable<Repository> GetAll(string owner, string repositoryName, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repositoryName, "repositoryName");
            Ensure.ArgumentNotNull(options, "options");

            return _connection.GetAndFlattenAllPages<Repository>(ApiUrls.RepositoryForks(owner, repositoryName), options);
        }

        /// <summary>
        /// Gets the list of forks defined for a repository
        /// </summary>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/forks/#list-forks">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        public IObservable<Repository> GetAll(string owner, string repositoryName, RepositoryForksListRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repositoryName, "repositoryName");

            return GetAll(owner, repositoryName, request, ApiOptions.None);
        }

        /// <summary>
        /// Gets the list of forks defined for a repository
        /// </summary>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/forks/#list-forks">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        public IObservable<Repository> GetAll(string owner, string repositoryName, RepositoryForksListRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repositoryName, "repositoryName");
            Ensure.ArgumentNotNull(options, "options");

            return request == null ? _connection.GetAndFlattenAllPages<Repository>(ApiUrls.RepositoryForks(owner, repositoryName), options) :
                _connection.GetAndFlattenAllPages<Repository>(ApiUrls.RepositoryForks(owner, repositoryName), request.ToParametersDictionary(), options);
        }

        /// <summary>
        /// Creates a fork for a repository. Specify organization in the fork parameter to create for an organization.
        /// </summary>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/forks/#create-a-fork">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        public IObservable<Repository> Create(string owner, string repositoryName, NewRepositoryFork fork)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repositoryName, "repositoryName");
            Ensure.ArgumentNotNull(fork, "fork");

            return _client.Create(owner, repositoryName, fork).ToObservable();
        }
    }
}