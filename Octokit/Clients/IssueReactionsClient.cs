using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public class IssueReactionsClient : ApiClient, IIssueReactionsClient
    {
        public IssueReactionsClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        /// <summary>
        /// Creates a reaction for an specified Issue
        /// </summary>
        /// <remarks>http://developer.github.com/v3/repos/comments/#create-reaction-for-a-commit-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue id</param>
        /// <param name="reaction">The reaction to create</param>
        /// <returns></returns>
        public Task<Reaction> Create(string owner, string name, int number, NewReaction reaction)
        {
            Ensure.ArgumentNotNull(owner, "owner");
            Ensure.ArgumentNotNull(name, "name");
            Ensure.ArgumentNotNull(reaction, "reaction");

            return ApiConnection.Post<Reaction>(ApiUrls.IssueReactions(owner, name, number), reaction, AcceptHeaders.ReactionsPreview);
        }

        /// <summary>
        /// Get all reactions for an specified Issue
        /// </summary>
        /// <remarks>http://developer.github.com/v3/repos/comments/#list-reactions-for-a-commit-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue id</param>        
        /// <returns></returns>
        public Task<IReadOnlyList<Reaction>> GetAll(string owner, string name, int number)
        {
            Ensure.ArgumentNotNull(owner, "owner");
            Ensure.ArgumentNotNull(name, "name");

            return ApiConnection.GetAll<Reaction>(ApiUrls.IssueReactions(owner, name, number), AcceptHeaders.ReactionsPreview);
        }
    }
}
