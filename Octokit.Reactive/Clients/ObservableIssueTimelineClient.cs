using System;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Issue Timeline API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/issues/timeline/">Issue Timeline API documentation</a> for more information.
    /// </remarks>
    public class ObservableIssueTimelineClient : IObservableIssueTimelineClient
    {
        readonly IConnection _connection;

        public ObservableIssueTimelineClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _connection = client.Connection;
        }

        /// <summary>
        /// Gets all the various events that have occurred around an issue or pull request.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/issues/timeline/#list-events-for-an-issue
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repo">The name of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        public IObservable<TimelineEventInfo> GetAllForIssue(string owner, string repo, long issueNumber)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repo, nameof(repo));

            return GetAllForIssue(owner, repo, issueNumber, ApiOptions.None);
        }

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
        public IObservable<TimelineEventInfo> GetAllForIssue(string owner, string repo, long issueNumber, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repo, nameof(repo));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<TimelineEventInfo>(ApiUrls.IssueTimeline(owner, repo, issueNumber), null, options);
        }

        /// <summary>
        /// Gets all the various events that have occurred around an issue or pull request.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/issues/timeline/#list-events-for-an-issue
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        public IObservable<TimelineEventInfo> GetAllForIssue(long repositoryId, long issueNumber)
        {
            return GetAllForIssue(repositoryId, issueNumber, ApiOptions.None);
        }

        /// <summary>
        /// Gets all the various events that have occurred around an issue or pull request.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/issues/timeline/#list-events-for-an-issue
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<TimelineEventInfo> GetAllForIssue(long repositoryId, long issueNumber, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<TimelineEventInfo>(ApiUrls.IssueTimeline(repositoryId, issueNumber), null, options);
        }
    }
}
