using System;

namespace Octokit.Reactive
{
    public class ObservableReactionsClient : IObservableReactionsClient
    {
        public ObservableReactionsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            CommitComment = new ObservableCommitCommentsReactionClient(client);
            Issue = new ObservableIssuesReactionsClient(client);
        }

        public IObservableCommitCommentsReactionsClient CommitComment { get; private set; }

        public IObservableIssuesReactionsClient Issue { get; private set; }
    }
}
