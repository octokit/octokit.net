using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Repository Comments API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/repos/comments/">Repository Comments API documentation</a> for more information.
    /// </remarks>
    public class RepositoryCommentsClient : ApiClient, IRepositoryCommentsClient
    {
        /// <summary>
        /// Instantiates a new GitHub Repository Comments API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public RepositoryCommentsClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        /// <summary>
        /// Gets a single Repository Comment by number.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="commentId">The comment id</param>
        /// <remarks>http://developer.github.com/v3/repos/comments/#get-a-single-commit-comment</remarks>
        [ManualRoute("GET", "/repos/{owner}/{repo}/comments/{comment_id}")]
        public Task<CommitComment> Get(string owner, string name, long commentId, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Get<CommitComment>(ApiUrls.CommitComment(owner, name, commentId), null);
        }

        /// <summary>
        /// Gets a single Repository Comment by number.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="commentId">The comment id</param>
        /// <remarks>http://developer.github.com/v3/repos/comments/#get-a-single-commit-comment</remarks>
        [ManualRoute("GET", "/repositories/{id}/comments/{number}")]
        public Task<CommitComment> Get(long repositoryId, long commentId, CancellationToken cancellationToken = default)
        {
            return ApiConnection.Get<CommitComment>(ApiUrls.CommitComment(repositoryId, commentId), null);
        }

        /// <summary>
        /// Gets Commit Comments for a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <remarks>http://developer.github.com/v3/repos/comments/#list-commit-comments-for-a-repository</remarks>
        [ManualRoute("GET", "/repos/{owner}/{repo}/comments")]
        public Task<IReadOnlyList<CommitComment>> GetAllForRepository(string owner, string name, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllForRepository(owner, name, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets Commit Comments for a repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <remarks>http://developer.github.com/v3/repos/comments/#list-commit-comments-for-a-repository</remarks>
        [ManualRoute("GET", "/repositories/{id}/comments")]
        public Task<IReadOnlyList<CommitComment>> GetAllForRepository(long repositoryId, CancellationToken cancellationToken = default)
        {
            return GetAllForRepository(repositoryId, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets Commit Comments for a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options to change the API response</param>
        /// <remarks>http://developer.github.com/v3/repos/comments/#list-commit-comments-for-a-repository</remarks>
        [ManualRoute("GET", "/repos/{owner}/{repo}/comments")]
        public Task<IReadOnlyList<CommitComment>> GetAllForRepository(string owner, string name, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<CommitComment>(ApiUrls.CommitComments(owner, name), null, options);
        }

        /// <summary>
        /// Gets Commit Comments for a repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options to change the API response</param>
        /// <remarks>http://developer.github.com/v3/repos/comments/#list-commit-comments-for-a-repository</remarks>
        [ManualRoute("GET", "/repositories/{id}/comments")]
        public Task<IReadOnlyList<CommitComment>> GetAllForRepository(long repositoryId, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<CommitComment>(ApiUrls.CommitComments(repositoryId), null, options);
        }

        /// <summary>
        /// Gets Commit Comments for a specified Commit.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="sha">The sha of the commit</param>
        /// <remarks>http://developer.github.com/v3/repos/comments/#list-comments-for-a-single-commit</remarks>
        [ManualRoute("GET", "/repos/{owner}/{repo}/commits/{commit_sha}/comments")]
        public Task<IReadOnlyList<CommitComment>> GetAllForCommit(string owner, string name, string sha, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(sha, nameof(sha));

            return GetAllForCommit(owner, name, sha, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets Commit Comments for a specified Commit.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="sha">The sha of the commit</param>
        /// <remarks>http://developer.github.com/v3/repos/comments/#list-comments-for-a-single-commit</remarks>
        [ManualRoute("GET", "/repositories/{id}/commits/{commit_sha}/comments")]
        public Task<IReadOnlyList<CommitComment>> GetAllForCommit(long repositoryId, string sha, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(sha, nameof(sha));

            return GetAllForCommit(repositoryId, sha, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets Commit Comments for a specified Commit.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="sha">The sha of the commit</param>
        /// <param name="options">Options to change the API response</param>
        /// <remarks>http://developer.github.com/v3/repos/comments/#list-comments-for-a-single-commit</remarks>
        [ManualRoute("GET", "/repos/{owner}/{repo}/commits/{commit_sha}/comments")]
        public Task<IReadOnlyList<CommitComment>> GetAllForCommit(string owner, string name, string sha, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(sha, nameof(sha));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<CommitComment>(ApiUrls.CommitComments(owner, name, sha), null, options);
        }

        /// <summary>
        /// Gets Commit Comments for a specified Commit.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="sha">The sha of the commit</param>
        /// <param name="options">Options to change the API response</param>
        /// <remarks>http://developer.github.com/v3/repos/comments/#list-comments-for-a-single-commit</remarks>
        [ManualRoute("GET", "/repositories/{id}/commits/{commit_sha}/comments")]
        public Task<IReadOnlyList<CommitComment>> GetAllForCommit(long repositoryId, string sha, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(sha, nameof(sha));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<CommitComment>(ApiUrls.CommitComments(repositoryId, sha), null, options);
        }

        /// <summary>
        /// Creates a new Commit Comment for a specified Commit.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="sha">The sha reference of commit</param>
        /// <param name="newCommitComment">The new comment to add to the commit</param>
        /// <remarks>http://developer.github.com/v3/repos/comments/#create-a-commit-comment</remarks>
        [ManualRoute("POST", "/repos/{owner}/{repo}/commits/{commit_sha}/comments")]
        public Task<CommitComment> Create(string owner, string name, string sha, NewCommitComment newCommitComment, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(sha, nameof(sha));
            Ensure.ArgumentNotNull(newCommitComment, nameof(newCommitComment));

            return ApiConnection.Post<CommitComment>(ApiUrls.CommitComments(owner, name, sha), newCommitComment);
        }

        /// <summary>
        /// Creates a new Commit Comment for a specified Commit.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="sha">The sha reference of commit</param>
        /// <param name="newCommitComment">The new comment to add to the commit</param>
        /// <remarks>http://developer.github.com/v3/repos/comments/#create-a-commit-comment</remarks>
        [ManualRoute("POST", "/repositories/{id}/commits/{commit_sha}/comments")]
        public Task<CommitComment> Create(long repositoryId, string sha, NewCommitComment newCommitComment, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(sha, nameof(sha));
            Ensure.ArgumentNotNull(newCommitComment, nameof(newCommitComment));

            return ApiConnection.Post<CommitComment>(ApiUrls.CommitComments(repositoryId, sha), newCommitComment);
        }

        /// <summary>
        /// Updates a specified Commit Comment.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="commentId">The comment id</param>
        /// <param name="commentUpdate">The modified comment</param>
        /// <remarks>http://developer.github.com/v3/repos/comments/#update-a-commit-comment</remarks>
        [ManualRoute("PATCH", "/repos/{owner}/{repo}/comments/{comment_id}")]
        public Task<CommitComment> Update(string owner, string name, long  commentId, string commentUpdate, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(commentUpdate, nameof(commentUpdate));

            return ApiConnection.Patch<CommitComment>(ApiUrls.CommitComment(owner, name, commentId), new BodyWrapper(commentUpdate));
        }

        /// <summary>
        /// Updates a specified Commit Comment.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="commentId">The comment id</param>
        /// <param name="commentUpdate">The modified comment</param>
        /// <remarks>http://developer.github.com/v3/repos/comments/#update-a-commit-comment</remarks>
        [ManualRoute("PATCH", "/repositories/{id}/comments/{number}")]
        public Task<CommitComment> Update(long repositoryId, long commentId, string commentUpdate, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(commentUpdate, nameof(commentUpdate));

            return ApiConnection.Patch<CommitComment>(ApiUrls.CommitComment(repositoryId, commentId), new BodyWrapper(commentUpdate));
        }

        /// <summary>
        /// Deletes the specified Commit Comment
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="commentId">The comment id</param>
        /// <remarks>http://developer.github.com/v3/repos/comments/#delete-a-commit-comment</remarks>
        [ManualRoute("DELETE", "/repos/{owner}/{repo}/comments/{comment_id}")]
        public Task Delete(string owner, string name, long commentId, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Delete(ApiUrls.CommitComment(owner, name, commentId));
        }

        /// <summary>
        /// Deletes the specified Commit Comment
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="commentId">The comment id</param>
        /// <remarks>http://developer.github.com/v3/repos/comments/#delete-a-commit-comment</remarks>
        [ManualRoute("DELETE", "/repositories/{id}/comments/{number}")]
        public Task Delete(long repositoryId, long commentId, CancellationToken cancellationToken = default)
        {
            return ApiConnection.Delete(ApiUrls.CommitComment(repositoryId, commentId));
        }
    }
}
