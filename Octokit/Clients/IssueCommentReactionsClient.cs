using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Reactions API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/reactions">Reactions API documentation</a> for more information.
    /// </remarks>
    public class IssueCommentReactionsClient : ApiClient, IIssueCommentReactionsClient
    {
        public IssueCommentReactionsClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        /// <summary>
        /// Creates a reaction for a specified Issue Comment
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#create-reactions-for-an-issue-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The comment id</param>
        /// <param name="reaction">The reaction to create</param>
        [ManualRoute("POST", "/repos/{owner}/{repo}/issues/comments/{number}/reactions")]
        public Task<Reaction> Create(string owner, string name, int number, NewReaction reaction)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(reaction, nameof(reaction));

            return ApiConnection.Post<Reaction>(ApiUrls.IssueCommentReactions(owner, name, number), reaction);
        }

        /// <summary>
        /// Creates a reaction for a specified Issue Comment
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#create-reaction-for-an-issue-comment</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The comment id</param>
        /// <param name="reaction">The reaction to create</param>
        [ManualRoute("POST", "/repositories/{0}/issues/comments/{number}/reactions")]
        public Task<Reaction> Create(long repositoryId, int number, NewReaction reaction)
        {
            Ensure.ArgumentNotNull(reaction, nameof(reaction));

            return ApiConnection.Post<Reaction>(ApiUrls.IssueCommentReactions(repositoryId, number), reaction);
        }

        /// <summary>
        /// Get all reactions for a specified Issue Comment
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#list-reactions-for-an-issue-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The comment id</param>
        [ManualRoute("POST", "/repos/{owner}/{repo}/issues/comments/{number}/reactions")]
        public Task<IReadOnlyList<Reaction>> GetAll(string owner, string name, int number)
        {
            return GetAll(owner, name, number, ApiOptions.None);
        }

        /// <summary>
        /// Get all reactions for a specified Issue Comment
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#list-reactions-for-an-issue-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The comment id</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("POST", "/repos/{owner}/{repo}/issues/comments/{number}/reactions")]
        public Task<IReadOnlyList<Reaction>> GetAll(string owner, string name, int number, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Reaction>(ApiUrls.IssueCommentReactions(owner, name, number), null, options);
        }

        /// <summary>
        /// Get all reactions for a specified Issue Comment
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#list-reactions-for-an-issue-comment</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The comment id</param>
        [ManualRoute("GET", "/repositories/{0}/issues/comments/{number}/reactions")]
        public Task<IReadOnlyList<Reaction>> GetAll(long repositoryId, int number)
        {
            return GetAll(repositoryId, number, ApiOptions.None);
        }

        /// <summary>
        /// Get all reactions for a specified Issue Comment
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#list-reactions-for-an-issue-comment</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The comment id</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/repositories/{0}/issues/comments/{number}/reactions")]
        public Task<IReadOnlyList<Reaction>> GetAll(long repositoryId, int number, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Reaction>(ApiUrls.IssueCommentReactions(repositoryId, number), null, options);
        }

        /// <summary>
        /// Deletes a reaction for a specified Issue Comment
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#list-reactions-for-a-commit-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="commentId">The issue id</param>
        /// <param name="reactionId">The reaction id</param>
        /// <returns></returns>
        [ManualRoute("DELETE", "/repos/{owner}/{repo}/issues/comments/{comment_id}/reactions/{reaction_id}")]
        public Task Delete(string owner, string name, int commentId, int reactionId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Delete(ApiUrls.IssueCommentReaction(owner, name, commentId, reactionId));
        }

        /// <summary>
        /// Deletes a reaction for a specified Issue Comment
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#list-reactions-for-a-commit-comment</remarks>
        /// <param name="repositoryId">The owner of the repository</param>
        /// <param name="commentId">The issue id</param>
        /// <param name="reactionId">The reaction id</param>
        /// <returns></returns>
        [ManualRoute("DELETE", "/repositories/{id}/issues/comments/{comment_id}/reactions/{reaction_id}")]
        public Task Delete(long repositoryId, int commentId, int reactionId)
        {
            return ApiConnection.Delete(ApiUrls.IssueCommentReaction(repositoryId, commentId, reactionId));
        }
    }
}
