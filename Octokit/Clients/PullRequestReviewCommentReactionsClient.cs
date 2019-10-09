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
        public Task<IReadOnlyList<Reaction>> GetAll(string owner, string name, int number)
        {
            return GetAll(owner, name, number, ApiOptions.None);
        }

        /// <summary>
        /// Get all reactions for a specified Pull Request Review Comment.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#list-reactions-for-a-pull-request-review-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The comment id</param>
        /// <param name="options">Options for changing the API response</param>        
        public Task<IReadOnlyList<Reaction>> GetAll(string owner, string name, int number, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Reaction>(ApiUrls.PullRequestReviewCommentReaction(owner, name, number), null, AcceptHeaders.ReactionsPreview, options);
        }

        /// <summary>
        /// Get all reactions for a specified Pull Request Review Comment.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#list-reactions-for-a-pull-request-review-comment</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The comment id</param>        
        public Task<IReadOnlyList<Reaction>> GetAll(long repositoryId, int number)
        {
            return GetAll(repositoryId, number, ApiOptions.None);
        }

        /// <summary>
        /// Get all reactions for a specified Pull Request Review Comment.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#list-reactions-for-a-pull-request-review-comment</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The comment id</param>
        /// <param name="options">Options for changing the API response</param>        
        public Task<IReadOnlyList<Reaction>> GetAll(long repositoryId, int number, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Reaction>(ApiUrls.PullRequestReviewCommentReaction(repositoryId, number), null, AcceptHeaders.ReactionsPreview, options);
        }

        /// <summary>
        /// Creates a reaction for a specified Pull Request Review Comment.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#create-reaction-for-a-pull-request-review-comment</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The comment id</param>
        /// <param name="reaction">The reaction to create</param>
        public Task<Reaction> Create(string owner, string name, int number, NewReaction reaction)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(reaction, nameof(reaction));

            return ApiConnection.Post<Reaction>(ApiUrls.PullRequestReviewCommentReaction(owner, name, number), reaction, AcceptHeaders.ReactionsPreview);
        }

        /// <summary>
        /// Creates a reaction for a specified Pull Request Review Comment.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/reactions/#create-reaction-for-a-pull-request-review-comment</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The comment id</param>
        /// <param name="reaction">The reaction to create</param>
        public Task<Reaction> Create(long repositoryId, int number, NewReaction reaction)
        {
            Ensure.ArgumentNotNull(reaction, nameof(reaction));

            return ApiConnection.Post<Reaction>(ApiUrls.PullRequestReviewCommentReaction(repositoryId, number), reaction, AcceptHeaders.ReactionsPreview);
        }
    }
}
