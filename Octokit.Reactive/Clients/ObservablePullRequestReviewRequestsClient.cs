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
            Ensure.ArgumentNotNull(client, "client");

            _client = client.PullRequest.ReviewRequest;
            _connection = client.Connection;
        }

        /// <summary>
        /// Gets review requests for a specified pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/review_requests/#list-review-requests</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request number</param>
        public IObservable<User> GetAll(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _connection.GetAndFlattenAllPages<User>(ApiUrls.PullRequestReviewRequests(owner, name, number), null, AcceptHeaders.PullRequestReviewsApiPreview);
        }

        /// <summary>
        /// Gets review requests for a specified pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/review_requests/#list-review-requests</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<User> GetAll(string owner, string name, int number, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(options, "options");

            return _connection.GetAndFlattenAllPages<User>(ApiUrls.PullRequestReviewRequests(owner, name, number), null, AcceptHeaders.PullRequestReviewsApiPreview, options);
        }

        /// <summary>
        /// Gets review requests for a specified pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/review_requests/#list-review-requests</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The pull request number</param>
        public IObservable<User> GetAll(long repositoryId, int number)
        {
            return _connection.GetAndFlattenAllPages<User>(ApiUrls.PullRequestReviewRequests(repositoryId, number), null, AcceptHeaders.PullRequestReviewsApiPreview);
        }

        /// <summary>
        /// Gets review requests for a specified pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/review_requests/#list-review-requests</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<User> GetAll(long repositoryId, int number, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");

            return _connection.GetAndFlattenAllPages<User>(ApiUrls.PullRequestReviewRequests(repositoryId, number), null, AcceptHeaders.PullRequestReviewsApiPreview, options);
        }

        /// <summary>
        /// Creates review requests on a pull request for specified users.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/review_requests/#create-a-review-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The Pull Request number</param>
        /// <param name="users">List of logins of user will be requested for review</param>
        public IObservable<PullRequest> Create(string owner, string name, int number, PullRequestReviewRequest users)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(users, "users");

            return _client.Create(owner, name, number, users).ToObservable();
        }

        /// <summary>
        /// Creates review requests on a pull request for specified users.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/review_requests/#create-a-review-request</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The Pull Request number</param>
        /// <param name="users">List of logins of user will be requested for review</param>
        public IObservable<PullRequest> Create(long repositoryId, int number, PullRequestReviewRequest users)
        {
            Ensure.ArgumentNotNull(users, "users");

            return _client.Create(repositoryId, number, users).ToObservable();
        }

        /// <summary>
        /// Deletes review request for given users on a pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/review_requests/#delete-a-review-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request review comment number</param>
        /// <param name="users">List of logins of users that will be not longer requested for review</param>
        public IObservable<Unit> Delete(string owner, string name, int number, PullRequestReviewRequest users)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(users, "users");

            return _client.Delete(owner, name, number, users).ToObservable();
        }

        /// <summary>
        /// Deletes review request for given users on a pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/review_requests/#delete-a-review-request</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The pull request review comment number</param>
        /// <param name="users">List of logins of users that will be not longer requested for review</param>
        public IObservable<Unit> Delete(long repositoryId, int number, PullRequestReviewRequest users)
        {
            Ensure.ArgumentNotNull(users, "users");

            return _client.Delete(repositoryId, number, users).ToObservable();
        }
    }
}