using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Pull Request Review API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/pulls/reviews/">Review API documentation</a> for more information.
    /// </remarks>
    public class PullRequestReviewClient : ApiClient, IPullRequestReviewClient
    {
        public PullRequestReviewClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        /// <summary>
        /// Gets reviews for a specified pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#list-reviews-on-a-pull-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestId">The pull request number</param>
        public Task<IReadOnlyList<PullRequestReview>> GetAll(string owner, string name, int pullRequestId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return GetAll(owner, name, pullRequestId,  ApiOptions.None);
        }

        /// <summary>
        /// Gets reviews for a specified pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#list-reviews-on-a-pull-request</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestId">The pull request number</param>
        public Task<IReadOnlyList<PullRequestReview>> GetAll(long repositoryId, int pullRequestId)
        {
            return GetAll(repositoryId, pullRequestId, ApiOptions.None);
        }

        /// <summary>
        /// Gets reviews for a specified pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#list-reviews-on-a-pull-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestId">The pull request number</param>
        /// <param name="options">Options for changing the API response</param>
        public Task<IReadOnlyList<PullRequestReview>> GetAll(string owner, string name, int pullRequestId, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(options, "options");
            var endpoint = ApiUrls.PullRequestReviews(owner, name, pullRequestId);
            return ApiConnection.GetAll<PullRequestReview>(endpoint, null, options);
        }

        /// <summary>
        /// Gets reviews for a specified pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#list-reviews-on-a-pull-request</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestId">The pull request number</param>
        /// <param name="options">Options for changing the API response</param>
        public Task<IReadOnlyList<PullRequestReview>> GetAll(long repositoryId, int pullRequestId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<PullRequestReview>(ApiUrls.PullRequestReviews(repositoryId, pullRequestId), options);
        }

        /// <summary>
        /// Gets a single pull request review by ID.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#get-a-single-review</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestId">The pull request review comment number</param>
        /// <param name="reviewId">The pull request review number</param>
        public Task<PullRequestReview> GetReview(string owner, string name, int pullRequestId, int reviewId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.Get<PullRequestReview>(ApiUrls.PullRequestReview(owner, name, pullRequestId, reviewId));
        }

        /// <summary>
        /// Gets a single pull request review by ID.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#get-a-single-review</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestId">The pull request review comment number</param>
        /// <param name="reviewId">The pull request review number</param>
        public Task<PullRequestReview> GetReview(long repositoryId, int pullRequestId, int reviewId)
        {
            return ApiConnection.Get<PullRequestReview>(ApiUrls.PullRequestReview(repositoryId, pullRequestId, reviewId));
        }

        /// <summary>
        /// Creates a comment on a pull review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#create-a-pull-request-review</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestId">The Pull Request number</param>
        /// <param name="review">The review</param>
        public async Task<PullRequestReview> Create(string owner, string name, int pullRequestId, PullRequestReviewCreate review)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(review, "review");

            var endpoint = ApiUrls.PullRequestReviews(owner, name, pullRequestId);
            var response = await ApiConnection.Connection.Post<PullRequestReview>(endpoint, review, null, null).ConfigureAwait(false);

            if (response.HttpResponse.StatusCode != HttpStatusCode.Created)
            {
                throw new ApiException("Invalid Status Code returned. Expected a 201", response.HttpResponse.StatusCode);
            }

            return response.Body;
        }

        /// <summary>
        /// Creates a comment on a pull review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#create-a-pull-request-review</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestId">The Pull Request number</param>
        /// <param name="review">The review</param>
        public async Task<PullRequestReview> Create(long repositoryId, int pullRequestId, PullRequestReviewCreate review)
        {
            Ensure.ArgumentNotNull(review, "review");

            var endpoint = ApiUrls.PullRequestReviews(repositoryId, pullRequestId);
            var response = await ApiConnection.Connection.Post<PullRequestReview>(endpoint, review, null, null).ConfigureAwait(false);

            if (response.HttpResponse.StatusCode != HttpStatusCode.Created)
            {
                throw new ApiException("Invalid Status Code returned. Expected a 201", response.HttpResponse.StatusCode);
            }

            return response.Body;
        }

        /// <summary>
        /// Deletes a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#delete-a-pending-review</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestId">The pull request review comment number</param>
        /// <param name="reviewId">The pull request review number</param>
        public Task Delete(string owner, string name, int pullRequestId, int reviewId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            var endpoint = ApiUrls.PullRequestReview(owner, name, pullRequestId, reviewId);
            return ApiConnection.Delete(endpoint);
        }

        /// <summary>
        /// Deletes a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#delete-a-pending-review</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestId">The pull request review comment number</param>
        /// <param name="reviewId">The pull request review number</param>
        public Task Delete(long repositoryId, int pullRequestId, int reviewId)
        {
            var endpoint = ApiUrls.PullRequestReview(repositoryId, pullRequestId, reviewId);
            return ApiConnection.Delete(endpoint);
        }

        /// <summary>
        /// Dismisses a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#dismiss-a-pull-request-review</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestId">The pull request review comment number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <param name="dismissMessage">The message indicating why the review was dismissed</param>
        public async Task<PullRequestReview> Dismiss(string owner, string name, int pullRequestId, int reviewId, PullRequestReviewDismiss dismissMessage)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(dismissMessage, "dismissMessage");

            var endpoint = ApiUrls.PullRequestReviewDismissal(owner, name, pullRequestId, reviewId);
            return await ApiConnection.Put<PullRequestReview>(endpoint, dismissMessage);
        }

        /// <summary>
        /// Dismisses a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#dismiss-a-pull-request-review</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestId">The pull request review comment number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <param name="dismissMessage">The message indicating why the review was dismissed</param>
        public async Task<PullRequestReview> Dismiss(long repositoryId, int pullRequestId, int reviewId, PullRequestReviewDismiss dismissMessage)
        {
            Ensure.ArgumentNotNull(dismissMessage, "dismissMessage");

            var endpoint = ApiUrls.PullRequestReviewDismissal(repositoryId, pullRequestId, reviewId);
            return await ApiConnection.Put<PullRequestReview>(endpoint, dismissMessage);
        }

        /// <summary>
        /// Submits an event to a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#submit-a-pull-request-review</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestId">The pull request review comment number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <param name="submitMessage">The message and event being submitted for the review</param>
        public async Task<PullRequestReview> Submit(string owner, string name, int pullRequestId, int reviewId, PullRequestReviewSubmit submitMessage)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(submitMessage, "submitMessage");

            var endpoint = ApiUrls.PullRequestReviewSubmit(owner, name, pullRequestId, reviewId);
            return await ApiConnection.Post<PullRequestReview>(endpoint, submitMessage, null, null);
        }

        /// <summary>
        /// Submits an event to a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#submit-a-pull-request-review</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestId">The pull request review comment number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <param name="submitMessage">The message and event being submitted for the review</param>
        public async Task<PullRequestReview> Submit(long repositoryId, int pullRequestId, int reviewId, PullRequestReviewSubmit submitMessage)
        {
            Ensure.ArgumentNotNull(submitMessage, "submitMessage");

            var endpoint = ApiUrls.PullRequestReviewSubmit(repositoryId, pullRequestId, reviewId);
            return await ApiConnection.Post<PullRequestReview>(endpoint, submitMessage, null, null);
        }

        /// <summary>
        /// Lists comments for a single review
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#get-comments-for-a-single-review</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestId">The pull request review comment number</param>
        /// <param name="reviewId">The pull request review number</param>
        public async Task<IReadOnlyList<PullRequestReviewComment>> GetAllComments(string owner, string name, int pullRequestId, int reviewId)
        {
            return await GetAllComments(owner, name, pullRequestId, reviewId, ApiOptions.None);
        }

        /// <summary>
        /// Dismisses a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#get-comments-for-a-single-review</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestId">The pull request review comment number</param>
        /// <param name="reviewId">The pull request review number</param>
        public async Task<IReadOnlyList<PullRequestReviewComment>> GetAllComments(long repositoryId, int pullRequestId, int reviewId)
        {
            return await GetAllComments(repositoryId, pullRequestId, reviewId, ApiOptions.None);
        }

        /// <summary>
        /// Lists comments for a single review
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#get-comments-for-a-single-review</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestId">The pull request review comment number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <param name="options">Options for changing the API response</param>
        public async Task<IReadOnlyList<PullRequestReviewComment>> GetAllComments(string owner, string name, int pullRequestId, int reviewId, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            var endpoint = ApiUrls.PullRequestReviewComments(owner, name, pullRequestId, reviewId);
            return await ApiConnection.GetAll<PullRequestReviewComment>(endpoint, null, options);
        }

        /// <summary>
        /// Dismisses a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#get-comments-for-a-single-review</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestId">The pull request review comment number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <param name="options">Options for changing the API response</param>
        public async Task<IReadOnlyList<PullRequestReviewComment>> GetAllComments(long repositoryId, int pullRequestId, int reviewId, ApiOptions options)
        {
            var endpoint = ApiUrls.PullRequestReviewComments(repositoryId, pullRequestId, reviewId);
            return await ApiConnection.GetAll<PullRequestReviewComment>(endpoint, null, options);
        }
    }
}
