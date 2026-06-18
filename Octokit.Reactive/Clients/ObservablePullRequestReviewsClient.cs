using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using System.Threading;
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
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="review">The review</param>
        public IObservable<PullRequestReview> Create(string owner, string name, int pullRequestNumber, PullRequestReviewCreate review, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(review, nameof(review));
            return _client.Create(owner, name, pullRequestNumber, review, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Creates a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#create-a-pull-request-review</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="review">The review</param>
        public IObservable<PullRequestReview> Create(long repositoryId, int pullRequestNumber, PullRequestReviewCreate review, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(review, nameof(review));

            return _client.Create(repositoryId, pullRequestNumber, review, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Deletes a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#delete-a-pending-review</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        public IObservable<Unit> Delete(string owner, string name, int pullRequestNumber, long reviewId, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Delete(owner, name, pullRequestNumber, reviewId, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Deletes a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#delete-a-pending-review</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        public IObservable<Unit> Delete(long repositoryId, int pullRequestNumber, long reviewId, CancellationToken cancellationToken = default)
        {
            return _client.Delete(repositoryId, pullRequestNumber, reviewId, cancellationToken).ToObservable();
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
        public IObservable<PullRequestReview> Dismiss(string owner, string name, int pullRequestNumber, long reviewId, PullRequestReviewDismiss dismissMessage, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(dismissMessage, nameof(dismissMessage));

            return _client.Dismiss(owner, name, pullRequestNumber, reviewId, dismissMessage, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Dismisses a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#dismiss-a-pull-request-review</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <param name="dismissMessage">The message indicating why the review was dismissed</param>
        public IObservable<PullRequestReview> Dismiss(long repositoryId, int pullRequestNumber, long reviewId, PullRequestReviewDismiss dismissMessage, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(dismissMessage, nameof(dismissMessage));

            return _client.Dismiss(repositoryId, pullRequestNumber, reviewId, dismissMessage, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Gets reviews for a specified pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#list-reviews-on-a-pull-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        public IObservable<PullRequestReview> GetAll(string owner, string name, int pullRequestNumber, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAll(owner, name, pullRequestNumber, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets reviews for a specified pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#list-reviews-on-a-pull-request</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        public IObservable<PullRequestReview> GetAll(long repositoryId, int pullRequestNumber, CancellationToken cancellationToken = default)
        {
            return GetAll(repositoryId, pullRequestNumber, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets reviews for a specified pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#list-reviews-on-a-pull-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<PullRequestReview> GetAll(string owner, string name, int pullRequestNumber, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<PullRequestReview>(ApiUrls.PullRequestReviews(owner, name, pullRequestNumber), null, null, options, cancellationToken);
        }

        /// <summary>
        /// Gets reviews for a specified pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#list-reviews-on-a-pull-request</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<PullRequestReview> GetAll(long repositoryId, int pullRequestNumber, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<PullRequestReview>(ApiUrls.PullRequestReviews(repositoryId, pullRequestNumber), null, null, options, cancellationToken);
        }

        /// <summary>
        /// Gets a single pull request review by ID.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#get-a-single-review</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        public IObservable<PullRequestReview> Get(string owner, string name, int pullRequestNumber, long reviewId, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Get(owner, name, pullRequestNumber, reviewId, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Gets a single pull request review by ID.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#get-a-single-review</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        public IObservable<PullRequestReview> Get(long repositoryId, int pullRequestNumber, long reviewId, CancellationToken cancellationToken = default)
        {
            return _client.Get(repositoryId, pullRequestNumber, reviewId, cancellationToken).ToObservable();
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
        public IObservable<PullRequestReview> Submit(string owner, string name, int pullRequestNumber, long reviewId, PullRequestReviewSubmit submitMessage, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(submitMessage, nameof(submitMessage));

            return _client.Submit(owner, name, pullRequestNumber, reviewId, submitMessage, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Submits a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#submit-a-pull-request-review</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <param name="submitMessage">The message and event being submitted for the review</param>
        public IObservable<PullRequestReview> Submit(long repositoryId, int pullRequestNumber, long reviewId, PullRequestReviewSubmit submitMessage, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(submitMessage, nameof(submitMessage));

            return _client.Submit(repositoryId, pullRequestNumber, reviewId, submitMessage, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Lists comments for a single review
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#get-comments-for-a-single-review</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        public IObservable<PullRequestReviewComment> GetAllComments(string owner, string name, int pullRequestNumber, long reviewId, CancellationToken cancellationToken = default)
        {
            return GetAllComments(owner, name, pullRequestNumber, reviewId, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Dismisses a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#get-comments-for-a-single-review</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        public IObservable<PullRequestReviewComment> GetAllComments(long repositoryId, int pullRequestNumber, long reviewId, CancellationToken cancellationToken = default)
        {
            return GetAllComments(repositoryId, pullRequestNumber, reviewId, ApiOptions.None, cancellationToken);
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
        public IObservable<PullRequestReviewComment> GetAllComments(string owner, string name, int pullRequestNumber, long reviewId, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<PullRequestReviewComment>(ApiUrls.PullRequestReviewComments(owner, name, pullRequestNumber, reviewId), options, cancellationToken);
        }

        /// <summary>
        /// Dismisses a pull request review.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/reviews/#get-comments-for-a-single-review</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="reviewId">The pull request review number</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<PullRequestReviewComment> GetAllComments(long repositoryId, int pullRequestNumber, long reviewId, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<PullRequestReviewComment>(ApiUrls.PullRequestReviewComments(repositoryId, pullRequestNumber, reviewId), options, cancellationToken);
        }
    }
}
