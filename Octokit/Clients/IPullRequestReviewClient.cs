using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Pull Request Review API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/pulls/reviews/">Review API documentation</a> for more information.
    /// </remarks>
    public interface IPullRequestReviewClient
    {

        /// <summary>
        /// Gets reviews for a specified pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#list-reviews-on-a-pull-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestId">The pull request number</param>
        Task<IReadOnlyList<PullRequestReview>> GetAll(string owner, string name, int pullRequestId);

        /// <summary>
        /// Gets reviews for a specified pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#list-reviews-on-a-pull-request</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestId">The pull request number</param>
        Task<IReadOnlyList<PullRequestReview>> GetAll(long repositoryId, int pullRequestId);

        /// <summary>
        /// Gets reviews for a specified pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#list-reviews-on-a-pull-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestId">The pull request number</param>
        /// <param name="options">Options for changing the API response</param>
        Task<IReadOnlyList<PullRequestReview>> GetAll(string owner, string name, int pullRequestId, ApiOptions options);

        /// <summary>
        /// Gets reviews for a specified pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#list-reviews-on-a-pull-request</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestId">The pull request number</param>
        /// <param name="options">Options for changing the API response</param>
        Task<IReadOnlyList<PullRequestReview>> GetAll(long repositoryId, int pullRequestId, ApiOptions options);

        /// <summary>
        /// Gets a single pull request review by ID.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#get-a-single-review</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestId">The pull request review comment number</param>
        /// <param name="reviewId">The pull request review number</param>
        Task<PullRequestReview> GetReview(string owner, string name, int pullRequestId, int reviewId);

        /// <summary>
        /// Gets a single pull request review by ID.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#get-a-single-review</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestId">The pull request review comment number</param>
        /// <param name="reviewId">The pull request review number</param>
        Task<PullRequestReview> GetReview(long repositoryId, int pullRequestId, int reviewId);

        /// <summary>
        /// Creates a comment on a pull review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#create-a-pull-request-review</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestId">The Pull Request number</param>
        /// <param name="review">The review</param>
        Task<PullRequestReview> Create(string owner, string name, int pullRequestId, PullRequestReviewCreate review);

        /// <summary>
        /// Creates a comment on a pull review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#create-a-pull-request-review</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestId">The Pull Request number</param>
        /// <param name="review">The review</param>
        Task<PullRequestReview> Create(long repositoryId, int pullRequestId, PullRequestReviewCreate review);


        /// <summary>
        /// Deletes a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#delete-a-pending-review</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestId">The pull request review comment number</param>
        /// <param name="reviewId">The pull request review number</param>
        Task Delete(string owner, string name, int pullRequestId, int reviewId);

        /// <summary>
        /// Deletes a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#delete-a-pending-review</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestId">The pull request review comment number</param>
        /// <param name="reviewId">The pull request review number</param>
        Task Delete(long repositoryId, int pullRequestId, int reviewId);


        /// <summary>
        /// Submits an event to a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#submit-a-pull-request-review</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestId">The pull request review comment number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <param name="submitMessage">The message and event being submitted for the review</param>
        Task<PullRequestReview> Submit(string owner, string name, int pullRequestId, int reviewId, PullRequestReviewSubmit submitMessage);

        /// <summary>
        /// Submits an event to a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#submit-a-pull-request-review</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestId">The pull request review comment number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <param name="submitMessage">The message and event being submitted for the review</param>
        Task<PullRequestReview> Submit(long repositoryId, int pullRequestId, int reviewId, PullRequestReviewSubmit submitMessage);

        /// <summary>
        /// Dismisses a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#dismiss-a-pull-request-review</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestId">The pull request review comment number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <param name="dismissMessage">The message indicating why the review was dismissed</param>
        Task<PullRequestReview> Dismiss(string owner, string name, int pullRequestId, int reviewId, PullRequestReviewDismiss dismissMessage);

        /// <summary>
        /// Dismisses a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#dismiss-a-pull-request-review</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestId">The pull request review comment number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <param name="dismissMessage">The message indicating why the review was dismissed</param>
        Task<PullRequestReview> Dismiss(long repositoryId, int pullRequestId, int reviewId, PullRequestReviewDismiss dismissMessage);

        /// <summary>
        /// Lists comments for a single review
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#get-comments-for-a-single-review</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestId">The pull request review comment number</param>
        /// <param name="reviewId">The pull request review number</param>
        Task<IReadOnlyList<PullRequestReviewComment>> GetAllComments(string owner, string name, int pullRequestId, int reviewId);

        /// <summary>
        /// Dismisses a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#get-comments-for-a-single-review</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestId">The pull request review comment number</param>
        /// <param name="reviewId">The pull request review number</param>
        Task<IReadOnlyList<PullRequestReviewComment>> GetAllComments(long repositoryId, int pullRequestId, int reviewId);

        /// <summary>
        /// Lists comments for a single review
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#get-comments-for-a-single-review</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestId">The pull request review comment number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <param name="options">Options for changing the API response</param>
        Task<IReadOnlyList<PullRequestReviewComment>> GetAllComments(string owner, string name, int pullRequestId, int reviewId, ApiOptions options);

        /// <summary>
        /// Dismisses a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#get-comments-for-a-single-review</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestId">The pull request review comment number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <param name="options">Options for changing the API response</param>
        Task<IReadOnlyList<PullRequestReviewComment>> GetAllComments(long repositoryId, int pullRequestId, int reviewId, ApiOptions options);
    }
}