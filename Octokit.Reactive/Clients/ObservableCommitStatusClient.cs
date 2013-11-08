using System;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservableCommitStatusClient : IObservableCommitStatusClient
    {
        readonly ICommitStatusClient _client;
        readonly IConnection _connection;

        public ObservableCommitStatusClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.Repository.CommitStatus;
            _connection = client.Connection;
        }

        /// <summary>
        /// Retrieves commit statuses for the specified reference. A reference can be a commit SHA, a branch name, or
        /// a tag name.
        /// </summary>
        /// <remarks>Only users with pull access can see this.</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The reference (SHA, branch name, or tag name) to list commits for</param>
        /// <returns></returns>
        public IObservable<CommitStatus> GetAll(string owner, string name, string reference)
        {
            return _connection.GetAndFlattenAllPages<CommitStatus>(ApiUrls.CommitStatus(owner, name, reference));
        }

        /// <summary>
        /// Creates a commit status for the specified ref.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The reference (SHA, branch name, or tag name) to list commits for</param>
        /// <param name="commitStatus">The commit status to create</param>
        /// <returns></returns>
        public IObservable<CommitStatus> Create(string owner, string name, string reference, NewCommitStatus commitStatus)
        {
            return _client.Create(owner, name, reference, commitStatus).ToObservable();
        }
    }
}
