using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Pull Request Review Comments API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/pulls/comments/">Review Comments API documentation</a> for more information.
    /// </remarks>
    public interface IPullRequestReviewCommentsClient
    {
        /// <summary>
        /// Gets review comments for a specified pull request.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-on-a-pull-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<PullRequestReviewComment>> GetAll(string owner, string name, int pullRequestNumber, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets review comments for a specified pull request.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-on-a-pull-request</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<PullRequestReviewComment>> GetAll(long repositoryId, int pullRequestNumber, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets review comments for a specified pull request.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-on-a-pull-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<PullRequestReviewComment>> GetAll(string owner, string name, int pullRequestNumber, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets review comments for a specified pull request.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-on-a-pull-request</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<PullRequestReviewComment>> GetAll(long repositoryId, int pullRequestNumber, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a list of the pull request review comments in a specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-in-a-repository</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<PullRequestReviewComment>> GetAllForRepository(string owner, string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a list of the pull request review comments in a specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-in-a-repository</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<PullRequestReviewComment>> GetAllForRepository(long repositoryId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a list of the pull request review comments in a specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-in-a-repository</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<PullRequestReviewComment>> GetAllForRepository(string owner, string name, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a list of the pull request review comments in a specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-in-a-repository</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<PullRequestReviewComment>> GetAllForRepository(long repositoryId, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a list of the pull request review comments in a specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-in-a-repository</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">The sorting <see cref="PullRequestReviewCommentRequest">parameters</see></param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<PullRequestReviewComment>> GetAllForRepository(string owner, string name, PullRequestReviewCommentRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a list of the pull request review comments in a specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-in-a-repository</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">The sorting <see cref="PullRequestReviewCommentRequest">parameters</see></param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<PullRequestReviewComment>> GetAllForRepository(long repositoryId, PullRequestReviewCommentRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a list of the pull request review comments in a specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-in-a-repository</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">The sorting <see cref="PullRequestReviewCommentRequest">parameters</see></param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<PullRequestReviewComment>> GetAllForRepository(string owner, string name, PullRequestReviewCommentRequest request, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a list of the pull request review comments in a specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-in-a-repository</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">The sorting <see cref="PullRequestReviewCommentRequest">parameters</see></param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<PullRequestReviewComment>> GetAllForRepository(long repositoryId, PullRequestReviewCommentRequest request, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a single pull request review comment by number.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#get-a-single-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="commentId">The pull request review comment id</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<PullRequestReviewComment> GetComment(string owner, string name, long commentId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a single pull request review comment by number.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#get-a-single-comment</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="commentId">The pull request review comment id</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<PullRequestReviewComment> GetComment(long repositoryId, long commentId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a comment on a pull request review.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#create-a-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="comment">The comment</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<PullRequestReviewComment> Create(string owner, string name, int pullRequestNumber, PullRequestReviewCommentCreate comment, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a comment on a pull request review.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#create-a-comment</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="comment">The comment</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<PullRequestReviewComment> Create(long repositoryId, int pullRequestNumber, PullRequestReviewCommentCreate comment, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a comment on a pull request review as a reply to another comment.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#create-a-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="comment">The comment</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<PullRequestReviewComment> CreateReply(string owner, string name, int pullRequestNumber, PullRequestReviewCommentReplyCreate comment, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a comment on a pull request review as a reply to another comment.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#create-a-comment</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="comment">The comment</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<PullRequestReviewComment> CreateReply(long repositoryId, int pullRequestNumber, PullRequestReviewCommentReplyCreate comment, CancellationToken cancellationToken = default);

        /// <summary>
        /// Edits a comment on a pull request review.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#edit-a-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="commentId">The pull request review comment id</param>
        /// <param name="comment">The edited comment</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<PullRequestReviewComment> Edit(string owner, string name, long commentId, PullRequestReviewCommentEdit comment, CancellationToken cancellationToken = default);

        /// <summary>
        /// Edits a comment on a pull request review.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#edit-a-comment</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="commentId">The pull request review comment id</param>
        /// <param name="comment">The edited comment</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<PullRequestReviewComment> Edit(long repositoryId, long commentId, PullRequestReviewCommentEdit comment, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a comment on a pull request review.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#delete-a-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="commentId">The pull request review comment id</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task Delete(string owner, string name, long commentId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a comment on a pull request review.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#delete-a-comment</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="commentId">The pull request review comment id</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task Delete(long repositoryId, long commentId, CancellationToken cancellationToken = default);
    }
}
