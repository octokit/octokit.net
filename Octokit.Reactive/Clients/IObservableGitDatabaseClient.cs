namespace Octokit.Reactive
{
    /// <summary>
    /// Used to maintain api structure therefore contains no methods
    /// </summary>
    public interface IObservableGitDatabaseClient
    {
        IObservableBlobClient Blob { get; set; }
        IObservableTagsClient Tag { get; set; }
        IObservableTreesClient Tree { get; set; }
        IObservableCommitsClient Commit { get; set; }
        IObservableReferencesClient Reference { get; set; }
    }
}