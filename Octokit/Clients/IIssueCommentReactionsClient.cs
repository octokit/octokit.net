using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public interface IIssueCommentReactionsClient
    {
        /// <summary>
        /// Creates a reaction for an specified Issue Comment
        /// </summary>
        /// <remarks>http://developer.github.com/v3/repos/comments/#create-reaction-for-an-issue-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The comment id</param>
        /// <param name="reaction">The reaction to create</param>
        /// <returns></returns>
        Task<Reaction> Create(string owner, string name, int number, NewReaction reaction);

        /// <summary>
        /// Get all reactions for a specified Commit Comment
        /// </summary>
        /// <remarks>http://developer.github.com/v3/repos/comments/#list-reactions-for-an-issue-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The comment id</param>        
        /// <returns></returns>
        Task<IReadOnlyList<Reaction>> GetAll(string owner, string name, int number);
    }
}
