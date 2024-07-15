using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Pull Request Review Requests API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/pulls/review_requests/">Review Requests API documentation</a> for more information.
    /// </remarks>
    public class PullRequestReviewRequestsClient : ApiClient, IPullRequestReviewRequestsClient
    {
        public PullRequestReviewRequestsClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        /// <summary>
        /// Gets review requests for a specified pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/review_requests/#list-review-requests</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/pulls/{pull_number}/requested_reviewers")]
        public Task<RequestedReviews> Get(string owner, string name, int pullRequestNumber)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Get<RequestedReviews>(ApiUrls.PullRequestReviewRequests(owner, name, pullRequestNumber));
        }

        /// <summary>
        /// Gets review requests for a specified pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/review_requests/#list-review-requests</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        [ManualRoute("GET", "/repositories/{id}/pulls/{number}/requested_reviewers")]
        public Task<RequestedReviews> Get(long repositoryId, int pullRequestNumber)
        {
            return ApiConnection.Get<RequestedReviews>(ApiUrls.PullRequestReviewRequests(repositoryId, pullRequestNumber));
        }

        /// <summary>
        /// Creates review requests on a pull request for specified users.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/review_requests/#create-a-review-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="users">List of logins of user will be requested for review</param>
        [ManualRoute("POST", "/repos/{owner}/{repo}/pulls/{pull_number}/requested_reviewers")]
        public Task<PullRequest> Create(string owner, string name, int pullRequestNumber, PullRequestReviewRequest users)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(users, nameof(users));

            var endpoint = ApiUrls.PullRequestReviewRequests(owner, name, pullRequestNumber);
            return ApiConnection.Post<PullRequest>(endpoint, users);
        }

        /// <summary>
        /// Creates review requests on a pull request for specified users.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/review_requests/#create-a-review-request</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="users">List of logins of user will be requested for review</param>
        [ManualRoute("POST", "/repositories/{id}/pulls/{number}/requested_reviewers")]
        public Task<PullRequest> Create(long repositoryId, int pullRequestNumber, PullRequestReviewRequest users)
        {
            Ensure.ArgumentNotNull(users, nameof(users));

            var endpoint = ApiUrls.PullRequestReviewRequests(repositoryId, pullRequestNumber);
            return ApiConnection.Post<PullRequest>(endpoint, users);
        }

        /// <summary>
        /// Deletes review request for given users on a pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/review_requests/#delete-a-review-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="users">List of logins of users that will be not longer requested for review</param>
        [ManualRoute("DELETE", "/repos/{owner}/{repo}/pulls/{pull_number}/requested_reviewers")]
        public Task Delete(string owner, string name, int pullRequestNumber, PullRequestReviewRequest users)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(users, nameof(users));

            return ApiConnection.Delete(ApiUrls.PullRequestReviewRequests(owner, name, pullRequestNumber), users);
        }

        /// <summary>
        /// Deletes review request for given users on a pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/review_requests/#delete-a-review-request</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="users">List of logins of users that will be not longer requested for review</param>
        [ManualRoute("DELETE", "/repositories/{id}/pulls/{number}/requested_reviewers")]
        public Task Delete(long repositoryId, int pullRequestNumber, PullRequestReviewRequest users)
        {
            Ensure.ArgumentNotNull(users, nameof(users));

            return ApiConnection.Delete(ApiUrls.PullRequestReviewRequests(repositoryId, pullRequestNumber), users);
        }
    }
}
