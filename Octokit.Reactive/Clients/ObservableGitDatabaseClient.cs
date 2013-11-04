namespace Octokit.Reactive
{
    public class ObservableGitDatabaseClient : IObservableGitDatabaseClient
    {
        public ObservableGitDatabaseClient(IGitHubClient client)
        {
            Tag = new ObservableTagsClient(client);
        }

        public IObservableTagsClient Tag { get; set; }
    }
}