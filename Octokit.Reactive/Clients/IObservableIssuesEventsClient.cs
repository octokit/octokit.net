using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Issue Events API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/issues/events/">Issue Events API documentation</a> for more information.
    /// </remarks>
    public interface IObservableIssuesEventsClient
    {
        /// <summary>
        /// Gets all events for the issue.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/events/#list-events-for-an-issue
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<IssueEvent> GetAllForIssue(string owner, string name, long issueNumber, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all events for the issue.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/events/#list-events-for-an-issue
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<IssueEvent> GetAllForIssue(long repositoryId, long issueNumber, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all events for the issue.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/events/#list-events-for-an-issue
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<IssueEvent> GetAllForIssue(string owner, string name, long issueNumber, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all events for the issue.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/events/#list-events-for-an-issue
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<IssueEvent> GetAllForIssue(long repositoryId, long issueNumber, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all events for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/events/#list-events-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<IssueEvent> GetAllForRepository(string owner, string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all events for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/events/#list-events-for-a-repository
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<IssueEvent> GetAllForRepository(long repositoryId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all events for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/events/#list-events-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<IssueEvent> GetAllForRepository(string owner, string name, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all events for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/events/#list-events-for-a-repository
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<IssueEvent> GetAllForRepository(long repositoryId, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a single event
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/events/#get-a-single-event
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="eventId">The event id</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
        Justification = "Method makes a network request")]
        IObservable<IssueEvent> Get(string owner, string name, long eventId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a single event
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/events/#get-a-single-event
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="eventId">The event id</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
        Justification = "Method makes a network request")]
        IObservable<IssueEvent> Get(long repositoryId, long eventId, CancellationToken cancellationToken = default);
    }
}
