using System;
using System.Collections.Generic;
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

            _client = client.Repository.RepoCollaborators;
            _connection = client.Connection;
        }

        /// <summary>
        /// Gets all the available collaborators on this repo.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        public IObservable<User> GetAll(string owner, string name)
        {
            Ensure.ArgumentNotNull(owner, "owner");
            Ensure.ArgumentNotNull(name, "name");
            var endpoint = ApiUrls.RepoCollaborators(owner, name);
            return _connection.GetAndFlattenAllPages<User>(endpoint);
        }

        /// <summary>
        /// Checks to see if a user is an assignee for a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="user">Username of the prospective collaborator</param>
        /// <returns></returns>
        public IObservable<bool> IsCollaborator(string owner, string name, string user)
        {
            return _client.IsCollaborator(owner, name, user).ToObservable();
        }

        /// <summary>
        /// Adds a user as a collaborator to a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="user">Username of the prospective collaborator</param>
        /// <returns></returns>
        public IObservable<Unit> Add(string owner, string name, string user)
        {
            return _client.Add(owner, name, user).ToObservable();
        }

        /// <summary>
        /// Removes a user as a collaborator for a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="user">Username of the prospective collaborator</param>
        /// <returns></returns>
        public IObservable<Unit> Delete(string owner, string name, string user)
        {
            return _client.Delete(owner, name, user).ToObservable();
        }
    }
}
