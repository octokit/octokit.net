using System;
using System.Collections.Generic;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;

namespace Octokit.Reactive
{
    public class ObservableRepositoryForksClient : IObservableRepositoryForksClient
    {
        readonly IRepositoryForksClient _client;

        /// <summary>
        /// Initializes a new GitHub Repos Fork API client.
        /// </summary>
        /// <param name="client"></param>
        public ObservableRepositoryForksClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");
            _client = client.Repository.Forks;
        }

        /// <summary>
        /// Gets the list of forks defined for a repository
        /// </summary>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/forks/#list-forks">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        public IObservable<IReadOnlyList<Repository>> Get(string owner, string repositoryName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repositoryName, "repositoryName");

            return _client.Get(owner, repositoryName).ToObservable();
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