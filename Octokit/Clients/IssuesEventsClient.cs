using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Issue Events API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/issues/events/">Issue Events API documentation</a> for more information.
    /// </remarks>
    public class IssuesEventsClient : ApiClient, IIssuesEventsClient
    {
        public IssuesEventsClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        /// <summary>
        /// Gets all events for the issue.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/events/#list-events-for-an-issue
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/issues/{issue_number}/events")]
        public Task<IReadOnlyList<IssueEvent>> GetAllForIssue(string owner, string name, long issueNumber, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllForIssue(owner, name, issueNumber, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets all events for the issue.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/events/#list-events-for-an-issue
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        [ManualRoute("GET", "/repositories/{id}/issues/{number}/events")]
        public Task<IReadOnlyList<IssueEvent>> GetAllForIssue(long repositoryId, long issueNumber, CancellationToken cancellationToken = default)
        {
            return GetAllForIssue(repositoryId, issueNumber, ApiOptions.None, cancellationToken);
        }

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
        [ManualRoute("GET", "/repos/{owner}/{repo}/issues/{issue_number}/events")]
        public Task<IReadOnlyList<IssueEvent>> GetAllForIssue(string owner, string name, long issueNumber, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<IssueEvent>(ApiUrls.IssuesEvents(owner, name, issueNumber), null, options, cancellationToken);
        }

        /// <summary>
        /// Gets all events for the issue.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/events/#list-events-for-an-issue
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/repositories/{id}/issues/{number}/events")]
        public Task<IReadOnlyList<IssueEvent>> GetAllForIssue(long repositoryId, long issueNumber, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<IssueEvent>(ApiUrls.IssuesEvents(repositoryId, issueNumber), null, options, cancellationToken);
        }

        /// <summary>
        /// Gets all events for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/events/#list-events-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/issues/events")]
        public Task<IReadOnlyList<IssueEvent>> GetAllForRepository(string owner, string name, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllForRepository(owner, name, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets all events for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/events/#list-events-for-a-repository
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        [ManualRoute("GET", "/repositories/{id}/issues/events")]
        public Task<IReadOnlyList<IssueEvent>> GetAllForRepository(long repositoryId, CancellationToken cancellationToken = default)
        {
            return GetAllForRepository(repositoryId, ApiOptions.None, cancellationToken);
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
        [ManualRoute("GET", "/repos/{owner}/{repo}/issues/events")]
        public Task<IReadOnlyList<IssueEvent>> GetAllForRepository(string owner, string name, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<IssueEvent>(ApiUrls.IssuesEvents(owner, name), null, options, cancellationToken);
        }

        /// <summary>
        /// Gets all events for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/events/#list-events-for-a-repository
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/repositories/{id}/issues/events")]
        public Task<IReadOnlyList<IssueEvent>> GetAllForRepository(long repositoryId, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<IssueEvent>(ApiUrls.IssuesEvents(repositoryId), null, options, cancellationToken);
        }

        /// <summary>
        /// Gets a single event
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/events/#get-a-single-event
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="eventId">The event id</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/issues/events/{event_id}")]
        public Task<IssueEvent> Get(string owner, string name, long eventId, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Get<IssueEvent>(ApiUrls.IssuesEvent(owner, name, eventId), null, cancellationToken);
        }

        /// <summary>
        /// Gets a single event
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/events/#get-a-single-event
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="eventId">The event id</param>
        [ManualRoute("GET", "/repositories/{id}/issues/events/{event_id}")]
        public Task<IssueEvent> Get(long repositoryId, long eventId, CancellationToken cancellationToken = default)
        {
            return ApiConnection.Get<IssueEvent>(ApiUrls.IssuesEvent(repositoryId, eventId), null, cancellationToken);
        }
    }
}
