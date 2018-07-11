using System;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Check Runs API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/checks/runs/">Check Runs API documentation</a> for more information.
    /// </remarks>
    public class ObservableCheckRunsClient : IObservableCheckRunsClient
    {
        readonly ICheckRunsClient _client;
        readonly IConnection _connection;

        /// <summary>
        /// Initializes a new GitHub Check Runs API client.
        /// </summary>
        /// <param name="client">An <see cref="IGitHubClient" /> used to make the requests</param>
        public ObservableCheckRunsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Check.Run;
            _connection = client.Connection;
        }

        /// <summary>
        /// Creates a new Check Run
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/#create-a-check-run">Check Runs API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="newCheckRun">Details of the Check Run to create</param>
        public IObservable<CheckRun> Create(string owner, string name, NewCheckRun newCheckRun)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(newCheckRun, nameof(newCheckRun));

            return _client.Create(owner, name, newCheckRun).ToObservable();
        }

        /// <summary>
        /// Creates a new Check Run
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/#create-a-check-run">Check Runs API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="newCheckRun">Details of the Check Run to create</param>
        public IObservable<CheckRun> Create(long repositoryId, NewCheckRun newCheckRun)
        {
            Ensure.ArgumentNotNull(newCheckRun, nameof(newCheckRun));

            return _client.Create(repositoryId, newCheckRun).ToObservable();
        }
    }
}
