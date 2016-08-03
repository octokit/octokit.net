using System;

namespace Octokit.Reactive
{
    public interface IObservableIssueTimelineClient
    {
        IObservable<TimelineEventInfo> GetAllForIssue(string owner, string repo, int number);
    }
}
