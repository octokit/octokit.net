using System.Collections.Generic;
using System.Threading.Tasks;

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
        /// <param name="number">The comment id</param>
        /// <remarks>http://developer.github.com/v3/repos/comments/#get-a-single-commit-comment</remarks>
        public Task<CommitComment> Get(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.Get<CommitComment>(ApiUrls.CommitComment(owner, name, number), null, AcceptHeaders.ReactionsPreview);
        }

        /// <summary>
        /// Gets a single Repository Comment by number.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The comment id</param>
        /// <remarks>http://developer.github.com/v3/repos/comments/#get-a-single-commit-comment</remarks>
        public Task<CommitComment> Get(long repositoryId, int number)
        {
            return ApiConnection.Get<CommitComment>(ApiUrls.CommitComment(repositoryId, number), null, AcceptHeaders.ReactionsPreview);
        }

        /// <summary>
        /// Gets Commit Comments for a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <remarks>http://developer.github.com/v3/repos/comments/#list-commit-comments-for-a-repository</remarks>
        public Task<IReadOnlyList<CommitComment>> GetAllForRepository(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return GetAllForRepository(owner, name, ApiOptions.None);
        }

        /// <summary>
        /// Gets Commit Comments for a repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <remarks>http://developer.github.com/v3/repos/comments/#list-commit-comments-for-a-repository</remarks>
        public Task<IReadOnlyList<CommitComment>> GetAllForRepository(long repositoryId)
        {
            return GetAllForRepository(repositoryId, ApiOptions.None);
        }

        /// <summary>
        /// Gets Commit Comments for a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options to change the API response</param>
        /// <remarks>http://developer.github.com/v3/repos/comments/#list-commit-comments-for-a-repository</remarks>
        public Task<IReadOnlyList<CommitComment>> GetAllForRepository(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<CommitComment>(ApiUrls.CommitComments(owner, name), null, AcceptHeaders.ReactionsPreview, options);
        }

        /// <summary>
        /// Gets Commit Comments for a repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options to change the API response</param>
        /// <remarks>http://developer.github.com/v3/repos/comments/#list-commit-comments-for-a-repository</remarks>
        public Task<IReadOnlyList<CommitComment>> GetAllForRepository(long repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<CommitComment>(ApiUrls.CommitComments(repositoryId), null, AcceptHeaders.ReactionsPreview, options);
        }

        /// <summary>
        /// Gets Commit Comments for a specified Commit.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="sha">The sha of the commit</param>
        /// <remarks>http://developer.github.com/v3/repos/comments/#list-comments-for-a-single-commit</remarks>
        public Task<IReadOnlyList<CommitComment>> GetAllForCommit(string owner, string name, string sha)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(sha, "sha");

            return GetAllForCommit(owner, name, sha, ApiOptions.None);
        }

        /// <summary>
        /// Gets Commit Comments for a specified Commit.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="sha">The sha of the commit</param>
        /// <remarks>http://developer.github.com/v3/repos/comments/#list-comments-for-a-single-commit</remarks>
        public Task<IReadOnlyList<CommitComment>> GetAllForCommit(long repositoryId, string sha)
        {
            Ensure.ArgumentNotNullOrEmptyString(sha, "sha");

            return GetAllForCommit(repositoryId, sha, ApiOptions.None);
        }

        /// <summary>
        /// Gets Commit Comments for a specified Commit.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="sha">The sha of the commit</param>
        /// <param name="options">Options to change the API response</param>
        /// <remarks>http://developer.github.com/v3/repos/comments/#list-comments-for-a-single-commit</remarks>
        public Task<IReadOnlyList<CommitComment>> GetAllForCommit(string owner, string name, string sha, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(sha, "sha");
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<CommitComment>(ApiUrls.CommitComments(owner, name, sha), null, AcceptHeaders.ReactionsPreview, options);
        }

        /// <summary>
        /// Gets Commit Comments for a specified Commit.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="sha">The sha of the commit</param>
        /// <param name="options">Options to change the API response</param>
        /// <remarks>http://developer.github.com/v3/repos/comments/#list-comments-for-a-single-commit</remarks>
        public Task<IReadOnlyList<CommitComment>> GetAllForCommit(long repositoryId, string sha, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(sha, "sha");
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<CommitComment>(ApiUrls.CommitComments(repositoryId, sha), null, AcceptHeaders.ReactionsPreview, options);
        }

        /// <summary>
        /// Creates a new Commit Comment for a specified Commit.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="sha">The sha reference of commit</param>
        /// <param name="newCommitComment">The new comment to add to the commit</param>
        /// <remarks>http://developer.github.com/v3/repos/comments/#create-a-commit-comment</remarks>
        public Task<CommitComment> Create(string owner, string name, string sha, NewCommitComment newCommitComment)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(sha, "sha");
            Ensure.ArgumentNotNull(newCommitComment, "newCommitComment");

            return ApiConnection.Post<CommitComment>(ApiUrls.CommitComments(owner, name, sha), newCommitComment);
        }

        /// <summary>
        /// Creates a new Commit Comment for a specified Commit.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="sha">The sha reference of commit</param>
        /// <param name="newCommitComment">The new comment to add to the commit</param>
        /// <remarks>http://developer.github.com/v3/repos/comments/#create-a-commit-comment</remarks>
        public Task<CommitComment> Create(long repositoryId, string sha, NewCommitComment newCommitComment)
        {
            Ensure.ArgumentNotNullOrEmptyString(sha, "sha");
            Ensure.ArgumentNotNull(newCommitComment, "newCommitComment");

            return ApiConnection.Post<CommitComment>(ApiUrls.CommitComments(repositoryId, sha), newCommitComment);
        }

        /// <summary>
        /// Updates a specified Commit Comment.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The comment number</param>
        /// <param name="commentUpdate">The modified comment</param>
        /// <remarks>http://developer.github.com/v3/repos/comments/#update-a-commit-comment</remarks>
        public Task<CommitComment> Update(string owner, string name, int number, string commentUpdate)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(commentUpdate, "commentUpdate");

            return ApiConnection.Patch<CommitComment>(ApiUrls.CommitComment(owner, name, number), new BodyWrapper(commentUpdate));
        }

        /// <summary>
        /// Updates a specified Commit Comment.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The comment number</param>
        /// <param name="commentUpdate">The modified comment</param>
        /// <remarks>http://developer.github.com/v3/repos/comments/#update-a-commit-comment</remarks>
        public Task<CommitComment> Update(long repositoryId, int number, string commentUpdate)
        {
            Ensure.ArgumentNotNull(commentUpdate, "commentUpdate");

            return ApiConnection.Patch<CommitComment>(ApiUrls.CommitComment(repositoryId, number), new BodyWrapper(commentUpdate));
        }

        /// <summary>
        /// Deletes the specified Commit Comment
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The comment id</param>
        /// <remarks>http://developer.github.com/v3/repos/comments/#delete-a-commit-comment</remarks>
        public Task Delete(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.Delete(ApiUrls.CommitComment(owner, name, number));
        }

        /// <summary>
        /// Deletes the specified Commit Comment
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The comment id</param>
        /// <remarks>http://developer.github.com/v3/repos/comments/#delete-a-commit-comment</remarks>
        public Task Delete(long repositoryId, int number)
        {
            return ApiConnection.Delete(ApiUrls.CommitComment(repositoryId, number));
        }
    }
}
