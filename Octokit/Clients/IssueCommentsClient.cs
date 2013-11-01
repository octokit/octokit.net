
using System.Threading.Tasks;

namespace Octokit
{
    public class IssueCommentsClient : ApiClient, IIssueCommentsClient
    {
        public IssueCommentsClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        /// <summary>
        /// Gets a single Issue Comment by number.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/comments/#get-a-single-comment
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue comment number</param>
        /// <returns>The <see cref="IssueComment"/>s for the specified Issue Comment.</returns>
        public Task<IssueComment> Get(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.Get<IssueComment>(ApiUrls.IssueComment(owner, name, number));
        }

        /// <summary>
        /// Gets a list of the Issue Comments in a specified repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/comments/#list-comments-in-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>The list of <see cref="IssueComment"/>s for the specified Repository.</returns>
        public Task<IReadOnlyList<IssueComment>> GetForRepository(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.GetAll<IssueComment>(ApiUrls.IssueComments(owner, name));
        }

        /// <summary>
        /// Gets a list of the Issue Comments for a specified issue.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/comments/#list-comments-on-an-issue
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue number</param>
        /// <returns>The list of <see cref="IssueComment"/>s for the specified Issue.</returns>
        public Task<IReadOnlyList<IssueComment>> GetForIssue(string owner, string name, int number)
        {

            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.GetAll<IssueComment>(ApiUrls.IssueComments(owner, name, number));
        }

        /// <summary>
        /// Creates a new Issue Comment in the specified Issue
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/comments/#create-a-comment
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue number</param>
        /// <param name="newComment">The text of the new comment</param>
        /// <returns>The <see cref="IssueComment"/>s for that was just created.</returns>
        public Task<IssueComment> Create(string owner, string name, int number, string newComment)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(newComment, "newComment");

            return ApiConnection.Post<IssueComment>(ApiUrls.IssueComment(owner, name, number), newComment);
        }

        /// <summary>
        /// Updates a specified Issue Comment
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/comments/#edit-a-comment
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue number</param>
        /// <param name="commentUpdate">The text of the updated comment</param>
        /// <returns>The <see cref="IssueComment"/>s for that was just updated.</returns>
        public Task<IssueComment> Update(string owner, string name, int number, string commentUpdate)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(commentUpdate, "commentUpdate");

            return ApiConnection.Patch<IssueComment>(ApiUrls.IssueComment(owner, name, number), commentUpdate);
        }
    }
}
