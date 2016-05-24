using System;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservableIssuesEventsClient : IObservableIssuesEventsClient
    {
        readonly IIssuesEventsClient _client;
        readonly IConnection _connection;

        public ObservableIssuesEventsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.Issue.Events;
            _connection = client.Connection;
        }

        /// <summary>
        /// Gets all events for the issue.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/events/#list-events-for-an-issue
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue number</param>
        /// <returns></returns>
        public IObservable<EventInfo> GetAllForIssue(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return GetAllForIssue(owner, name, number, ApiOptions.None);
        }

        /// <summary>
        /// Gets all events for the issue.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/events/#list-events-for-an-issue
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue number</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns></returns>
        public IObservable<EventInfo> GetAllForIssue(string owner, string name, int number, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(options, "options");

            return _connection.GetAndFlattenAllPages<EventInfo>(ApiUrls.IssuesEvents(owner, name, number), options);
        }

        /// <summary>
        /// Gets all events for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/events/#list-events-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        public IObservable<IssueEvent> GetAllForRepository(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return GetAllForRepository(owner, name, ApiOptions.None);
        }

        /// <summary>
        /// Gets all events for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/events/#list-events-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns></returns>
        public IObservable<IssueEvent> GetAllForRepository(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(options, "options");

            return _connection.GetAndFlattenAllPages<IssueEvent>(ApiUrls.IssuesEvents(owner, name), options);
        }

        /// <summary>
        /// Gets a single event
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/events/#get-a-single-event
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The event id</param>
        /// <returns></returns>
        public IObservable<IssueEvent> Get(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _client.Get(owner, name, number).ToObservable();
        }
    }
}