namespace Octokit.Reactive
{
    /// <summary>
    /// Used to maintain api structure therefore contains no methods
    /// </summary>
    public interface IObservableGitDatabaseClient
    {
        IObservableBlobsClient Blob { get; }
        IObservableTagsClient Tag { get; }
        IObservableTreesClient Tree { get; }
        IObservableCommitsClient Commit { get; }
        IObservableReferencesClient Reference { get; }
    }
}