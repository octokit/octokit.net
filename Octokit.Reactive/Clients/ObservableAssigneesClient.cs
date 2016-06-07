using System;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservableAssigneesClient : IObservableAssigneesClient
    {
        readonly IAssigneesClient _client;
        readonly IConnection _connection;

        public ObservableAssigneesClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.Issue.Assignee;
            _connection = client.Connection;
        }

        /// <summary>
        /// Gets all the available assignees (owner + collaborators) to which issues may be assigned.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        public IObservable<User> GetAllForRepository(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return GetAllForRepository(owner, name, ApiOptions.None);
        }

        /// <summary>
        /// Gets all the available assignees (owner + collaborators) to which issues may be assigned.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">The options to change API's behaviour</param>
        /// <returns></returns>
        public IObservable<User> GetAllForRepository(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(options, "options");

            return _connection.GetAndFlattenAllPages<User>(ApiUrls.Assignees(owner, name), options);
        }

        /// <summary>
        /// Checks to see if a user is an assignee for a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="assignee">Username of the prospective assignee</param>
        /// <returns></returns>
        public IObservable<bool> CheckAssignee(string owner, string name, string assignee)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(assignee, "assignee");

            return _client.CheckAssignee(owner, name, assignee).ToObservable();
        }

        /// <summary>
        /// Add assignees to a specified Issue.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue number</param>
        /// <param name="assignees">List of names of assignees to add</param>
        public IObservable<Issue> AddAssignees(string owner, string name, int number, AssigneesUpdate assignees)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(assignees, "assignees");

            return _client.AddAssignees(owner, name, number, assignees).ToObservable();
        }

        /// <summary>
        /// Remove assignees from a specified Issue.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue number</param>
        /// <param name="assignees">List of assignees to remove </param>
        /// <returns></returns>
        public IObservable<Issue> RemoveAssignees(string owner, string name, int number, AssigneesUpdate assignees)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(assignees, "assignees");

            return _client.RemoveAssignees(owner, name, number, assignees).ToObservable();
        }
    }
}
