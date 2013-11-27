namespace Octokit
{
    /// <summary>
    /// Used to maintain api structure therefore contains no methods
    /// </summary>
    public interface IGitDatabaseClient
    {
        IBlobsClient Blob { get; set; }
        ITagsClient Tag { get; set; }
        ITreesClient Tree { get; set; }
        ICommitsClient Commit { get; set; }
        IReferencesClient Reference { get; set; }
    }
}