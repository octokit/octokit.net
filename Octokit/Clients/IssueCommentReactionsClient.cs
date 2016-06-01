using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public class IssueCommentReactionsClient : ApiClient, IIssueCommentReactionsClient
    {
        public IssueCommentReactionsClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        /// <summary>
        /// Creates a reaction for an specified Issue Comment
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#create-reactions-for-an-issue-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The comment id</param>
        /// <param name="reaction">The reaction to create</param>
        /// <returns></returns>
        public Task<Reaction> Create(string owner, string name, int number, NewReaction reaction)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(reaction, "reaction");

            return ApiConnection.Post<Reaction>(ApiUrls.IssueCommentReactions(owner, name, number), reaction, AcceptHeaders.ReactionsPreview);
        }

        /// <summary>
        /// Get all reactions for an specified Issue Comment
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#list-reactions-for-an-issue-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The comment id</param>        
        /// <returns></returns>
        public Task<IReadOnlyList<Reaction>> GetAll(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.GetAll<Reaction>(ApiUrls.IssueCommentReactions(owner, name, number), AcceptHeaders.ReactionsPreview);
        }
    }
}
