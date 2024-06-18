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
        /// <param name="commentId">The comment id</param>
        /// <param name="reaction">The reaction to create</param>
        [ManualRoute("POST", "/repos/{owner}/{repo}/issues/comments/{number}/reactions")]
        public Task<Reaction> Create(string owner, string name, long commentId, NewReaction reaction)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(reaction, nameof(reaction));

            return ApiConnection.Post<Reaction>(ApiUrls.IssueCommentReactions(owner, name, commentId), reaction);
        }

        /// <summary>
        /// Creates a reaction for a specified Issue Comment
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#create-reaction-for-an-issue-comment</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="commentId">The comment id</param>
        /// <param name="reaction">The reaction to create</param>
        [ManualRoute("POST", "/repositories/{0}/issues/comments/{number}/reactions")]
        public Task<Reaction> Create(long repositoryId, long commentId, NewReaction reaction)
        {
            Ensure.ArgumentNotNull(reaction, nameof(reaction));

            return ApiConnection.Post<Reaction>(ApiUrls.IssueCommentReactions(repositoryId, commentId), reaction);
        }

        /// <summary>
        /// Get all reactions for a specified Issue Comment
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#list-reactions-for-an-issue-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="commentId">The comment id</param>
        [ManualRoute("POST", "/repos/{owner}/{repo}/issues/comments/{number}/reactions")]
        public Task<IReadOnlyList<Reaction>> GetAll(string owner, string name, long commentId)
        {
            return GetAll(owner, name, commentId, ApiOptions.None);
        }

        /// <summary>
        /// Get all reactions for a specified Issue Comment
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#list-reactions-for-an-issue-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="commentId">The comment id</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("POST", "/repos/{owner}/{repo}/issues/comments/{number}/reactions")]
        public Task<IReadOnlyList<Reaction>> GetAll(string owner, string name, long commentId, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Reaction>(ApiUrls.IssueCommentReactions(owner, name, commentId), null, options);
        }

        /// <summary>
        /// Get all reactions for a specified Issue Comment
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#list-reactions-for-an-issue-comment</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="commentId">The comment id</param>
        [ManualRoute("GET", "/repositories/{0}/issues/comments/{number}/reactions")]
        public Task<IReadOnlyList<Reaction>> GetAll(long repositoryId, long commentId)
        {
            return GetAll(repositoryId, commentId, ApiOptions.None);
        }

        /// <summary>
        /// Get all reactions for a specified Issue Comment
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#list-reactions-for-an-issue-comment</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="commentId">The comment id</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/repositories/{0}/issues/comments/{number}/reactions")]
        public Task<IReadOnlyList<Reaction>> GetAll(long repositoryId, long commentId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Reaction>(ApiUrls.IssueCommentReactions(repositoryId, commentId), null, options);
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
        public Task Delete(string owner, string name, long commentId, long reactionId)
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
        public Task Delete(long repositoryId, long commentId, long reactionId)
        {
            return ApiConnection.Delete(ApiUrls.IssueCommentReaction(repositoryId, commentId, reactionId));
        }
    }
}
