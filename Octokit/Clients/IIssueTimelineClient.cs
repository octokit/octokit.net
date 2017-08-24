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
    public interface IIssueTimelineClient
    {
        /// <summary>
        /// Gets all the various events that have occurred around an issue or pull request.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/issues/timeline/#list-events-for-an-issue
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repo">The name of the repository</param>
        /// <param name="number">The issue number</param>
        Task<IReadOnlyList<TimelineEventInfo>> GetAllForIssue(string owner, string repo, int number);

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
        Task<IReadOnlyList<TimelineEventInfo>> GetAllForIssue(string owner, string repo, int number, ApiOptions options);

        /// <summary>
        /// Gets all the various events that have occurred around an issue or pull request.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/issues/timeline/#list-events-for-an-issue
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The issue number</param>
        Task<IReadOnlyList<TimelineEventInfo>> GetAllForIssue(long repositoryId, int number);

        /// <summary>
        /// Gets all the various events that have occurred around an issue or pull request.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/issues/timeline/#list-events-for-an-issue
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The issue number</param>
        /// <param name="options">Options for changing the API response</param>
        Task<IReadOnlyList<TimelineEventInfo>> GetAllForIssue(long repositoryId, int number, ApiOptions options);
    }
}
