using System;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Issue Timeline API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/issues/timeline/">Issue Timeline API documentation</a> for more information.
    /// </remarks>
    public interface IObservableIssueTimelineClient
    {
        /// <summary>
        /// Gets all the various events that have occurred around an issue or pull request.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/issues/timeline/#list-events-for-an-issue
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repo">The name of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        IObservable<TimelineEventInfo> GetAllForIssue(string owner, string repo, long issueNumber);

        /// <summary>
        /// Gets all the various events that have occurred around an issue or pull request.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/issues/timeline/#list-events-for-an-issue
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repo">The name of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <param name="options">Options for changing the API response</param>
        IObservable<TimelineEventInfo> GetAllForIssue(string owner, string repo, long issueNumber, ApiOptions options);

        /// <summary>
        /// Gets all the various events that have occurred around an issue or pull request.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/issues/timeline/#list-events-for-an-issue
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        IObservable<TimelineEventInfo> GetAllForIssue(long repositoryId, long issueNumber);

        /// <summary>
        /// Gets all the various events that have occurred around an issue or pull request.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/issues/timeline/#list-events-for-an-issue
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <param name="options">Options for changing the API response</param>
        IObservable<TimelineEventInfo> GetAllForIssue(long repositoryId, long issueNumber, ApiOptions options);
    }
}
