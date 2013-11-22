namespace Octokit
{
    /// <summary>
    /// Used to maintain api structure therefore contains no methods
    /// </summary>
    public interface IGitDatabaseClient
    {
        ITagsClient Tag { get; set; }
        ICommitsClient Commit { get; set; }
        IReferencesClient Reference { get; set; }
    }
}