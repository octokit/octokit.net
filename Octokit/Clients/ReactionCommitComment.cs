using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public class CommitCommentReaction : ApiClient, ICommitCommentReaction
    {
        public CommitCommentReaction(IApiConnection apiConnection) 
            : base(apiConnection)
        {
        }

        /// <summary>
        /// Creates a reaction for a specified Commit Comment
        /// </summary>
        /// <remarks>http://developer.github.com/v3/repos/comments/#create-reaction-for-a-commit-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The comment id</param>
        /// <param name="reaction">The reaction for </param>
        /// <returns></returns>
        public Task<Reaction> CreateReaction(string owner, string name, int number, NewReaction reaction)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(reaction, "reaction");

            return ApiConnection.Post<Reaction>(ApiUrls.CommitCommentReaction(owner, name, number), reaction, AcceptHeaders.ReactionsPreview);
        }

        /// <summary>
        /// List reactions for a specified Commit Comment
        /// </summary>
        /// <remarks>http://developer.github.com/v3/repos/comments/#create-reaction-for-a-commit-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The comment id</param>
        /// <param name="reaction">The reaction for </param>
        /// <returns></returns>
        public Task<IReadOnlyList<Reaction>> ListReactions(string owner, string name, int number, NewReaction reaction)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(reaction, "reaction");

            return ApiConnection.Get<Reaction>(ApiUrls.CommitCommentReaction(owner, name, number),"", AcceptHeaders.ReactionsPreview);
        }
    }
}
