using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Pull Request Review Comments API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/pulls/comments/">Review Comments API documentation</a> for more information.
    /// </remarks>
    public class ObservablePullRequestReviewCommentsClient : IObservablePullRequestReviewCommentsClient
    {
        readonly IPullRequestReviewCommentsClient _client;
        readonly IConnection _connection;

        public ObservablePullRequestReviewCommentsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.PullRequest.ReviewComment;
            _connection = client.Connection;
        }

        /// <summary>
        /// Gets review comments for a specified pull request.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-on-a-pull-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        public IObservable<PullRequestReviewComment> GetAll(string owner, string name, int pullRequestNumber)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAll(owner, name, pullRequestNumber, ApiOptions.None);
        }

        /// <summary>
        /// Gets review comments for a specified pull request.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-on-a-pull-request</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        public IObservable<PullRequestReviewComment> GetAll(long repositoryId, int pullRequestNumber)
        {
            return GetAll(repositoryId, pullRequestNumber, ApiOptions.None);
        }

        /// <summary>
        /// Gets review comments for a specified pull request.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-on-a-pull-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<PullRequestReviewComment> GetAll(string owner, string name, int pullRequestNumber, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<PullRequestReviewComment>(ApiUrls.PullRequestReviewComments(owner, name, pullRequestNumber), null, options);
        }

        /// <summary>
        /// Gets review comments for a specified pull request.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-on-a-pull-request</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<PullRequestReviewComment> GetAll(long repositoryId, int pullRequestNumber, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<PullRequestReviewComment>(ApiUrls.PullRequestReviewComments(repositoryId, pullRequestNumber), options);
        }

        /// <summary>
        /// Gets a list of the pull request review comments in a specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-in-a-repository</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        public IObservable<PullRequestReviewComment> GetAllForRepository(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllForRepository(owner, name, new PullRequestReviewCommentRequest(), ApiOptions.None);
        }

        /// <summary>
        /// Gets a list of the pull request review comments in a specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-in-a-repository</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        public IObservable<PullRequestReviewComment> GetAllForRepository(long repositoryId)
        {
            return GetAllForRepository(repositoryId, new PullRequestReviewCommentRequest(), ApiOptions.None);
        }

        /// <summary>
        /// Gets a list of the pull request review comments in a specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-in-a-repository</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<PullRequestReviewComment> GetAllForRepository(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return GetAllForRepository(owner, name, new PullRequestReviewCommentRequest(), options);
        }

        /// <summary>
        /// Gets a list of the pull request review comments in a specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-in-a-repository</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<PullRequestReviewComment> GetAllForRepository(long repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return GetAllForRepository(repositoryId, new PullRequestReviewCommentRequest(), options);
        }

        /// <summary>
        /// Gets a list of the pull request review comments in a specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-in-a-repository</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">The sorting <see cref="PullRequestReviewCommentRequest">parameters</see></param>
        public IObservable<PullRequestReviewComment> GetAllForRepository(string owner, string name, PullRequestReviewCommentRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAllForRepository(owner, name, request, ApiOptions.None);
        }

        /// <summary>
        /// Gets a list of the pull request review comments in a specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-in-a-repository</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">The sorting <see cref="PullRequestReviewCommentRequest">parameters</see></param>
        public IObservable<PullRequestReviewComment> GetAllForRepository(long repositoryId, PullRequestReviewCommentRequest request)
        {
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAllForRepository(repositoryId, request, ApiOptions.None);
        }

        /// <summary>
        /// Gets a list of the pull request review comments in a specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-in-a-repository</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">The sorting <see cref="PullRequestReviewCommentRequest">parameters</see></param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<PullRequestReviewComment> GetAllForRepository(string owner, string name, PullRequestReviewCommentRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<PullRequestReviewComment>(ApiUrls.PullRequestReviewCommentsRepository(owner, name), request.ToParametersDictionary(), options);
        }

        /// <summary>
        /// Gets a list of the pull request review comments in a specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#list-comments-in-a-repository</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">The sorting <see cref="PullRequestReviewCommentRequest">parameters</see></param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<PullRequestReviewComment> GetAllForRepository(long repositoryId, PullRequestReviewCommentRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<PullRequestReviewComment>(ApiUrls.PullRequestReviewCommentsRepository(repositoryId), request.ToParametersDictionary(), options);
        }

        /// <summary>
        /// Gets a single pull request review comment by number.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#get-a-single-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="commentId">The pull request review comment id</param>
        public IObservable<PullRequestReviewComment> GetComment(string owner, string name, long commentId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.GetComment(owner, name, commentId).ToObservable();
        }

        /// <summary>
        /// Gets a single pull request review comment by number.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#get-a-single-comment</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="commentId">The pull request review comment id</param>
        public IObservable<PullRequestReviewComment> GetComment(long repositoryId, long commentId)
        {
            return _client.GetComment(repositoryId, commentId).ToObservable();
        }

        /// <summary>
        /// Creates a comment on a pull request review.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#create-a-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="comment">The comment</param>
        public IObservable<PullRequestReviewComment> Create(string owner, string name, int pullRequestNumber, PullRequestReviewCommentCreate comment)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(comment, nameof(comment));

            return _client.Create(owner, name, pullRequestNumber, comment).ToObservable();
        }

        /// <summary>
        /// Creates a comment on a pull request review.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#create-a-comment</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="comment">The comment</param>
        public IObservable<PullRequestReviewComment> Create(long repositoryId, int pullRequestNumber, PullRequestReviewCommentCreate comment)
        {
            Ensure.ArgumentNotNull(comment, nameof(comment));

            return _client.Create(repositoryId, pullRequestNumber, comment).ToObservable();
        }

        /// <summary>
        /// Creates a comment on a pull request review as a reply to another comment.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#create-a-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="comment">The comment</param>
        public IObservable<PullRequestReviewComment> CreateReply(string owner, string name, int pullRequestNumber, PullRequestReviewCommentReplyCreate comment)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(comment, nameof(comment));

            return _client.CreateReply(owner, name, pullRequestNumber, comment).ToObservable();
        }

        /// <summary>
        /// Creates a comment on a pull request review as a reply to another comment.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#create-a-comment</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="comment">The comment</param>
        public IObservable<PullRequestReviewComment> CreateReply(long repositoryId, int pullRequestNumber, PullRequestReviewCommentReplyCreate comment)
        {
            Ensure.ArgumentNotNull(comment, nameof(comment));

            return _client.CreateReply(repositoryId, pullRequestNumber, comment).ToObservable();
        }

        /// <summary>
        /// Edits a comment on a pull request review.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#edit-a-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="commentId">The pull request review comment id</param>
        /// <param name="comment">The edited comment</param>
        public IObservable<PullRequestReviewComment> Edit(string owner, string name, long commentId, PullRequestReviewCommentEdit comment)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(comment, nameof(comment));

            return _client.Edit(owner, name, commentId, comment).ToObservable();
        }

        /// <summary>
        /// Edits a comment on a pull request review.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#edit-a-comment</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="commentId">The pull request review comment id</param>
        /// <param name="comment">The edited comment</param>
        public IObservable<PullRequestReviewComment> Edit(long repositoryId, long commentId, PullRequestReviewCommentEdit comment)
        {
            Ensure.ArgumentNotNull(comment, nameof(comment));

            return _client.Edit(repositoryId, commentId, comment).ToObservable();
        }

        /// <summary>
        /// Deletes a comment on a pull request review.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#delete-a-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="commentId">The pull request review comment id</param>
        public IObservable<Unit> Delete(string owner, string name, long commentId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Delete(owner, name, commentId).ToObservable();
        }

        /// <summary>
        /// Deletes a comment on a pull request review.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/comments/#delete-a-comment</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="commentId">The pull request review comment id</param>
        public IObservable<Unit> Delete(long repositoryId, long commentId)
        {
            return _client.Delete(repositoryId, commentId).ToObservable();
        }
    }
}
