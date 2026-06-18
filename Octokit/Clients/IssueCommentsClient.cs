using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Issue Comments API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/issues/comments/">Issue Comments API documentation</a> for more information.
    /// </remarks>
    public class IssueCommentsClient : ApiClient, IIssueCommentsClient
    {
        /// <summary>
        /// Instantiates a new GitHub Issue Comments API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public IssueCommentsClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        /// <summary>
        /// Gets a single Issue Comment by id.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#get-a-single-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="commentId">The issue comment id</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/issues/comments/{comment_id}")]
        public Task<IssueComment> Get(string owner, string name, long commentId, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Get<IssueComment>(ApiUrls.IssueComment(owner, name, commentId), null, cancellationToken);
        }

        /// <summary>
        /// Gets a single Issue Comment by id.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#get-a-single-comment</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="commentId">The issue comment id</param>
        [ManualRoute("GET", "/repositories/{id}/issues/comments/{comment_id}")]
        public Task<IssueComment> Get(long repositoryId, long commentId, CancellationToken cancellationToken = default)
        {
            return ApiConnection.Get<IssueComment>(ApiUrls.IssueComment(repositoryId, commentId), null, cancellationToken);
        }

        /// <summary>
        /// Gets Issue Comments for a repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-in-a-repository</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/issues/comments")]
        public Task<IReadOnlyList<IssueComment>> GetAllForRepository(string owner, string name, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllForRepository(owner, name, new IssueCommentRequest(), ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets Issue Comments for a repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-in-a-repository</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        [ManualRoute("GET", "/repositories/{id}/issues/comments")]
        public Task<IReadOnlyList<IssueComment>> GetAllForRepository(long repositoryId, CancellationToken cancellationToken = default)
        {
            return GetAllForRepository(repositoryId, new IssueCommentRequest(), ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets Issue Comments for a repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-in-a-repository</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/issues/comments")]
        public Task<IReadOnlyList<IssueComment>> GetAllForRepository(string owner, string name, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return GetAllForRepository(owner, name, new IssueCommentRequest(), options, cancellationToken);
        }

        /// <summary>
        /// Gets Issue Comments for a repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-in-a-repository</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/repositories/{id}/issues/comments")]
        public Task<IReadOnlyList<IssueComment>> GetAllForRepository(long repositoryId, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return GetAllForRepository(repositoryId, new IssueCommentRequest(), options, cancellationToken);
        }

        /// <summary>
        /// Gets Issue Comments for a repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-in-a-repository</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">The sorting <see cref="IssueCommentRequest">parameters</see></param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/issues/comments")]
        public Task<IReadOnlyList<IssueComment>> GetAllForRepository(string owner, string name, IssueCommentRequest request, CancellationToken cancellationToken = default)
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
        [ManualRoute("GET", "/repositories/{id}/issues/comments")]
        public Task<IReadOnlyList<IssueComment>> GetAllForRepository(long repositoryId, IssueCommentRequest request, CancellationToken cancellationToken = default)
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
        [ManualRoute("GET", "/repos/{owner}/{repo}/issues/comments")]
        public Task<IReadOnlyList<IssueComment>> GetAllForRepository(string owner, string name, IssueCommentRequest request, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<IssueComment>(ApiUrls.IssueComments(owner, name), request.ToParametersDictionary(), options, cancellationToken);
        }

        /// <summary>
        /// Gets Issue Comments for a repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-in-a-repository</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">The sorting <see cref="IssueCommentRequest">parameters</see></param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/repositories/{id}/issues/comments")]
        public Task<IReadOnlyList<IssueComment>> GetAllForRepository(long repositoryId, IssueCommentRequest request, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<IssueComment>(ApiUrls.IssueComments(repositoryId), request.ToParametersDictionary(), options, cancellationToken);
        }

        /// <summary>
        /// Gets Issue Comments for a specified Issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-on-an-issue</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/issues/{number]/comments")]
        public Task<IReadOnlyList<IssueComment>> GetAllForIssue(string owner, string name, long issueNumber, CancellationToken cancellationToken = default)
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
        [ManualRoute("GET", "/repositories/{id}/issues/{number}/comments")]
        public Task<IReadOnlyList<IssueComment>> GetAllForIssue(long repositoryId, long issueNumber, CancellationToken cancellationToken = default)
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
        [ManualRoute("GET", "/repos/{owner}/{repo}/issues/{number]/comments")]
        public Task<IReadOnlyList<IssueComment>> GetAllForIssue(string owner, string name, long issueNumber, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return GetAllForIssue(owner, name, issueNumber, new IssueCommentRequest(), options, cancellationToken);
        }

        /// <summary>
        /// Gets Issue Comments for a specified Issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-on-an-issue</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/repositories/{id}/issues/{number}/comments")]
        public Task<IReadOnlyList<IssueComment>> GetAllForIssue(long repositoryId, long issueNumber, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return GetAllForIssue(repositoryId, issueNumber, new IssueCommentRequest(), options, cancellationToken);
        }

        /// <summary>
        /// Gets Issue Comments for a specified Issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-on-an-issue</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <param name="request">The sorting <see cref="IssueCommentRequest">parameters</see></param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/issues/{number]/comments")]
        public Task<IReadOnlyList<IssueComment>> GetAllForIssue(string owner, string name, long issueNumber, IssueCommentRequest request, CancellationToken cancellationToken = default)
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
        [ManualRoute("GET", "/repositories/{id}/issues/{number}/comments")]
        public Task<IReadOnlyList<IssueComment>> GetAllForIssue(long repositoryId, long issueNumber, IssueCommentRequest request, CancellationToken cancellationToken = default)
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
        [ManualRoute("GET", "/repos/{owner}/{repo}/issues/{number]/comments")]
        public Task<IReadOnlyList<IssueComment>> GetAllForIssue(string owner, string name, long issueNumber, IssueCommentRequest request, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<IssueComment>(ApiUrls.IssueComments(owner, name, issueNumber), request.ToParametersDictionary(), options, cancellationToken);
        }

        /// <summary>
        /// Gets Issue Comments for a specified Issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#list-comments-on-an-issue</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <param name="request">The sorting <see cref="IssueCommentRequest">parameters</see></param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/repositories/{id}/issues/{number}/comments")]
        public Task<IReadOnlyList<IssueComment>> GetAllForIssue(long repositoryId, long issueNumber, IssueCommentRequest request, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<IssueComment>(ApiUrls.IssueComments(repositoryId, issueNumber), request.ToParametersDictionary(), options, cancellationToken);
        }

        /// <summary>
        /// Creates a new Issue Comment for a specified Issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#create-a-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <param name="newComment">The new comment to add to the issue</param>
        [ManualRoute("POST", "/repos/{owner}/{repo}/issues/{number]/comments")]
        public Task<IssueComment> Create(string owner, string name, long issueNumber, string newComment, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(newComment, nameof(newComment));

            return ApiConnection.Post<IssueComment>(ApiUrls.IssueComments(owner, name, issueNumber), new BodyWrapper(newComment), cancellationToken);
        }

        /// <summary>
        /// Creates a new Issue Comment for a specified Issue.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#create-a-comment</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <param name="newComment">The new comment to add to the issue</param>
        [ManualRoute("POST", "/repositories/{id}/issues/{number}/comments")]
        public Task<IssueComment> Create(long repositoryId, long issueNumber, string newComment, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(newComment, nameof(newComment));

            return ApiConnection.Post<IssueComment>(ApiUrls.IssueComments(repositoryId, issueNumber), new BodyWrapper(newComment), cancellationToken);
        }

        /// <summary>
        /// Updates a specified Issue Comment.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#edit-a-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="id">The comment id</param>
        /// <param name="commentUpdate">The modified comment</param>
        [ManualRoute("PATCH", "/repos/{owner}/{repo}/issues/comments/{id}")]
        public Task<IssueComment> Update(string owner, string name, long id, string commentUpdate, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(commentUpdate, nameof(commentUpdate));

            return ApiConnection.Patch<IssueComment>(ApiUrls.IssueComment(owner, name, id), new BodyWrapper(commentUpdate), cancellationToken);
        }

        /// <summary>
        /// Updates a specified Issue Comment.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#edit-a-comment</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="id">The comment id</param>
        /// <param name="commentUpdate">The modified comment</param>
        [ManualRoute("PATCH", "/repositories/{id}/issues/comments/{number}")]
        public Task<IssueComment> Update(long repositoryId, long id, string commentUpdate, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(commentUpdate, nameof(commentUpdate));

            return ApiConnection.Patch<IssueComment>(ApiUrls.IssueComment(repositoryId, id), new BodyWrapper(commentUpdate), cancellationToken);
        }

        /// <summary>
        /// Deletes the specified Issue Comment
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#delete-a-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="id">The comment id</param>
        [ManualRoute("DELETE", "/repos/{owner}/{repo}/issues/comments/{id}")]
        public Task Delete(string owner, string name, long id, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Delete(ApiUrls.IssueComment(owner, name, id), cancellationToken);
        }

        /// <summary>
        /// Deletes the specified Issue Comment
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/comments/#delete-a-comment</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="id">The comment id</param>
        [ManualRoute("DELETE", "/repositories/{id}/issues/comments/{number}")]
        public Task Delete(long repositoryId, long id, CancellationToken cancellationToken = default)
        {
            return ApiConnection.Delete(ApiUrls.IssueComment(repositoryId, id), cancellationToken);
        }
    }
}
