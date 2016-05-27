using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public interface ICommitCommentReaction
    {
        /// <summary>
        /// Creates a reaction for an specified Commit Comment
        /// </summary>
        /// <remarks>http://developer.github.com/v3/repos/comments/#create-reaction-for-a-commit-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The comment id</param>
        /// <param name="reaction">The reaction for </param>
        /// <returns></returns>
        Task<Reaction> CreateReaction(string owner, string name, int number, NewReaction reaction);

        /// <summary>
        /// List reactions for a specified Commit Comment
        /// </summary>
        /// <remarks>http://developer.github.com/v3/repos/comments/#list-reaction-for-a-commit-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The comment id</param>        
        /// <returns></returns>
        Task<IReadOnlyList<Reaction>> ListReactions(string owner, string name, int number);
    }
}
