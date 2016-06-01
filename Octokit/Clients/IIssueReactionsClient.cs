using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public interface IIssueReactionsClient
    {
        /// <summary>
        /// Get all reactions for an specified Issue
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#list-reactions-for-an-issue</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue id</param>        
        /// <returns></returns>
        Task<IReadOnlyList<Reaction>> GetAll(string owner, string name, int number);

        /// <summary>
        /// Creates a reaction for an specified Issue
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#create-reaction-for-an-issue</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue id</param>
        /// <param name="reaction">The reaction to create</param>
        /// <returns></returns>
        Task<Reaction> Create(string owner, string name, int number, NewReaction reaction);
    }
}
