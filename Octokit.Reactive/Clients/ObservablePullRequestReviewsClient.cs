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
    public class ObservablePullRequestReviewsClient : IObservablePullRequestReviewsClient
    {
        readonly IPullRequestReviewsClient _client;
        readonly IConnection _connection;

        public ObservablePullRequestReviewsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.PullRequest.Review;
            _connection = client.Connection;
        }

        /// <summary>
        /// Creates a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#create-a-pull-request-review</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The Pull Request number</param>
        /// <param name="review">The review</param>
        public IObservable<PullRequestReview> Create(string owner, string name, int number, PullRequestReviewCreate review)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(review, nameof(review));
            return _client.Create(owner, name, number, review).ToObservable();
        }

        /// <summary>
        /// Creates a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#create-a-pull-request-review</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The Pull Request number</param>
        /// <param name="review">The review</param>
        public IObservable<PullRequestReview> Create(long repositoryId, int number, PullRequestReviewCreate review)
        {
            Ensure.ArgumentNotNull(review, nameof(review));

            return _client.Create(repositoryId, number, review).ToObservable();
        }

        /// <summary>
        /// Deletes a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#delete-a-pending-review</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        public IObservable<Unit> Delete(string owner, string name, int number, long reviewId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Delete(owner, name, number, reviewId).ToObservable();
        }

        /// <summary>
        /// Deletes a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#delete-a-pending-review</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        public IObservable<Unit> Delete(long repositoryId, int number, long reviewId)
        {
            return _client.Delete(repositoryId, number, reviewId).ToObservable();
        }

        /// <summary>
        /// Dismisses a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#dismiss-a-pull-request-review</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <param name="dismissMessage">The message indicating why the review was dismissed</param>
        public IObservable<PullRequestReview> Dismiss(string owner, string name, int number, long reviewId, PullRequestReviewDismiss dismissMessage)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(dismissMessage, nameof(dismissMessage));

            return _client.Dismiss(owner, name, number, reviewId, dismissMessage).ToObservable();
        }

        /// <summary>
        /// Dismisses a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#dismiss-a-pull-request-review</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <param name="dismissMessage">The message indicating why the review was dismissed</param>
        public IObservable<PullRequestReview> Dismiss(long repositoryId, int number, long reviewId, PullRequestReviewDismiss dismissMessage)
        {
            Ensure.ArgumentNotNull(dismissMessage, nameof(dismissMessage));

            return GetAll(repositoryId, number);
        }

        /// <summary>
        /// Gets reviews for a specified pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#list-reviews-on-a-pull-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request number</param>
        public IObservable<PullRequestReview> GetAll(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAll(owner, name, number, ApiOptions.None);
        }

        /// <summary>
        /// Gets reviews for a specified pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#list-reviews-on-a-pull-request</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The pull request number</param>
        public IObservable<PullRequestReview> GetAll(long repositoryId, int number)
        {
            return GetAll(repositoryId, number, ApiOptions.None);
        }

        /// <summary>
        /// Gets reviews for a specified pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#list-reviews-on-a-pull-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<PullRequestReview> GetAll(string owner, string name, int number, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<PullRequestReview>(ApiUrls.PullRequestReviews(owner, name, number), null, null, options);
        }

        /// <summary>
        /// Gets reviews for a specified pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#list-reviews-on-a-pull-request</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<PullRequestReview> GetAll(long repositoryId, int number, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<PullRequestReview>(ApiUrls.PullRequestReviews(repositoryId, number), null, null, options);
        }

        /// <summary>
        /// Gets a single pull request review by ID.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#get-a-single-review</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        public IObservable<PullRequestReview> Get(string owner, string name, int number, long reviewId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Get(owner, name, number, reviewId).ToObservable();
        }

        /// <summary>
        /// Gets a single pull request review by ID.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#get-a-single-review</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        public IObservable<PullRequestReview> Get(long repositoryId, int number, long reviewId)
        {
            return _client.Get(repositoryId, number, reviewId).ToObservable();
        }

        /// <summary>
        /// Submits a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#submit-a-pull-request-review</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <param name="submitMessage">The message and event being submitted for the review</param>
        public IObservable<PullRequestReview> Submit(string owner, string name, int number, long reviewId, PullRequestReviewSubmit submitMessage)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(submitMessage, nameof(submitMessage));

            return _client.Submit(owner, name, number, reviewId, submitMessage).ToObservable();
        }

        /// <summary>
        /// Submits a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#submit-a-pull-request-review</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <param name="submitMessage">The message and event being submitted for the review</param>
        public IObservable<PullRequestReview> Submit(long repositoryId, int number, long reviewId, PullRequestReviewSubmit submitMessage)
        {
            Ensure.ArgumentNotNull(submitMessage, nameof(submitMessage));

            return _client.Submit(repositoryId, number, reviewId, submitMessage).ToObservable();
        }

        /// <summary>
        /// Lists comments for a single review
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#get-comments-for-a-single-review</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        public IObservable<PullRequestReviewComment> GetAllComments(string owner, string name, int number, long reviewId)
        {
            return GetAllComments(owner, name, number, reviewId, ApiOptions.None);
        }

        /// <summary>
        /// Dismisses a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#get-comments-for-a-single-review</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        public IObservable<PullRequestReviewComment> GetAllComments(long repositoryId, int number, long reviewId)
        {
            return GetAllComments(repositoryId, number, reviewId, ApiOptions.None);
        }

        /// <summary>
        /// Lists comments for a single review
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#get-comments-for-a-single-review</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<PullRequestReviewComment> GetAllComments(string owner, string name, int number, long reviewId, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<PullRequestReviewComment>(ApiUrls.PullRequestReviewComments(owner, name, number, reviewId), options);
        }

        /// <summary>
        /// Dismisses a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#get-comments-for-a-single-review</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<PullRequestReviewComment> GetAllComments(long repositoryId, int number, long reviewId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<PullRequestReviewComment>(ApiUrls.PullRequestReviewComments(repositoryId, number, reviewId), options);
        }
    }
}
