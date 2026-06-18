using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Pull Request Review API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/pulls/reviews/">Review API documentation</a> for more information.
    /// </remarks>
    public interface IPullRequestReviewsClient
    {
        /// <summary>
        /// Gets reviews for a specified pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#list-reviews-on-a-pull-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<PullRequestReview>> GetAll(string owner, string name, int pullRequestNumber, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets reviews for a specified pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#list-reviews-on-a-pull-request</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<PullRequestReview>> GetAll(long repositoryId, int pullRequestNumber, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets reviews for a specified pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#list-reviews-on-a-pull-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<PullRequestReview>> GetAll(string owner, string name, int pullRequestNumber, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets reviews for a specified pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#list-reviews-on-a-pull-request</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<PullRequestReview>> GetAll(long repositoryId, int pullRequestNumber, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a single pull request review by ID.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#get-a-single-review</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<PullRequestReview> Get(string owner, string name, int pullRequestNumber, long reviewId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a single pull request review by ID.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#get-a-single-review</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<PullRequestReview> Get(long repositoryId, int pullRequestNumber, long reviewId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#create-a-pull-request-review</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="review">The review</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<PullRequestReview> Create(string owner, string name, int pullRequestNumber, PullRequestReviewCreate review, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#create-a-pull-request-review</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="review">The review</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<PullRequestReview> Create(long repositoryId, int pullRequestNumber, PullRequestReviewCreate review, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#delete-a-pending-review</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task Delete(string owner, string name, int pullRequestNumber, long reviewId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#delete-a-pending-review</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task Delete(long repositoryId, int pullRequestNumber, long reviewId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Submits a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#submit-a-pull-request-review</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <param name="submitMessage">The message and event being submitted for the review</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<PullRequestReview> Submit(string owner, string name, int pullRequestNumber, long reviewId, PullRequestReviewSubmit submitMessage, CancellationToken cancellationToken = default);

        /// <summary>
        /// Submits a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#submit-a-pull-request-review</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <param name="submitMessage">The message and event being submitted for the review</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<PullRequestReview> Submit(long repositoryId, int pullRequestNumber, long reviewId, PullRequestReviewSubmit submitMessage, CancellationToken cancellationToken = default);

        /// <summary>
        /// Dismisses a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#dismiss-a-pull-request-review</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <param name="dismissMessage">The message indicating why the review was dismissed</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<PullRequestReview> Dismiss(string owner, string name, int pullRequestNumber, long reviewId, PullRequestReviewDismiss dismissMessage, CancellationToken cancellationToken = default);

        /// <summary>
        /// Dismisses a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#dismiss-a-pull-request-review</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <param name="dismissMessage">The message indicating why the review was dismissed</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<PullRequestReview> Dismiss(long repositoryId, int pullRequestNumber, long reviewId, PullRequestReviewDismiss dismissMessage, CancellationToken cancellationToken = default);

        /// <summary>
        /// Lists comments for a single review
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#get-comments-for-a-single-review</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<PullRequestReviewComment>> GetAllComments(string owner, string name, int pullRequestNumber, long reviewId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Lists comments for a single review
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#get-comments-for-a-single-review</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<PullRequestReviewComment>> GetAllComments(long repositoryId, int pullRequestNumber, long reviewId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Lists comments for a single review
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#get-comments-for-a-single-review</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<PullRequestReviewComment>> GetAllComments(string owner, string name, int pullRequestNumber, long reviewId, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Lists comments for a single review
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#get-comments-for-a-single-review</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<PullRequestReviewComment>> GetAllComments(long repositoryId, int pullRequestNumber, long reviewId, ApiOptions options, CancellationToken cancellationToken = default);
    }
}
