using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;
using System.Threading;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Issue Comments API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/issues/comments/">Issue Comments API documentation</a> for more information.
    /// </remarks>
    public class ObservableIssueCommentsClient : IObservableIssueCommentsClient
    {
        readonly IIssueCommentsClient _client;
        readonly IConnection _connection;

        public ObservableIssueCommentsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Issue.Comment;
            _connection = client.Connection;
        }

        /// <summary>
        /// Gets a single Issue Comment by id.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#get-a-single-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="commentId">The issue comment id</param>
        public IObservable<IssueComment> Get(string owner, string name, long commentId, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Get(owner, name, commentId, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Gets a single Issue Comment by id.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#get-a-single-comment</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="commentId">The issue comment id</param>
        public IObservable<IssueComment> Get(long repositoryId, long commentId, CancellationToken cancellationToken = default)
        {
            return _client.Get(repositoryId, commentId, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Gets Issue Comments for a repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-in-a-repository</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        public IObservable<IssueComment> GetAllForRepository(string owner, string name, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllForRepository(owner, name, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets Issue Comments for a repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-in-a-repository</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        public IObservable<IssueComment> GetAllForRepository(long repositoryId, CancellationToken cancellationToken = default)
        {
            return GetAllForRepository(repositoryId, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets Issue Comments for a repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-in-a-repository</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<IssueComment> GetAllForRepository(string owner, string name, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return GetAllForRepository(owner, name, new IssueCommentRequest(), options);
        }

        /// <summary>
        /// Gets Issue Comments for a repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-in-a-repository</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<IssueComment> GetAllForRepository(long repositoryId, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return GetAllForRepository(repositoryId, new IssueCommentRequest(), options);
        }

        /// <summary>
        /// Gets Issue Comments for a repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-in-a-repository</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">The sorting <see cref="IssueCommentRequest">parameters</see></param>
        public IObservable<IssueComment> GetAllForRepository(string owner, string name, IssueCommentRequest request, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAllForRepository(owner, name, request, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets Issue Comments for a repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-in-a-repository</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">The sorting <see cref="IssueCommentRequest">parameters</see></param>
        public IObservable<IssueComment> GetAllForRepository(long repositoryId, IssueCommentRequest request, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAllForRepository(repositoryId, request, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets Issue Comments for a repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-in-a-repository</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">The sorting <see cref="IssueCommentRequest">parameters</see></param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<IssueComment> GetAllForRepository(string owner, string name, IssueCommentRequest request, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<IssueComment>(ApiUrls.IssueComments(owner, name), request.ToParametersDictionary(), options);
        }

        /// <summary>
        /// Gets Issue Comments for a repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-in-a-repository</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">The sorting <see cref="IssueCommentRequest">parameters</see></param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<IssueComment> GetAllForRepository(long repositoryId, IssueCommentRequest request, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<IssueComment>(ApiUrls.IssueComments(repositoryId), request.ToParametersDictionary(), options, cancellationToken);
        }

        /// <summary>
        /// Gets Issue Comments for a specified Issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-on-an-issue</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        public IObservable<IssueComment> GetAllForIssue(string owner, string name, long issueNumber, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllForIssue(owner, name, issueNumber, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets Issue Comments for a specified Issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-on-an-issue</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        public IObservable<IssueComment> GetAllForIssue(long repositoryId, long issueNumber, CancellationToken cancellationToken = default)
        {
            return GetAllForIssue(repositoryId, issueNumber, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets Issue Comments for a specified Issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-on-an-issue</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<IssueComment> GetAllForIssue(string owner, string name, long issueNumber, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return GetAllForIssue(owner, name, issueNumber, new IssueCommentRequest(), options);
        }

        /// <summary>
        /// Gets Issue Comments for a specified Issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-on-an-issue</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<IssueComment> GetAllForIssue(long repositoryId, long issueNumber, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return GetAllForIssue(repositoryId, issueNumber, new IssueCommentRequest(), options);
        }

        /// <summary>
        /// Gets Issue Comments for a specified Issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-on-an-issue</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <param name="request">The sorting <see cref="IssueCommentRequest">parameters</see></param>
        public IObservable<IssueComment> GetAllForIssue(string owner, string name, long issueNumber, IssueCommentRequest request, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAllForIssue(owner, name, issueNumber, request, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets Issue Comments for a specified Issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-on-an-issue</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <param name="request">The sorting <see cref="IssueCommentRequest">parameters</see></param>
        public IObservable<IssueComment> GetAllForIssue(long repositoryId, long issueNumber, IssueCommentRequest request, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAllForIssue(repositoryId, issueNumber, request, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets Issue Comments for a specified Issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-on-an-issue</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <param name="request">The sorting <see cref="IssueCommentRequest">parameters</see></param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<IssueComment> GetAllForIssue(string owner, string name, long issueNumber, IssueCommentRequest request, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<IssueComment>(ApiUrls.IssueComments(owner, name, issueNumber), request.ToParametersDictionary(), options);

        }

        /// <summary>
        /// Gets Issue Comments for a specified Issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-on-an-issue</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <param name="request">The sorting <see cref="IssueCommentRequest">parameters</see></param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<IssueComment> GetAllForIssue(long repositoryId, long issueNumber, IssueCommentRequest request, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<IssueComment>(ApiUrls.IssueComments(repositoryId, issueNumber), request.ToParametersDictionary(), options);
        }

        /// <summary>
        /// Creates a new Issue Comment for a specified Issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#create-a-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <param name="newComment">The text of the new comment</param>
        public IObservable<IssueComment> Create(string owner, string name, long issueNumber, string newComment, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(newComment, nameof(newComment));

            return _client.Create(owner, name, issueNumber, newComment, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Creates a new Issue Comment for a specified Issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#create-a-comment</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <param name="newComment">The text of the new comment</param>
        public IObservable<IssueComment> Create(long repositoryId, long issueNumber, string newComment, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(newComment, nameof(newComment));

            return _client.Create(repositoryId, issueNumber, newComment, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Updates a specified Issue Comment.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#edit-a-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="commentId">The comment id</param>
        /// <param name="commentUpdate">The modified comment</param>
        public IObservable<IssueComment> Update(string owner, string name, long commentId, string commentUpdate, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(commentUpdate, nameof(commentUpdate));

            return _client.Update(owner, name, commentId, commentUpdate, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Updates a specified Issue Comment.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#edit-a-comment</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="commentId">The comment id</param>
        /// <param name="commentUpdate">The modified comment</param>
        public IObservable<IssueComment> Update(long repositoryId, long commentId, string commentUpdate, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(commentUpdate, nameof(commentUpdate));

            return _client.Update(repositoryId, commentId, commentUpdate, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Deletes the specified Issue Comment
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#delete-a-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="commentId">The comment id</param>
        public IObservable<Unit> Delete(string owner, string name, long commentId, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Delete(owner, name, commentId, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Deletes the specified Issue Comment
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#delete-a-comment</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="commentId">The comment id</param>
        public IObservable<Unit> Delete(long repositoryId, long commentId, CancellationToken cancellationToken = default)
        {
            return _client.Delete(repositoryId, commentId, cancellationToken).ToObservable();
        }
    }
}
