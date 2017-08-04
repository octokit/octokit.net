using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Pull Request Review API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/pulls/reviews/">Review API documentation</a> for more information.
    /// </remarks>
    public class ObservablePullRequestReviewClient : IObservablePullRequestReviewClient
    {
        readonly IPullRequestReviewClient _client;
        readonly IConnection _connection;

        public ObservablePullRequestReviewClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.PullRequest.PullRequestReview;
            _connection = client.Connection;
        }

        /// <summary>
        /// Creates a comment on a pull review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#create-a-pull-request-review</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestId">The Pull Request number</param>
        /// <param name="review">The review</param>
        public IObservable<PullRequestReview> Create(string owner, string name, int pullRequestId, PullRequestReviewCreate review)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(review, "review");
            return _client.Create(owner, name, pullRequestId, review).ToObservable();
        }

        /// <summary>
        /// Creates a comment on a pull review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#create-a-pull-request-review</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestId">The Pull Request number</param>
        /// <param name="review">The review</param>
        public IObservable<PullRequestReview> Create(long repositoryId, int pullRequestId, PullRequestReviewCreate review)
        {
            Ensure.ArgumentNotNull(review, "review");
            return _client.Create(repositoryId, pullRequestId, review).ToObservable();
        }

        /// <summary>
        /// Deletes a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#delete-a-pending-review</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestId">The pull request review comment number</param>
        /// <param name="reviewId">The pull request review number</param>
        public IObservable<Unit> Delete(string owner, string name, int pullRequestId, int reviewId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            return _client.Delete(owner, name, pullRequestId, reviewId).ToObservable();
        }

        /// <summary>
        /// Deletes a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#delete-a-pending-review</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestId">The pull request review comment number</param>
        /// <param name="reviewId">The pull request review number</param>
        public IObservable<Unit> Delete(long repositoryId, int pullRequestId, int reviewId)
        {
            return _client.Delete(repositoryId, pullRequestId, reviewId).ToObservable();
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
        public IObservable<PullRequestReview> Dismiss(string owner, string name, int pullRequestId, int reviewId, PullRequestReviewDismiss dismissMessage)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(dismissMessage, "dismissMessage");
            return _client.Dismiss(owner, name, pullRequestId, reviewId, dismissMessage).ToObservable();
        }

        /// <summary>
        /// Dismisses a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#dismiss-a-pull-request-review</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestId">The pull request review comment number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <param name="dismissMessage">The message indicating why the review was dismissed</param>
        public IObservable<PullRequestReview> Dismiss(long repositoryId, int pullRequestId, int reviewId, PullRequestReviewDismiss dismissMessage)
        {
            Ensure.ArgumentNotNull(dismissMessage, "dismissMessage");
            return GetAll(repositoryId, pullRequestId);
        }

        /// <summary>
        /// Gets reviews for a specified pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#list-reviews-on-a-pull-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestId">The pull request number</param>
        public IObservable<PullRequestReview> GetAll(string owner, string name, int pullRequestId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return GetAll(owner, name, pullRequestId, ApiOptions.None);
        }

        /// <summary>
        /// Gets reviews for a specified pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#list-reviews-on-a-pull-request</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestId">The pull request number</param>
        public IObservable<PullRequestReview> GetAll(long repositoryId, int pullRequestId)
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
        public IObservable<PullRequestReview> GetAll(string owner, string name, int pullRequestId, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(options, "options");
            return _connection.GetAndFlattenAllPages<PullRequestReview>(ApiUrls.PullRequestReviews(owner, name, pullRequestId), null, null, options);
        }

        /// <summary>
        /// Gets reviews for a specified pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#list-reviews-on-a-pull-request</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestId">The pull request number</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<PullRequestReview> GetAll(long repositoryId, int pullRequestId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");
            return _connection.GetAndFlattenAllPages<PullRequestReview>(ApiUrls.PullRequestReviews(repositoryId, pullRequestId), null, null, options);
        }

        /// <summary>
        /// Gets a single pull request review by ID.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#get-a-single-review</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestId">The pull request review comment number</param>
        /// <param name="reviewId">The pull request review number</param>
        public IObservable<PullRequestReview> GetReview(string owner, string name, int pullRequestId, int reviewId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            return _client.GetReview(owner, name, pullRequestId, reviewId).ToObservable();
        }

        /// <summary>
        /// Gets a single pull request review by ID.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#get-a-single-review</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestId">The pull request review comment number</param>
        /// <param name="reviewId">The pull request review number</param>
        public IObservable<PullRequestReview> GetReview(long repositoryId, int pullRequestId, int reviewId)
        {
            return _client.GetReview(repositoryId, pullRequestId, reviewId).ToObservable();
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
        public IObservable<PullRequestReview> Submit(string owner, string name, int pullRequestId, int reviewId, PullRequestReviewSubmit submitMessage)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(submitMessage, "submitMessage");

            return _client.Submit(owner, name, pullRequestId, reviewId, submitMessage).ToObservable();
        }

        /// <summary>
        /// Submits an event to a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#submit-a-pull-request-review</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestId">The pull request review comment number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <param name="submitMessage">The message and event being submitted for the review</param>
        public IObservable<PullRequestReview> Submit(long repositoryId, int pullRequestId, int reviewId, PullRequestReviewSubmit submitMessage)
        {
            Ensure.ArgumentNotNull(submitMessage, "submitMessage");
            return _client.Submit(repositoryId, pullRequestId, reviewId, submitMessage).ToObservable();
        }

        /// <summary>
        /// Lists comments for a single review
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#get-comments-for-a-single-review</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestId">The pull request review comment number</param>
        /// <param name="reviewId">The pull request review number</param>
        public IObservable<PullRequestReviewComment> GetAllComments(string owner, string name, int pullRequestId, int reviewId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            return _connection.GetAndFlattenAllPages<PullRequestReviewComment>(ApiUrls.PullRequestReviewComments(owner, name, pullRequestId, reviewId));
        }

        /// <summary>
        /// Dismisses a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#get-comments-for-a-single-review</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestId">The pull request review comment number</param>
        /// <param name="reviewId">The pull request review number</param>
        public IObservable<PullRequestReviewComment> GetAllComments(long repositoryId, int pullRequestId, int reviewId)
        {
            return _connection.GetAndFlattenAllPages<PullRequestReviewComment>(ApiUrls.PullRequestReviewComments(repositoryId, pullRequestId, reviewId));

        }
    }
}
