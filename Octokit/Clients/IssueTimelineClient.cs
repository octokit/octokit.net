using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Issue Timeline API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/issues/timeline/">Issue Timeline API documentation</a> for more information.
    /// </remarks>
    public class IssueTimelineClient : ApiClient, IIssueTimelineClient
    {
        public IssueTimelineClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        /// <summary>
        /// Gets all the various events that have occurred around an issue or pull request.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/issues/timeline/#list-events-for-an-issue
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repo">The name of the repository</param>
        /// <param name="number">The issue number</param>
        public Task<IReadOnlyList<TimelineEventInfo>> GetAllForIssue(string owner, string repo, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repo, "repo");

            return GetAllForIssue(owner, repo, number, ApiOptions.None);
        }

        /// <summary>
        /// Gets all the various events that have occurred around an issue or pull request.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/issues/timeline/#list-events-for-an-issue
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repo">The name of the repository</param>
        /// <param name="number">The issue number</param>
        /// <param name="options">Options for changing the API repsonse</param>
        public Task<IReadOnlyList<TimelineEventInfo>> GetAllForIssue(string owner, string repo, int number, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repo, "repo");
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<TimelineEventInfo>(ApiUrls.IssueTimeline(owner, repo, number), null, AcceptHeaders.IssueTimelineApiPreview, options);
        }

        /// <summary>
        /// Gets all the various events that have occurred around an issue or pull request.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/issues/timeline/#list-events-for-an-issue
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The issue number</param>
        public Task<IReadOnlyList<TimelineEventInfo>> GetAllForIssue(long repositoryId, int number)
        {
            return GetAllForIssue(repositoryId, number, ApiOptions.None);
        }

        /// <summary>
        /// Gets all the various events that have occurred around an issue or pull request.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/issues/timeline/#list-events-for-an-issue
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The issue number</param>
        /// <param name="options">Options for changing the API response</param>
        public Task<IReadOnlyList<TimelineEventInfo>> GetAllForIssue(long repositoryId, int number, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<TimelineEventInfo>(ApiUrls.IssueTimeline(repositoryId, number), null, AcceptHeaders.IssueTimelineApiPreview, options);
        }
    }
}
