namespace Octokit.Reactive
{
    public class ObservableGitDatabaseClient : IObservableGitDatabaseClient
    {
        public ObservableGitDatabaseClient(IGitHubClient client)
        {
            Tag = new ObservableTagsClient(client);
            Commit = new ObservableCommitsClient(client);
        }

        public IObservableTagsClient Tag { get; set; }
        public IObservableCommitsClient Commit { get; set; }
    }
}