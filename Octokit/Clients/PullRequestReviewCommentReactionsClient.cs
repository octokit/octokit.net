using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Reactions API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/reactions/">Reactions API documentation</a> for more information.
    /// </remarks>
    public class PullRequestReviewCommentReactionsClient : ApiClient, IPullRequestReviewCommentReactionsClient
    {
        public PullRequestReviewCommentReactionsClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        /// <summary>
        /// Get all reactions for a specified Pull Request Review Comment.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#list-reactions-for-a-pull-request-review-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The comment id</param>        
        /// <returns>A <see cref="Task{T}"/> of <see cref="IReadOnlyList{Reactions}"/> representing <see cref="Reaction"/>s for a specified pull request review comment.</returns>
        public Task<IReadOnlyList<Reaction>> GetAll(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.GetAll<Reaction>(ApiUrls.PullRequestReviewCommentReaction(owner, name, number), AcceptHeaders.ReactionsPreview);
        }

        /// <summary>
        /// Get all reactions for a specified Pull Request Review Comment.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#list-reactions-for-a-pull-request-review-comment</remarks>
        /// <param name="repositoryId">The ID of the repository</param>
        /// <param name="number">The comment id</param>        
        /// <returns>A <see cref="Task{T}"/> of <see cref="IReadOnlyList{Reactions}"/> representing <see cref="Reaction"/>s for a specified pull request review comment.</returns>
        public Task<IReadOnlyList<Reaction>> GetAll(int repositoryId, int number)
        {
            return ApiConnection.GetAll<Reaction>(ApiUrls.PullRequestReviewCommentReaction(repositoryId, number), AcceptHeaders.ReactionsPreview);
        }

        /// <summary>
        /// Creates a reaction for a specified Pull Request Review Comment.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#create-reaction-for-a-pull-request-review-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The comment id</param>
        /// <param name="reaction">The reaction to create</param>
        /// <returns>A <see cref="Task{Reaction}"/> representing created <see cref="Reaction"/> for a specified pull request review comment.</returns>
        public Task<Reaction> Create(string owner, string name, int number, NewReaction reaction)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(reaction, "reaction");

            return ApiConnection.Post<Reaction>(ApiUrls.PullRequestReviewCommentReaction(owner, name, number), reaction, AcceptHeaders.ReactionsPreview);
        }

        /// <summary>
        /// Creates a reaction for a specified Pull Request Review Comment.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#create-reaction-for-a-pull-request-review-comment</remarks>
        /// <param name="repositoryId">The ID of the repository</param>
        /// <param name="number">The comment id</param>
        /// <param name="reaction">The reaction to create</param>
        /// <returns>A <see cref="Task{Reaction}"/> representing created <see cref="Reaction"/> for a specified pull request review comment.</returns>
        public Task<Reaction> Create(int repositoryId, int number, NewReaction reaction)
        {
            Ensure.ArgumentNotNull(reaction, "reaction");

            return ApiConnection.Post<Reaction>(ApiUrls.PullRequestReviewCommentReaction(repositoryId, number), reaction, AcceptHeaders.ReactionsPreview);
        }
    }
}
