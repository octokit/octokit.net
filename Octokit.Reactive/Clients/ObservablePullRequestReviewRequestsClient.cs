using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservablePullRequestReviewRequestsClient : IObservablePullRequestReviewRequestsClient
    {
        readonly IPullRequestReviewRequestsClient _client;
        readonly IConnection _connection;

        public ObservablePullRequestReviewRequestsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.PullRequest.ReviewRequest;
            _connection = client.Connection;
        }

        /// <summary>
        /// Gets review requests for a specified pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/review_requests/#list-review-requests</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        public IObservable<RequestedReviews> Get(string owner, string name, int pullRequestNumber)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Get(owner, name, pullRequestNumber).ToObservable();
        }

        /// <summary>
        /// Gets review requests for a specified pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/review_requests/#list-review-requests</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        public IObservable<RequestedReviews> Get(long repositoryId, int pullRequestNumber)
        {
            return _client.Get(repositoryId, pullRequestNumber).ToObservable();
        }

        /// <summary>
        /// Creates review requests on a pull request for specified users.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/review_requests/#create-a-review-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="users">List of logins of user will be requested for review</param>
        public IObservable<PullRequest> Create(string owner, string name, int pullRequestNumber, PullRequestReviewRequest users)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(users, nameof(users));

            return _client.Create(owner, name, pullRequestNumber, users).ToObservable();
        }

        /// <summary>
        /// Creates review requests on a pull request for specified users.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/review_requests/#create-a-review-request</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="users">List of logins of user will be requested for review</param>
        public IObservable<PullRequest> Create(long repositoryId, int pullRequestNumber, PullRequestReviewRequest users)
        {
            Ensure.ArgumentNotNull(users, nameof(users));

            return _client.Create(repositoryId, pullRequestNumber, users).ToObservable();
        }

        /// <summary>
        /// Deletes review request for given users on a pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/review_requests/#delete-a-review-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="users">List of logins of users that will be not longer requested for review</param>
        public IObservable<Unit> Delete(string owner, string name, int pullRequestNumber, PullRequestReviewRequest users)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(users, nameof(users));

            return _client.Delete(owner, name, pullRequestNumber, users).ToObservable();
        }

        /// <summary>
        /// Deletes review request for given users on a pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/review_requests/#delete-a-review-request</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="users">List of logins of users that will be not longer requested for review</param>
        public IObservable<Unit> Delete(long repositoryId, int pullRequestNumber, PullRequestReviewRequest users)
        {
            Ensure.ArgumentNotNull(users, nameof(users));

            return _client.Delete(repositoryId, pullRequestNumber, users).ToObservable();
        }
    }
}
