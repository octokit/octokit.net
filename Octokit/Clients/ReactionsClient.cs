using System;
using System.Threading.Tasks;

namespace Octokit
{
    public class ReactionsClient : ApiClient, IReactionsClient
    {
        /// <summary>
        /// Instantiates a new GitHub Reactions API client
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public ReactionsClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
            CommitComment = new CommitCommentReactionsClient(apiConnection);
            Issue = new IssueReactionsClient(apiConnection);
            IssueComment = new IssueCommentReactionsClient(apiConnection);
            PullRequestReviewComment = new PullRequestReviewCommentReactionsClient(apiConnection);
        }

        /// <summary>
        /// Access GitHub's Reactions API for Commit Comments.
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://developer.github.com/v3/reactions/
        /// </remarks>
        public ICommitCommentReactionsClient CommitComment { get; private set; }

        /// <summary>
        /// Access GitHub's Reactions API for Issues.
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://developer.github.com/v3/reactions/
        /// </remarks>
        public IIssueReactionsClient Issue { get; private set; }

        /// <summary>
        /// Access GitHub's Reactions API for Issue Comments.
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://developer.github.com/v3/reactions/
        /// </remarks>
        public IIssueCommentReactionsClient IssueComment { get; private set; }

        /// <summary>
        /// Access GitHub's Reactions API for Pull Request Review Comments.
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://developer.github.com/v3/reactions/
        /// </remarks>
        public IPullRequestReviewCommentReactionsClient PullRequestReviewComment { get; private set; }
    }
}
