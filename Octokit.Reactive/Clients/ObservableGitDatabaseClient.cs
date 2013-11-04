namespace Octokit.Reactive
{
    public class ObservableGitDatabaseClient : IObservableGitDatabaseClient
    {
        public ObservableGitDatabaseClient(IGitHubClient client)
        {
            this.Tag = new ObservableTagsClient(client);
        }

        public IObservableTagsClient Tag { get; set; }
    }
}