using System;

namespace Octokit.Reactive
{
    public class ObservableReactionsClient : IObservableReactionsClient
    {
        public ObservableReactionsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            CommitComment = new ObservableCommitCommentReactionsClient(client);
            Issue = new ObservableIssueReactionsClient(client);
        }

        public IObservableCommitCommentReactionsClient CommitComment { get; private set; }

        public IObservableIssueReactionsClient Issue { get; private set; }
    }
}
