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
    public class PullRequestReviewsClient : ApiClient, IPullRequestReviewsClient
    {
        public PullRequestReviewsClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        /// <summary>
        /// Gets reviews for a specified pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#list-reviews-on-a-pull-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/pulls/{pull_number}/reviews")]
        public Task<IReadOnlyList<PullRequestReview>> GetAll(string owner, string name, int pullRequestNumber)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAll(owner, name, pullRequestNumber, ApiOptions.None);
        }

        /// <summary>
        /// Gets reviews for a specified pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#list-reviews-on-a-pull-request</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        [ManualRoute("GET", "/repositories/{id}/pulls/{number}/reviews")]
        public Task<IReadOnlyList<PullRequestReview>> GetAll(long repositoryId, int pullRequestNumber)
        {
            return GetAll(repositoryId, pullRequestNumber, ApiOptions.None);
        }

        /// <summary>
        /// Gets reviews for a specified pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#list-reviews-on-a-pull-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/pulls/{pull_number}/reviews")]
        public Task<IReadOnlyList<PullRequestReview>> GetAll(string owner, string name, int pullRequestNumber, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            var endpoint = ApiUrls.PullRequestReviews(owner, name, pullRequestNumber);
            return ApiConnection.GetAll<PullRequestReview>(endpoint, null, options);
        }

        /// <summary>
        /// Gets reviews for a specified pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#list-reviews-on-a-pull-request</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/repositories/{id}/pulls/{number}/reviews")]
        public Task<IReadOnlyList<PullRequestReview>> GetAll(long repositoryId, int pullRequestNumber, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            var endpoint = ApiUrls.PullRequestReviews(repositoryId, pullRequestNumber);
            return ApiConnection.GetAll<PullRequestReview>(endpoint, options);
        }

        /// <summary>
        /// Gets a single pull request review by ID.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#get-a-single-review</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/pulls/{pull_number}/reviews/{review_id}")]
        public Task<PullRequestReview> Get(string owner, string name, int pullRequestNumber, long reviewId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            var endpoint = ApiUrls.PullRequestReview(owner, name, pullRequestNumber, reviewId);
            return ApiConnection.Get<PullRequestReview>(endpoint);
        }

        /// <summary>
        /// Gets a single pull request review by ID.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#get-a-single-review</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        [ManualRoute("GET", "/repositories/{id}/pulls/{number}/reviews/{review_id}")]
        public Task<PullRequestReview> Get(long repositoryId, int pullRequestNumber, long reviewId)
        {
            var endpoint = ApiUrls.PullRequestReview(repositoryId, pullRequestNumber, reviewId);
            return ApiConnection.Get<PullRequestReview>(endpoint);
        }

        /// <summary>
        /// Creates a review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#create-a-pull-request-review</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The Pull Request number</param>
        /// <param name="review">The review</param>
        [ManualRoute("POST", "/repos/{owner}/{repo}/pulls/{pull_number}/reviews")]
        public Task<PullRequestReview> Create(string owner, string name, int pullRequestNumber, PullRequestReviewCreate review)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(review, nameof(review));

            var endpoint = ApiUrls.PullRequestReviews(owner, name, pullRequestNumber);
            return ApiConnection.Post<PullRequestReview>(endpoint, review, null, null);
        }

        /// <summary>
        /// Creates a review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#create-a-pull-request-review</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The Pull Request number</param>
        /// <param name="review">The review</param>
        [ManualRoute("POST", "/repositories/{id}/pulls/{number}/reviews")]
        public Task<PullRequestReview> Create(long repositoryId, int pullRequestNumber, PullRequestReviewCreate review)
        {
            Ensure.ArgumentNotNull(review, nameof(review));

            var endpoint = ApiUrls.PullRequestReviews(repositoryId, pullRequestNumber);
            return ApiConnection.Post<PullRequestReview>(endpoint, review, null, null);
        }

        /// <summary>
        /// Deletes a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#delete-a-pending-review</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        [ManualRoute("DELETE", "/repos/{owner}/{repo}/pulls/{pull_number}/reviews/{review_id}")]
        public Task Delete(string owner, string name, int pullRequestNumber, long reviewId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            var endpoint = ApiUrls.PullRequestReview(owner, name, pullRequestNumber, reviewId);
            return ApiConnection.Delete(endpoint);
        }

        /// <summary>
        /// Deletes a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#delete-a-pending-review</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        [ManualRoute("DELETE", "/repositories/{id}/pulls/{number}/reviews/{review_id}")]
        public Task Delete(long repositoryId, int pullRequestNumber, long reviewId)
        {
            var endpoint = ApiUrls.PullRequestReview(repositoryId, pullRequestNumber, reviewId);
            return ApiConnection.Delete(endpoint);
        }

        /// <summary>
        /// Dismisses a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#dismiss-a-pull-request-review</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <param name="dismissMessage">The message indicating why the review was dismissed</param>
        [ManualRoute("PUT", "/repos/{owner}/{repo}/pulls/{pull_number}/reviews/{review_id}/dismissals")]
        public Task<PullRequestReview> Dismiss(string owner, string name, int pullRequestNumber, long reviewId, PullRequestReviewDismiss dismissMessage)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(dismissMessage, nameof(dismissMessage));

            var endpoint = ApiUrls.PullRequestReviewDismissal(owner, name, pullRequestNumber, reviewId);
            return ApiConnection.Put<PullRequestReview>(endpoint, dismissMessage);
        }

        /// <summary>
        /// Dismisses a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#dismiss-a-pull-request-review</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <param name="dismissMessage">The message indicating why the review was dismissed</param>
        [ManualRoute("PUT", "/repositories/{id}/pulls/{number}/reviews/{review_id}/dismissals")]
        public Task<PullRequestReview> Dismiss(long repositoryId, int pullRequestNumber, long reviewId, PullRequestReviewDismiss dismissMessage)
        {
            Ensure.ArgumentNotNull(dismissMessage, nameof(dismissMessage));

            var endpoint = ApiUrls.PullRequestReviewDismissal(repositoryId, pullRequestNumber, reviewId);
            return ApiConnection.Put<PullRequestReview>(endpoint, dismissMessage);
        }

        /// <summary>
        /// Submits a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#submit-a-pull-request-review</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <param name="submitMessage">The message and event being submitted for the review</param>
        [ManualRoute("POST", "/repos/{owner}/{repo}/pulls/{pull_number}/reviews/{review_id}/events")]
        public Task<PullRequestReview> Submit(string owner, string name, int pullRequestNumber, long reviewId, PullRequestReviewSubmit submitMessage)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(submitMessage, nameof(submitMessage));

            var endpoint = ApiUrls.PullRequestReviewSubmit(owner, name, pullRequestNumber, reviewId);
            return ApiConnection.Post<PullRequestReview>(endpoint, submitMessage, null, null);
        }

        /// <summary>
        /// Submits a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#submit-a-pull-request-review</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <param name="submitMessage">The message and event being submitted for the review</param>
        [ManualRoute("POST", "/repositories/{id}/pulls/{number}/reviews/{review_id}/events")]
        public Task<PullRequestReview> Submit(long repositoryId, int pullRequestNumber, long reviewId, PullRequestReviewSubmit submitMessage)
        {
            Ensure.ArgumentNotNull(submitMessage, nameof(submitMessage));

            var endpoint = ApiUrls.PullRequestReviewSubmit(repositoryId, pullRequestNumber, reviewId);
            return ApiConnection.Post<PullRequestReview>(endpoint, submitMessage, null, null);
        }

        /// <summary>
        /// Lists comments for a single review
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#get-comments-for-a-single-review</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/pulls/{pull_number}/reviews/{review_id}/comments")]
        public Task<IReadOnlyList<PullRequestReviewComment>> GetAllComments(string owner, string name, int pullRequestNumber, long reviewId)
        {
            return GetAllComments(owner, name, pullRequestNumber, reviewId, ApiOptions.None);
        }

        /// <summary>
        /// Dismisses a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#get-comments-for-a-single-review</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        [ManualRoute("POST", "/repositories/{id}/pulls/{number}/reviews/{review_id}/comments")]
        public Task<IReadOnlyList<PullRequestReviewComment>> GetAllComments(long repositoryId, int pullRequestNumber, long reviewId)
        {
            return GetAllComments(repositoryId, pullRequestNumber, reviewId, ApiOptions.None);
        }

        /// <summary>
        /// Lists comments for a single review
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#get-comments-for-a-single-review</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/pulls/{pull_number}/reviews/{review_id}/comments")]
        public Task<IReadOnlyList<PullRequestReviewComment>> GetAllComments(string owner, string name, int pullRequestNumber, long reviewId, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            var endpoint = ApiUrls.PullRequestReviewComments(owner, name, pullRequestNumber, reviewId);
            return ApiConnection.GetAll<PullRequestReviewComment>(endpoint, null, options);
        }

        /// <summary>
        /// Dismisses a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#get-comments-for-a-single-review</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("POST", "/repositories/{id}/pulls/{number}/reviews/{review_id}/comments")]
        public Task<IReadOnlyList<PullRequestReviewComment>> GetAllComments(long repositoryId, int pullRequestNumber, long reviewId, ApiOptions options)
        {
            var endpoint = ApiUrls.PullRequestReviewComments(repositoryId, pullRequestNumber, reviewId);

            return ApiConnection.GetAll<PullRequestReviewComment>(endpoint, null, options);
        }
    }
}
