using System;

namespace Octokit.Reactive
{
    public interface IObservableRepositoryForksClient
    {
        /// <summary>
        /// Gets the list of forks defined for a repository
        /// </summary>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/forks/#list-forks">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        IObservable<Repository> GetAll(string owner, string repositoryName, RepositoryForksListRequest request);

        /// <summary>
        /// Creates a fork for a repository. Specify organization in the fork parameter to create for an organization.
        /// </summary>
        /// <remarks>See <a href="http://developer.github.com/v3/repos/forks/#create-a-fork">API documentation</a> for more information.</remarks>
        /// <returns></returns>
        IObservable<Repository> Create(string owner, string repositoryName, NewRepositoryFork fork);
    }
}
