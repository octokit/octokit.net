namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Reactions Events API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/reactions/">Reactions API documentation</a> for more information
    /// </remarks>
    public interface IReactionsClient
    {
        /// <summary>
        /// Access GitHub's Reactions API.
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://developer.github.com/v3/reactions/
        /// </remarks>
        ICommitCommentReaction CommitComments { get; }
    }
}
