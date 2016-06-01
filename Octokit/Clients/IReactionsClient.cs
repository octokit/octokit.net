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
        /// Access GitHub's Reactions API for Commit Comments.
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://developer.github.com/v3/reactions/
        /// </remarks>
        ICommitCommentReactionsClient CommitComment { get; }

        /// <summary>
        /// Access GitHub's Reactions API for Issues.
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://developer.github.com/v3/reactions/
        /// </remarks>
        IIssueReactionsClient Issue { get; }

        /// <summary>
        /// Access GitHub's Reactions API for Issue Comments.
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://developer.github.com/v3/reactions/
        /// </remarks>
        IIssueCommentReactionsClient IssueComment { get; }

        /// <summary>
        /// Access GitHub's Reactions API for Issue Comments.
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://developer.github.com/v3/reactions/
        /// </remarks>
        IPullRequestReviewCommentReactionsClient PullRequestReviewComment { get; }
    }
}
