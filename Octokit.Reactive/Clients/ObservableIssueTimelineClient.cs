using System;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservableIssueTimelineClient : IObservableIssueTimelineClient
    {
        readonly IConnection _connection;

        public ObservableIssueTimelineClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _connection = client.Connection;
        }

        public IObservable<TimelineEventInfo> GetAllForIssue(string owner, string repo, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repo, "repo");

            return _connection.GetAndFlattenAllPages<TimelineEventInfo>(ApiUrls.IssueTimeline(owner, repo, number), ApiOptions.None);
        }
    }
}
