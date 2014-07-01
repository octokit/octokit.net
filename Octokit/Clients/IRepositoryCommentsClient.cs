using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Repository Comments API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/repos/comments/">Repository Comments API documentation</a> for more information.
    /// </remarks>
    public interface IRepositoryCommentsClient
    {
        /// <summary>
        /// Gets a single Repository Comment by number.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/repos/comments/#get-a-single-commit-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The comment id</param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
            Justification = "Method makes a network request")]
        Task<CommitComment> Get(string owner, string name, int number);

        /// <summary>
        /// Gets Commit Comments for a repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/repos/comments/#list-commit-comments-for-a-repository</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        Task<IReadOnlyList<CommitComment>> GetForRepository(string owner, string name);

        /// <summary>
        /// Gets Commit Comments for a specified Commit.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/repos/comments/#list-comments-for-a-single-commit</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="sha">The sha of the commit</param>
        /// <returns></returns>
        Task<IReadOnlyList<CommitComment>> GetForCommit(string owner, string name, string sha);

        /// <summary>
        /// Creates a new Commit Comment for a specified Commit.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/repos/comments/#create-a-commit-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="sha">The sha reference of commit</param>
        /// <param name="newCommitComment">The new comment to add to the commit</param>
        /// <returns></returns>
        Task<CommitComment> Create(string owner, string name, string sha, NewCommitComment newCommitComment);

        /// <summary>
        /// Updates a specified Commit Comment.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/repos/comments/#update-a-commit-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The comment number</param>
        /// <param name="commentUpdate">The modified comment</param>
        /// <returns></returns>
        Task<CommitComment> Update(string owner, string name, int number, string commentUpdate);

        /// <summary>
        /// Deletes the specified Commit Comment
        /// </summary>
        /// <remarks>http://developer.github.com/v3/repos/comments/#delete-a-commit-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The comment id</param>
        /// <returns></returns>
        Task Delete(string owner, string name, int number);
    }
}
