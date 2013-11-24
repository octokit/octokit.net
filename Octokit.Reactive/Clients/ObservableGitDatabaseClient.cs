namespace Octokit.Reactive
{
    public class ObservableGitDatabaseClient : IObservableGitDatabaseClient
    {
        public ObservableGitDatabaseClient(IGitHubClient client)
        {
            Tag = new ObservableTagsClient(client);
            Commit = new ObservableCommitsClient(client);
            Reference = new ObservableReferencesClient(client);
        }

        public IObservableTagsClient Tag { get; set; }
        public IObservableCommitsClient Commit { get; set; }
        public IObservableReferencesClient Reference { get; set; }
    }
}