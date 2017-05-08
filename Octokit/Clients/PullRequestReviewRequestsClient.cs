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
        /// <param name="number">The pull request number</param>
        public Task<IReadOnlyList<User>> GetAll(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.GetAll<User>(ApiUrls.PullRequestReviewRequests(owner, name, number), null, AcceptHeaders.PullRequestReviewsApiPreview);
        }

        /// <summary>
        /// Gets review requests for a specified pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/review_requests/#list-review-requests</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <param name="options">Options for changing the API response</param>
        public Task<IReadOnlyList<User>> GetAll(string owner, string name, int number, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<User>(ApiUrls.PullRequestReviewRequests(owner, name, number), null, AcceptHeaders.PullRequestReviewsApiPreview, options);
        }

        /// <summary>
        /// Gets review requests for a specified pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/review_requests/#list-review-requests</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The pull request number</param>
        public Task<IReadOnlyList<User>> GetAll(long repositoryId, int number)
        {
            return ApiConnection.GetAll<User>(ApiUrls.PullRequestReviewRequests(repositoryId, number), null, AcceptHeaders.PullRequestReviewsApiPreview);
        }

        /// <summary>
        /// Gets review requests for a specified pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/review_requests/#list-review-requests</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <param name="options">Options for changing the API response</param>
        public Task<IReadOnlyList<User>> GetAll(long repositoryId, int number, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<User>(ApiUrls.PullRequestReviewRequests(repositoryId, number), null, AcceptHeaders.PullRequestReviewsApiPreview, options);
        }

        /// <summary>
        /// Creates review requests on a pull request for specified users.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/review_requests/#create-a-review-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The Pull Request number</param>
        /// <param name="users">List of logins of user will be requested for review</param>
        public async Task<PullRequest> Create(string owner, string name, int number, PullRequestReviewRequest users)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(users, "users");

            var endpoint = ApiUrls.PullRequestReviewRequests(owner, name, number);
            var response = await ApiConnection.Connection.Post<PullRequest>(endpoint, users, AcceptHeaders.PullRequestReviewsApiPreview, null).ConfigureAwait(false);

            if (response.HttpResponse.StatusCode != HttpStatusCode.Created)
            {
                throw new ApiException("Invalid Status Code returned. Expected a 201", response.HttpResponse.StatusCode);
            }

            return response.Body;
        }

        /// <summary>
        /// Creates review requests on a pull request for specified users.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/review_requests/#create-a-review-request</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The Pull Request number</param>
        /// <param name="users">List of logins of user will be requested for review</param>
        public async Task<PullRequest> Create(long repositoryId, int number, PullRequestReviewRequest users)
        {
            Ensure.ArgumentNotNull(users, "users");

            var endpoint = ApiUrls.PullRequestReviewRequests(repositoryId, number);
            var response = await ApiConnection.Connection.Post<PullRequest>(endpoint, users, AcceptHeaders.PullRequestReviewsApiPreview, null).ConfigureAwait(false);

            if (response.HttpResponse.StatusCode != HttpStatusCode.Created)
            {
                throw new ApiException("Invalid Status Code returned. Expected a 201", response.HttpResponse.StatusCode);
            }

            return response.Body;
        }

        /// <summary>
        /// Deletes review request for given users on a pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/review_requests/#delete-a-review-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request review comment number</param>
        /// <param name="users">List of logins of users that will be not longer requested for review</param>
        public Task Delete(string owner, string name, int number, PullRequestReviewRequest users)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(users, "users");

            return ApiConnection.Delete(ApiUrls.PullRequestReviewRequests(owner, name, number), users, AcceptHeaders.PullRequestReviewsApiPreview);
        }

        /// <summary>
        /// Deletes review request for given users on a pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/review_requests/#delete-a-review-request</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The pull request review comment number</param>
        /// <param name="users">List of logins of users that will be not longer requested for review</param>
        public Task Delete(long repositoryId, int number, PullRequestReviewRequest users)
        {
            Ensure.ArgumentNotNull(users, "users");

            return ApiConnection.Delete(ApiUrls.PullRequestReviewRequests(repositoryId, number), users, AcceptHeaders.PullRequestReviewsApiPreview);
        }
    }
}