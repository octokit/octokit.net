namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Git Database API. Gives you access to read and write raw Git objects and to list and update your references.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/git/">Git API documentation</a> for more information.
    /// </remarks>
    public interface IGitDatabaseClient
    {
        IBlobsClient Blob { get; }
        ITagsClient Tag { get; }
        ITreesClient Tree { get; }
        ICommitsClient Commit { get; }
        IReferencesClient Reference { get; }
    }
}