using System.Collections.Generic;
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
        /// <param name="number">The pull request number</param>
        Task<IReadOnlyList<PullRequestReviewComment>> GetAll(string owner, string name, int number);

        /// <summary>
        /// Gets review comments for a specified pull request.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-on-a-pull-request</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The pull request number</param>
        Task<IReadOnlyList<PullRequestReviewComment>> GetAll(long repositoryId, int number);

        /// <summary>
        /// Gets review comments for a specified pull request.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-on-a-pull-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <param name="options">Options for changing the API response</param>
        Task<IReadOnlyList<PullRequestReviewComment>> GetAll(string owner, string name, int number, ApiOptions options);

        /// <summary>
        /// Gets review comments for a specified pull request.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-on-a-pull-request</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <param name="options">Options for changing the API response</param>
        Task<IReadOnlyList<PullRequestReviewComment>> GetAll(long repositoryId, int number, ApiOptions options);

        /// <summary>
        /// Gets a list of the pull request review comments in a specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-in-a-repository</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        Task<IReadOnlyList<PullRequestReviewComment>> GetAllForRepository(string owner, string name);

        /// <summary>
        /// Gets a list of the pull request review comments in a specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-in-a-repository</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        Task<IReadOnlyList<PullRequestReviewComment>> GetAllForRepository(long repositoryId);

        /// <summary>
        /// Gets a list of the pull request review comments in a specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-in-a-repository</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        Task<IReadOnlyList<PullRequestReviewComment>> GetAllForRepository(string owner, string name, ApiOptions options);

        /// <summary>
        /// Gets a list of the pull request review comments in a specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-in-a-repository</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        Task<IReadOnlyList<PullRequestReviewComment>> GetAllForRepository(long repositoryId, ApiOptions options);

        /// <summary>
        /// Gets a list of the pull request review comments in a specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-in-a-repository</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">The sorting <see cref="PullRequestReviewCommentRequest">parameters</see></param>
        Task<IReadOnlyList<PullRequestReviewComment>> GetAllForRepository(string owner, string name, PullRequestReviewCommentRequest request);

        /// <summary>
        /// Gets a list of the pull request review comments in a specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-in-a-repository</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">The sorting <see cref="PullRequestReviewCommentRequest">parameters</see></param>
        Task<IReadOnlyList<PullRequestReviewComment>> GetAllForRepository(long repositoryId, PullRequestReviewCommentRequest request);

        /// <summary>
        /// Gets a list of the pull request review comments in a specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-in-a-repository</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">The sorting <see cref="PullRequestReviewCommentRequest">parameters</see></param>
        /// <param name="options">Options for changing the API response</param>
        Task<IReadOnlyList<PullRequestReviewComment>> GetAllForRepository(string owner, string name, PullRequestReviewCommentRequest request, ApiOptions options);

        /// <summary>
        /// Gets a list of the pull request review comments in a specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-in-a-repository</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">The sorting <see cref="PullRequestReviewCommentRequest">parameters</see></param>
        /// <param name="options">Options for changing the API response</param>
        Task<IReadOnlyList<PullRequestReviewComment>> GetAllForRepository(long repositoryId, PullRequestReviewCommentRequest request, ApiOptions options);

        /// <summary>
        /// Gets a single pull request review comment by number.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#get-a-single-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request review comment number</param>
        Task<PullRequestReviewComment> GetComment(string owner, string name, int number);

        /// <summary>
        /// Gets a single pull request review comment by number.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#get-a-single-comment</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The pull request review comment number</param>
        Task<PullRequestReviewComment> GetComment(long repositoryId, int number);

        /// <summary>
        /// Creates a comment on a pull request review.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#create-a-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The Pull Request number</param>
        /// <param name="comment">The comment</param>
        Task<PullRequestReviewComment> Create(string owner, string name, int number, PullRequestReviewCommentCreate comment);

        /// <summary>
        /// Creates a comment on a pull request review.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#create-a-comment</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The Pull Request number</param>
        /// <param name="comment">The comment</param>
        Task<PullRequestReviewComment> Create(long repositoryId, int number, PullRequestReviewCommentCreate comment);

        /// <summary>
        /// Creates a comment on a pull request review as a reply to another comment.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#create-a-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <param name="comment">The comment</param>
        Task<PullRequestReviewComment> CreateReply(string owner, string name, int number, PullRequestReviewCommentReplyCreate comment);

        /// <summary>
        /// Creates a comment on a pull request review as a reply to another comment.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#create-a-comment</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <param name="comment">The comment</param>
        Task<PullRequestReviewComment> CreateReply(long repositoryId, int number, PullRequestReviewCommentReplyCreate comment);

        /// <summary>
        /// Edits a comment on a pull request review.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#edit-a-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request review comment number</param>
        /// <param name="comment">The edited comment</param>
        Task<PullRequestReviewComment> Edit(string owner, string name, int number, PullRequestReviewCommentEdit comment);

        /// <summary>
        /// Edits a comment on a pull request review.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#edit-a-comment</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The pull request review comment number</param>
        /// <param name="comment">The edited comment</param>
        Task<PullRequestReviewComment> Edit(long repositoryId, int number, PullRequestReviewCommentEdit comment);

        /// <summary>
        /// Deletes a comment on a pull request review.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#delete-a-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request review comment number</param>
        Task Delete(string owner, string name, int number);

        /// <summary>
        /// Deletes a comment on a pull request review.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#delete-a-comment</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The pull request review comment number</param>
        Task Delete(long repositoryId, int number);
    }
}
