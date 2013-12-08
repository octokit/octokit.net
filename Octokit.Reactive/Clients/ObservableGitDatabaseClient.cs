namespace Octokit.Reactive
{
    public class ObservableGitDatabaseClient : IObservableGitDatabaseClient
    {
        public ObservableGitDatabaseClient(IGitHubClient client)
        {
            Blob = new ObservableBlobClient(client);
            Tag = new ObservableTagsClient(client);
            Tree = new ObservableTreesClient(client);
            Commit = new ObservableCommitsClient(client);
            Reference = new ObservableReferencesClient(client);
        }

        public IObservableBlobClient Blob { get; set; }
        public IObservableTagsClient Tag { get; set; }
        public IObservableTreesClient Tree { get; set; }
        public IObservableCommitsClient Commit { get; set; }
        public IObservableReferencesClient Reference { get; set; }
    }
}