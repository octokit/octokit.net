using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservableRepoCollaboratorsClient : IObservableRepoCollaboratorsClient
    {
        readonly IRepoCollaboratorsClient _client;
        readonly IConnection _connection;

        public ObservableRepoCollaboratorsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.Repository.Collaborator;
            _connection = client.Connection;
        }

        /// <summary>
        /// Gets all the available collaborators on this repo.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repo">The name of the repository</param>
        /// <returns>The list of <see cref="User"/>s for the specified repository.</returns>
        public IObservable<User> GetAll(string owner, string repo)
        {
            Ensure.ArgumentNotNull(owner, "owner");
            Ensure.ArgumentNotNull(repo, "repo");

            return GetAll(owner, repo, ApiOptions.None);
        }

        /// <summary>
        /// Gets all the available collaborators on this repo.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repo">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>The list of <see cref="User"/>s for the specified repository.</returns>
        public IObservable<User> GetAll(string owner, string repo, ApiOptions options)
        {
            Ensure.ArgumentNotNull(owner, "owner");
            Ensure.ArgumentNotNull(repo, "repo");
            Ensure.ArgumentNotNull(options, "options");
            
            return _connection.GetAndFlattenAllPages<User>(ApiUrls.RepoCollaborators(owner, repo), options);
        }

        /// <summary>
        /// Checks to see if a user is an assignee for a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repo">The name of the repository</param>
        /// <param name="user">Username of the prospective collaborator</param>
        /// <returns></returns>
        public IObservable<bool> IsCollaborator(string owner, string repo, string user)
        {
            return _client.IsCollaborator(owner, repo, user).ToObservable();
        }

        /// <summary>
        /// Adds a user as a collaborator to a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repo">The name of the repository</param>
        /// <param name="user">Username of the prospective collaborator</param>
        /// <returns></returns>
        public IObservable<Unit> Add(string owner, string repo, string user)
        {
            return _client.Add(owner, repo, user).ToObservable();
        }

        /// <summary>
        /// Removes a user as a collaborator for a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repo">The name of the repository</param>
        /// <param name="user">Username of the prospective collaborator</param>
        /// <returns></returns>
        public IObservable<Unit> Delete(string owner, string repo, string user)
        {
            return _client.Delete(owner, repo, user).ToObservable();
        }
    }
}
