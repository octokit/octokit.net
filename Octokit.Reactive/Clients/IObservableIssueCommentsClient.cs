using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Issue Comments API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/issues/comments/">Issue Comments API documentation</a> for more information.
    /// </remarks>
    public interface IObservableIssueCommentsClient
    {
        /// <summary>
        /// Gets a single Issue Comment by id.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#get-a-single-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="id">The issue comment id</param>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
            Justification = "Method makes a network request")]
        IObservable<IssueComment> Get(string owner, string name, int id);

        /// <summary>
        /// Gets a single Issue Comment by id.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#get-a-single-comment</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="id">The issue comment id</param>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
            Justification = "Method makes a network request")]
        IObservable<IssueComment> Get(long repositoryId, int id);

        /// <summary>
        /// Gets Issue Comments for a repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-in-a-repository</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        IObservable<IssueComment> GetAllForRepository(string owner, string name);

        /// <summary>
        /// Gets Issue Comments for a repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-in-a-repository</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        IObservable<IssueComment> GetAllForRepository(long repositoryId);

        /// <summary>
        /// Gets Issue Comments for a repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-in-a-repository</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        IObservable<IssueComment> GetAllForRepository(string owner, string name, ApiOptions options);

        /// <summary>
        /// Gets Issue Comments for a repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-in-a-repository</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        IObservable<IssueComment> GetAllForRepository(long repositoryId, ApiOptions options);

        /// <summary>
        /// Gets Issue Comments for a repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-in-a-repository</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">The sorting <see cref="IssueCommentRequest">parameters</see></param>
        IObservable<IssueComment> GetAllForRepository(string owner, string name, IssueCommentRequest request);

        /// <summary>
        /// Gets Issue Comments for a repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-in-a-repository</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">The sorting <see cref="IssueCommentRequest">parameters</see></param>
        IObservable<IssueComment> GetAllForRepository(long repositoryId, IssueCommentRequest request);

        /// <summary>
        /// Gets Issue Comments for a repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-in-a-repository</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">The sorting <see cref="IssueCommentRequest">parameters</see></param>
        /// <param name="options">Options for changing the API response</param>
        IObservable<IssueComment> GetAllForRepository(string owner, string name, IssueCommentRequest request, ApiOptions options);

        /// <summary>
        /// Gets Issue Comments for a repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-in-a-repository</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">The sorting <see cref="IssueCommentRequest">parameters</see></param>
        /// <param name="options">Options for changing the API response</param>
        IObservable<IssueComment> GetAllForRepository(long repositoryId, IssueCommentRequest request, ApiOptions options);

        /// <summary>
        /// Gets Issue Comments for a specified Issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-on-an-issue</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue number</param>
        IObservable<IssueComment> GetAllForIssue(string owner, string name, int number);

        /// <summary>
        /// Gets Issue Comments for a specified Issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-on-an-issue</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The issue number</param>
        IObservable<IssueComment> GetAllForIssue(long repositoryId, int number);

        /// <summary>
        /// Gets Issue Comments for a specified Issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-on-an-issue</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue number</param>
        /// <param name="options">Options for changing the API response</param>
        IObservable<IssueComment> GetAllForIssue(string owner, string name, int number, ApiOptions options);

        /// <summary>
        /// Gets Issue Comments for a specified Issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-on-an-issue</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The issue number</param>
        /// <param name="options">Options for changing the API response</param>
        IObservable<IssueComment> GetAllForIssue(long repositoryId, int number, ApiOptions options);

        /// <summary>
        /// Gets Issue Comments for a specified Issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-on-an-issue</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue number</param>
        /// <param name="request">The sorting <see cref="IssueCommentRequest">parameters</see></param>
        IObservable<IssueComment> GetAllForIssue(string owner, string name, int number, IssueCommentRequest request);

        /// <summary>
        /// Gets Issue Comments for a specified Issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-on-an-issue</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The issue number</param>
        /// <param name="request">The sorting <see cref="IssueCommentRequest">parameters</see></param>
        IObservable<IssueComment> GetAllForIssue(long repositoryId, int number, IssueCommentRequest request);

        /// <summary>
        /// Gets Issue Comments for a specified Issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-on-an-issue</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue number</param>
        /// <param name="request">The sorting <see cref="IssueCommentRequest">parameters</see></param>
        /// <param name="options">Options for changing the API response</param>
        IObservable<IssueComment> GetAllForIssue(string owner, string name, int number, IssueCommentRequest request, ApiOptions options);

        /// <summary>
        /// Gets Issue Comments for a specified Issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-on-an-issue</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The issue number</param>
        /// <param name="request">The sorting <see cref="IssueCommentRequest">parameters</see></param>
        /// <param name="options">Options for changing the API response</param>
        IObservable<IssueComment> GetAllForIssue(long repositoryId, int number, IssueCommentRequest request, ApiOptions options);

        /// <summary>
        /// Creates a new Issue Comment for a specified Issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#create-a-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The number of the issue</param>
        /// <param name="newComment">The text of the new comment</param>
        IObservable<IssueComment> Create(string owner, string name, int number, string newComment);

        /// <summary>
        /// Creates a new Issue Comment for a specified Issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#create-a-comment</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The number of the issue</param>
        /// <param name="newComment">The text of the new comment</param>
        IObservable<IssueComment> Create(long repositoryId, int number, string newComment);

        /// <summary>
        /// Updates a specified Issue Comment.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#edit-a-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="id">The comment id</param>
        /// <param name="commentUpdate">The modified comment</param>
        IObservable<IssueComment> Update(string owner, string name, int id, string commentUpdate);

        /// <summary>
        /// Updates a specified Issue Comment.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#edit-a-comment</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="id">The comment id</param>
        /// <param name="commentUpdate">The modified comment</param>
        IObservable<IssueComment> Update(long repositoryId, int id, string commentUpdate);

        /// <summary>
        /// Deletes the specified Issue Comment
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#delete-a-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="id">The comment id</param>
        IObservable<Unit> Delete(string owner, string name, int id);

        /// <summary>
        /// Deletes the specified Issue Comment
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#delete-a-comment</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="id">The comment id</param>
        IObservable<Unit> Delete(long repositoryId, int id);
    }
}
