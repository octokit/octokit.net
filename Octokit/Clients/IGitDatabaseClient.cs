namespace Octokit
{
    /// <summary>
    /// Used to maintain api structure therefore contains no methods
    /// </summary>
    public interface IGitDatabaseClient
    {
        IBlobsClient Blob { get; }
        ITagsClient Tag { get; }
        ITreesClient Tree { get; }
        ICommitsClient Commit { get; }
        IReferencesClient Reference { get; }
    }
}