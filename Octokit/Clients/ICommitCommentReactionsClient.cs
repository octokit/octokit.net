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
    public interface ICommitCommentReactionsClient
    {
        /// <summary>
        /// Creates a reaction for a specified Commit Comment
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#create-reaction-for-a-commit-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The comment id</param>
        /// <param name="reaction">The reaction to create</param>
        /// <returns></returns>
        Task<Reaction> Create(string owner, string name, int number, NewReaction reaction);

        /// <summary>
        /// Creates a reaction for a specified Commit Comment
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#create-reaction-for-a-commit-comment</remarks>
        /// <param name="repositoryId">The owner of the repository</param>
        /// <param name="number">The comment id</param>
        /// <param name="reaction">The reaction to create</param>
        /// <returns></returns>
        Task<Reaction> Create(long repositoryId, int number, NewReaction reaction);

        /// <summary>
        /// Get all reactions for a specified Commit Comment
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#list-reactions-for-a-commit-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The comment id</param>
        /// <returns></returns>
        Task<IReadOnlyList<Reaction>> GetAll(string owner, string name, int number);

        /// <summary>
        /// Get all reactions for a specified Commit Comment
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#list-reactions-for-a-commit-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The comment id</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns></returns>
        Task<IReadOnlyList<Reaction>> GetAll(string owner, string name, int number, ApiOptions options);

        /// <summary>
        /// Get all reactions for a specified Commit Comment
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#list-reactions-for-a-commit-comment</remarks>
        /// <param name="repositoryId">The owner of the repository</param>
        /// <param name="number">The comment id</param>
        /// <returns></returns>
        Task<IReadOnlyList<Reaction>> GetAll(long repositoryId, int number);

        /// <summary>
        /// Get all reactions for a specified Commit Comment
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#list-reactions-for-a-commit-comment</remarks>
        /// <param name="repositoryId">The owner of the repository</param>
        /// <param name="number">The comment id</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns></returns>
        Task<IReadOnlyList<Reaction>> GetAll(long repositoryId, int number, ApiOptions options);

        /// <summary>
        /// Deletes a reaction for a specified Commit Comment
        /// </summary>
        /// <remarks>https://docs.github.com/rest/reactions#delete-a-commit-comment-reaction</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="commentId">The comment id</param>
        /// <param name="reactionId">The reaction id</param>
        /// <returns></returns>
        Task Delete(string owner, string name, int commentId, int reactionId);

        /// <summary>
        /// Deletes a reaction for a specified Commit Comment
        /// </summary>
        /// <remarks>https://docs.github.com/rest/reactions#delete-a-commit-comment-reaction</remarks>
        /// <param name="repositoryId">The owner of the repository</param>
        /// <param name="commentId">The comment id</param>
        /// <param name="reactionId">The reaction id</param>
        /// <returns></returns>
        Task Delete(long repositoryId, int commentId, int reactionId);
    }
}
