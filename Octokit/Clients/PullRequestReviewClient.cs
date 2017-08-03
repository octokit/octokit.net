using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Pull Request Review Comments API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/pulls/comments/">Review Comments API documentation</a> for more information.
    /// </remarks>
    public class PullRequestReviewClient : ApiClient, IPullRequestReviewClient
    {
        public PullRequestReviewClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }
        
        public Task<IReadOnlyList<PullRequestReview>> GetAll(string owner, string name, int pullRequestId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return GetAll(owner, name, pullRequestId,  ApiOptions.None);
        }

        public Task<IReadOnlyList<PullRequestReview>> GetAll(long repositoryId, int pullRequestId)
        {
            return GetAll(repositoryId, pullRequestId, ApiOptions.None);
        }

        public Task<IReadOnlyList<PullRequestReview>> GetAll(string owner, string name, int pullRequestId, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(options, "options");
            var endpoint = ApiUrls.PullRequestReviews(owner, name, pullRequestId);
            return ApiConnection.GetAll<PullRequestReview>(endpoint, null, options);
        }

        public Task<IReadOnlyList<PullRequestReview>> GetAll(long repositoryId, int pullRequestId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<PullRequestReview>(ApiUrls.PullRequestReviews(repositoryId, pullRequestId), options);
        }

        public Task<PullRequestReview> GetReview(string owner, string name, int pullRequestId, int reviewId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.Get<PullRequestReview>(ApiUrls.PullRequestReview(owner, name, pullRequestId, reviewId));
        }

        public Task<PullRequestReview> GetReview(long repositoryId, int pullRequestId, int reviewId)
        {
            return ApiConnection.Get<PullRequestReview>(ApiUrls.PullRequestReview(repositoryId, pullRequestId, reviewId));
        }

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

        public Task Delete(string owner, string name, int pullRequestId, int reviewId)
        {
            Ensure.ArgumentNotNull(owner, "owner");
            Ensure.ArgumentNotNull(name, "name");

            var endpoint = ApiUrls.PullRequestReview(owner, name, pullRequestId, reviewId);
            return ApiConnection.Connection.Delete(endpoint);
        }

        public Task Delete(long repositoryId, int pullRequestId, int reviewId)
        {
            var endpoint = ApiUrls.PullRequestReview(repositoryId, pullRequestId, reviewId);
            return ApiConnection.Connection.Delete(endpoint);
        }

        public async Task<PullRequestReview> Dismiss(string owner, string name, int pullRequestId, int reviewId, PullRequestReviewDismiss dismissMessage)
        {
            var endpoint = ApiUrls.PullRequestReviewDismissal(owner, name, pullRequestId, reviewId);
            var response = await ApiConnection.Connection.Put<PullRequestReview>(endpoint, dismissMessage);
            if (response.HttpResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new ApiException("Invalid Status Code returned. Expected a 200", response.HttpResponse.StatusCode);
            }
            return response.Body;
        }

        public async Task<PullRequestReview> Dismiss(long repositoryId, int pullRequestId, int reviewId, PullRequestReviewDismiss dismissMessage)
        {
            var endpoint = ApiUrls.PullRequestReviewDismissal(repositoryId, pullRequestId, reviewId);
            var response = await ApiConnection.Connection.Put<PullRequestReview>(endpoint, dismissMessage);
            if (response.HttpResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new ApiException("Invalid Status Code returned. Expected a 200", response.HttpResponse.StatusCode);
            }
            return response.Body;
        }

        public async Task<PullRequestReview> Submit(string owner, string name, int pullRequestId, int reviewId, PullRequestReviewSubmit submitMessage)
        {
            var endpoint = ApiUrls.PullRequestReviewDismissal(owner, name, pullRequestId, reviewId);
            var response = await ApiConnection.Connection.Post<PullRequestReview>(endpoint, submitMessage, null, null);
            if (response.HttpResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new ApiException("Invalid Status Code returned. Expected a 200", response.HttpResponse.StatusCode);
            }
            return response.Body;
        }

        public async Task<PullRequestReview> Submit(long repositoryId, int pullRequestId, int reviewId, PullRequestReviewSubmit submitMessage)
        {
            var endpoint = ApiUrls.PullRequestReviewDismissal(repositoryId, pullRequestId, reviewId);
            var response = await ApiConnection.Connection.Post<PullRequestReview>(endpoint, submitMessage, null, null);
            if (response.HttpResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new ApiException("Invalid Status Code returned. Expected a 200", response.HttpResponse.StatusCode);
            }
            return response.Body;
        }
    }
}
