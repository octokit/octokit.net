using System.Threading.Tasks;
using System.Collections.Generic;

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
        /// <param name="number">The issue number</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/issues/{issue_number}/events")]
        public Task<IReadOnlyList<IssueEvent>> GetAllForIssue(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllForIssue(owner, name, number, ApiOptions.None);
        }

        /// <summary>
        /// Gets all events for the issue.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/events/#list-events-for-an-issue
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The issue number</param>
        [ManualRoute("GET", "/repositories/{id}/issues/{number}/events")]
        public Task<IReadOnlyList<IssueEvent>> GetAllForIssue(long repositoryId, int number)
        {
            return GetAllForIssue(repositoryId, number, ApiOptions.None);
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
        [ManualRoute("GET", "/repos/{owner}/{repo}/issues/{issue_number}/events")]
        public Task<IReadOnlyList<IssueEvent>> GetAllForIssue(string owner, string name, int number, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<IssueEvent>(ApiUrls.IssuesEvents(owner, name, number), null, options);
        }

        /// <summary>
        /// Gets all events for the issue.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/events/#list-events-for-an-issue
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The issue number</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/repositories/{id}/issues/{number}/events")]
        public Task<IReadOnlyList<IssueEvent>> GetAllForIssue(long repositoryId, int number, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<IssueEvent>(ApiUrls.IssuesEvents(repositoryId, number), null, options);
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
        public Task<IReadOnlyList<IssueEvent>> GetAllForRepository(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllForRepository(owner, name, ApiOptions.None);
        }

        /// <summary>
        /// Gets all events for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/events/#list-events-for-a-repository
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        [ManualRoute("GET", "/repositories/{id}/issues/events")]
        public Task<IReadOnlyList<IssueEvent>> GetAllForRepository(long repositoryId)
        {
            return GetAllForRepository(repositoryId, ApiOptions.None);
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
        public Task<IReadOnlyList<IssueEvent>> GetAllForRepository(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<IssueEvent>(ApiUrls.IssuesEvents(owner, name), null, options);
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
        public Task<IReadOnlyList<IssueEvent>> GetAllForRepository(long repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<IssueEvent>(ApiUrls.IssuesEvents(repositoryId), null, options);
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
        public Task<IssueEvent> Get(string owner, string name, long eventId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Get<IssueEvent>(ApiUrls.IssuesEvent(owner, name, eventId), null);
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
        public Task<IssueEvent> Get(long repositoryId, long eventId)
        {
            return ApiConnection.Get<IssueEvent>(ApiUrls.IssuesEvent(repositoryId, eventId), null);
        }
    }
}
