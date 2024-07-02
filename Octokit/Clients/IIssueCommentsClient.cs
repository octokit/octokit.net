using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Issue Comments API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/issues/comments/">Issue Comments API documentation</a> for more information.
    /// </remarks>
    public interface IIssueCommentsClient
    {
        /// <summary>
        /// Gets a single Issue Comment by id.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#get-a-single-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="commentId">The issue comment id</param>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
             Justification = "Method makes a network request")]
        Task<IssueComment> Get(string owner, string name, long commentId);

        /// <summary>
        /// Gets a single Issue Comment by id.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#get-a-single-comment</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="commentId">The issue comment id</param>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
             Justification = "Method makes a network request")]
        Task<IssueComment> Get(long repositoryId, long commentId);

        /// <summary>
        /// Gets Issue Comments for a repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-in-a-repository</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        Task<IReadOnlyList<IssueComment>> GetAllForRepository(string owner, string name);

        /// <summary>
        /// Gets Issue Comments for a repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-in-a-repository</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        Task<IReadOnlyList<IssueComment>> GetAllForRepository(long repositoryId);

        /// <summary>
        /// Gets Issue Comments for a repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-in-a-repository</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        Task<IReadOnlyList<IssueComment>> GetAllForRepository(string owner, string name, ApiOptions options);

        /// <summary>
        /// Gets Issue Comments for a repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-in-a-repository</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        Task<IReadOnlyList<IssueComment>> GetAllForRepository(long repositoryId, ApiOptions options);

        /// <summary>
        /// Gets Issue Comments for a repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-in-a-repository</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">The sorting <see cref="IssueCommentRequest">parameters</see></param>
        Task<IReadOnlyList<IssueComment>> GetAllForRepository(string owner, string name, IssueCommentRequest request);

        /// <summary>
        /// Gets Issue Comments for a repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-in-a-repository</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">The sorting <see cref="IssueCommentRequest">parameters</see></param>
        Task<IReadOnlyList<IssueComment>> GetAllForRepository(long repositoryId, IssueCommentRequest request);

        /// <summary>
        /// Gets Issue Comments for a repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-in-a-repository</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">The sorting <see cref="IssueCommentRequest">parameters</see></param>
        /// <param name="options">Options for changing the API response</param>
        Task<IReadOnlyList<IssueComment>> GetAllForRepository(string owner, string name, IssueCommentRequest request, ApiOptions options);

        /// <summary>
        /// Gets Issue Comments for a repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-in-a-repository</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">The sorting <see cref="IssueCommentRequest">parameters</see></param>
        /// <param name="options">Options for changing the API response</param>
        Task<IReadOnlyList<IssueComment>> GetAllForRepository(long repositoryId, IssueCommentRequest request, ApiOptions options);

        /// <summary>
        /// Gets Issue Comments for a specified Issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-on-an-issue</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        Task<IReadOnlyList<IssueComment>> GetAllForIssue(string owner, string name, int issueNumber);

        /// <summary>
        /// Gets Issue Comments for a specified Issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-on-an-issue</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        Task<IReadOnlyList<IssueComment>> GetAllForIssue(long repositoryId, int issueNumber);

        /// <summary>
        /// Gets Issue Comments for a specified Issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-on-an-issue</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <param name="options">Options for changing the API response</param>
        Task<IReadOnlyList<IssueComment>> GetAllForIssue(string owner, string name, int issueNumber, ApiOptions options);

        /// <summary>
        /// Gets Issue Comments for a specified Issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-on-an-issue</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <param name="options">Options for changing the API response</param>
        Task<IReadOnlyList<IssueComment>> GetAllForIssue(long repositoryId, int issueNumber, ApiOptions options);

        /// <summary>
        /// Gets Issue Comments for a specified Issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-on-an-issue</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <param name="request">The sorting <see cref="IssueCommentRequest">parameters</see></param>
        Task<IReadOnlyList<IssueComment>> GetAllForIssue(string owner, string name, int issueNumber, IssueCommentRequest request);

        /// <summary>
        /// Gets Issue Comments for a specified Issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-on-an-issue</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <param name="request">The sorting <see cref="IssueCommentRequest">parameters</see></param>
        Task<IReadOnlyList<IssueComment>> GetAllForIssue(long repositoryId, int issueNumber, IssueCommentRequest request);

        /// <summary>
        /// Gets Issue Comments for a specified Issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-on-an-issue</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <param name="request">The sorting <see cref="IssueCommentRequest">parameters</see></param>
        /// <param name="options">Options for changing the API response</param>
        Task<IReadOnlyList<IssueComment>> GetAllForIssue(string owner, string name, int issueNumber, IssueCommentRequest request, ApiOptions options);

        /// <summary>
        /// Gets Issue Comments for a specified Issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-on-an-issue</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <param name="request">The sorting <see cref="IssueCommentRequest">parameters</see></param>
        /// <param name="options">Options for changing the API response</param>
        Task<IReadOnlyList<IssueComment>> GetAllForIssue(long repositoryId, int issueNumber, IssueCommentRequest request, ApiOptions options);

        /// <summary>
        /// Creates a new Issue Comment for a specified Issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#create-a-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="issueNumber">The number of the issue</param>
        /// <param name="newComment">The new comment to add to the issue</param>
        Task<IssueComment> Create(string owner, string name, int issueNumber, string newComment);

        /// <summary>
        /// Creates a new Issue Comment for a specified Issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#create-a-comment</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="issueNumber">The number of the issue</param>
        /// <param name="newComment">The new comment to add to the issue</param>
        Task<IssueComment> Create(long repositoryId, int issueNumber, string newComment);

        /// <summary>
        /// Updates a specified Issue Comment.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#edit-a-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="commentId">The comment id</param>
        /// <param name="commentUpdate">The modified comment</param>
        Task<IssueComment> Update(string owner, string name, long commentId, string commentUpdate);

        /// <summary>
        /// Updates a specified Issue Comment.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#edit-a-comment</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="commentId">The comment id</param>
        /// <param name="commentUpdate">The modified comment</param>
        Task<IssueComment> Update(long repositoryId, long commentId, string commentUpdate);

        /// <summary>
        /// Deletes the specified Issue Comment
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#delete-a-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="commentId">The comment id</param>
        Task Delete(string owner, string name, long commentId);

        /// <summary>
        /// Deletes the specified Issue Comment
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#delete-a-comment</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="commentId">The comment id</param>
        Task Delete(long repositoryId, long commentId);
    }
}
