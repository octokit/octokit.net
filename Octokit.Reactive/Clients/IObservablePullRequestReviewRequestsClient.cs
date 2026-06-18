using System;
using System.Reactive;
using System.Threading;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Pull Request Review Requests API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/pulls/review_requests/">Review Requests API documentation</a> for more information.
    /// </remarks>
    public interface IObservablePullRequestReviewRequestsClient
    {
        /// <summary>
        /// Gets review requests for a specified pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/review_requests/#list-review-requests</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<RequestedReviews> Get(string owner, string name, int pullRequestNumber, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets review requests for a specified pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/review_requests/#list-review-requests</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<RequestedReviews> Get(long repositoryId, int pullRequestNumber, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates review requests on a pull request for specified users.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/review_requests/#create-a-review-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="users">List of logins of user will be requested for review</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<PullRequest> Create(string owner, string name, int pullRequestNumber, PullRequestReviewRequest users, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates review requests on a pull request for specified users.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/review_requests/#create-a-review-request</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="users">List of logins of user will be requested for review</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<PullRequest> Create(long repositoryId, int pullRequestNumber, PullRequestReviewRequest users, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes review request for given users on a pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/review_requests/#delete-a-review-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="users">List of logins of users that will be not longer requested for review</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<Unit> Delete(string owner, string name, int pullRequestNumber, PullRequestReviewRequest users, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes review request for given users on a pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/review_requests/#delete-a-review-request</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="users">List of logins of users that will be not longer requested for review</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<Unit> Delete(long repositoryId, int pullRequestNumber, PullRequestReviewRequest users, CancellationToken cancellationToken = default);
    }
}
