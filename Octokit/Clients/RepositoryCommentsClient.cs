
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
        /// <remarks>http://developer.github.com/v3/repos/comments/#get-a-single-commit-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The comment id</param>
        /// <returns></returns>
        public Task<CommitComment> Get(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.Get<CommitComment>(ApiUrls.CommitComment(owner, name, number));
        }

        /// <summary>
        /// Gets Commit Comments for a repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/repos/comments/#list-commit-comments-for-a-repository</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        public Task<IReadOnlyList<CommitComment>> GetForRepository(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.GetAll<CommitComment>(ApiUrls.CommitComments(owner, name));
        }

        /// <summary>
        /// Gets Commit Comments for a specified Commit.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/repos/comments/#list-comments-for-a-single-commit</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="sha">The sha of the commit</param>
        /// <returns></returns>
        public Task<IReadOnlyList<CommitComment>> GetForCommit(string owner, string name, string sha)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(sha, "sha");

            return ApiConnection.GetAll<CommitComment>(ApiUrls.CommitComments(owner, name, sha));
        }

        /// <summary>
        /// Creates a new Commit Comment for a specified Commit.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/repos/comments/#create-a-commit-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="sha">The sha reference of commit</param>
        /// <param name="newCommitComment">The new comment to add to the commit</param>
        /// <returns></returns>
        public Task<CommitComment> Create(string owner, string name, string sha, NewCommitComment newCommitComment)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(sha, "sha");
            Ensure.ArgumentNotNull(newCommitComment, "newCommitComment");

            return ApiConnection.Post<CommitComment>(ApiUrls.CommitComments(owner, name, sha), newCommitComment);
        }

        /// <summary>
        /// Updates a specified Commit Comment.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/repos/comments/#update-a-commit-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The comment number</param>
        /// <param name="commentUpdate">The modified comment</param>
        /// <returns></returns>
        public Task<CommitComment> Update(string owner, string name, int number, string commentUpdate)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(commentUpdate, "commentUpdate");

            return ApiConnection.Patch<CommitComment>(ApiUrls.CommitComment(owner, name, number), new BodyWrapper(commentUpdate));
        }

        /// <summary>
        /// Deletes the specified Commit Comment
        /// </summary>
        /// <remarks>http://developer.github.com/v3/repos/comments/#delete-a-commit-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The comment id</param>
        /// <returns></returns>
        public Task Delete(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.Delete(ApiUrls.CommitComment(owner, name, number));
        }
    }
}
