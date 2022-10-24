using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Pull Requests API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/pulls/">Pull Requests API documentation</a> for more information.
    /// </remarks>
    public class PullRequestsClient : ApiClient, IPullRequestsClient
    {
        public PullRequestsClient(IApiConnection apiConnection) : base(apiConnection)
        {
            Review = new PullRequestReviewsClient(apiConnection);
            ReviewComment = new PullRequestReviewCommentsClient(apiConnection);
            ReviewRequest = new PullRequestReviewRequestsClient(apiConnection);
            LockUnlock = new LockUnlockClient(apiConnection);
        }

        /// <summary>
        /// Client for managing reviews.
        /// </summary>
        public IPullRequestReviewsClient Review { get; set; }

        /// <summary>
        /// Client for managing review comments.
        /// </summary>
        public IPullRequestReviewCommentsClient ReviewComment { get; set; }

        /// <summary>
        /// Client for managing review requests.
        /// </summary>
        public IPullRequestReviewRequestsClient ReviewRequest { get; set; }

        /// <summary>
        /// Client for locking/unlocking a coversation on a pull request
        /// </summary>
        public ILockUnlockClient LockUnlock { get; set; }

        /// <summary>
        /// Get a pull request by number.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/pulls/#get-a-single-pull-request
        /// </remarks>
        [ManualRoute("GET", "/repos/{owner}/{repo}/pulls/{pull_number}")]
        public Task<PullRequest> Get(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Get<PullRequest>(ApiUrls.PullRequest(owner, name, number), null);
        }

        /// <summary>
        /// Get a pull request by number.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/pulls/#get-a-single-pull-request
        /// </remarks>
        [ManualRoute("GET", "/repositories/{id}/pulls/{number}")]
        public Task<PullRequest> Get(long repositoryId, int number)
        {
            return ApiConnection.Get<PullRequest>(ApiUrls.PullRequest(repositoryId, number), null);
        }

        /// <summary>
        /// Get all open pull requests for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/pulls/#list-pull-requests
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/pulls")]
        public Task<IReadOnlyList<PullRequest>> GetAllForRepository(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllForRepository(owner, name, new PullRequestRequest(), ApiOptions.None);
        }

        /// <summary>
        /// Get all open pull requests for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/pulls/#list-pull-requests
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        [ManualRoute("GET", "/repositories/{id}/pulls")]
        public Task<IReadOnlyList<PullRequest>> GetAllForRepository(long repositoryId)
        {
            return GetAllForRepository(repositoryId, new PullRequestRequest(), ApiOptions.None);
        }

        /// <summary>
        /// Get all open pull requests for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/pulls/#list-pull-requests
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/pulls")]
        public Task<IReadOnlyList<PullRequest>> GetAllForRepository(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return GetAllForRepository(owner, name, new PullRequestRequest(), options);
        }

        /// <summary>
        /// Get all open pull requests for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/pulls/#list-pull-requests
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/repositories/{id}/pulls")]
        public Task<IReadOnlyList<PullRequest>> GetAllForRepository(long repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return GetAllForRepository(repositoryId, new PullRequestRequest(), options);
        }

        /// <summary>
        /// Query pull requests for the repository based on criteria
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/pulls/#list-pull-requests
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">Used to filter and sort the list of pull requests returned</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/pulls")]
        public Task<IReadOnlyList<PullRequest>> GetAllForRepository(string owner, string name, PullRequestRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAllForRepository(owner, name, request, ApiOptions.None);
        }

        /// <summary>
        /// Query pull requests for the repository based on criteria
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/pulls/#list-pull-requests
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">Used to filter and sort the list of pull requests returned</param>
        [ManualRoute("GET", "/repositories/{id}/pulls")]
        public Task<IReadOnlyList<PullRequest>> GetAllForRepository(long repositoryId, PullRequestRequest request)
        {
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAllForRepository(repositoryId, request, ApiOptions.None);
        }

        /// <summary>
        /// Query pull requests for the repository based on criteria
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/pulls/#list-pull-requests
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">Used to filter and sort the list of pull requests returned</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/pulls")]
        public Task<IReadOnlyList<PullRequest>> GetAllForRepository(string owner, string name, PullRequestRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<PullRequest>(ApiUrls.PullRequests(owner, name), request.ToParametersDictionary(), options);
        }

        /// <summary>
        /// Query pull requests for the repository based on criteria
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/pulls/#list-pull-requests
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">Used to filter and sort the list of pull requests returned</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/repositories/{id}/pulls")]
        public Task<IReadOnlyList<PullRequest>> GetAllForRepository(long repositoryId, PullRequestRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<PullRequest>(ApiUrls.PullRequests(repositoryId), request.ToParametersDictionary(), options);
        }

        /// <summary>
        /// Create a pull request for the specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/#create-a-pull-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="newPullRequest">A <see cref="NewPullRequest"/> instance describing the new PullRequest to create</param>
        [ManualRoute("POST", "/repos/{owner}/{repo}/pulls")]
        public Task<PullRequest> Create(string owner, string name, NewPullRequest newPullRequest)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(newPullRequest, nameof(newPullRequest));

            return ApiConnection.Post<PullRequest>(ApiUrls.PullRequests(owner, name), newPullRequest);
        }

        /// <summary>
        /// Create a pull request for the specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/#create-a-pull-request</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="newPullRequest">A <see cref="NewPullRequest"/> instance describing the new PullRequest to create</param>
        [ManualRoute("POST", "/repositories/{id}/pulls")]
        public Task<PullRequest> Create(long repositoryId, NewPullRequest newPullRequest)
        {
            Ensure.ArgumentNotNull(newPullRequest, nameof(newPullRequest));

            return ApiConnection.Post<PullRequest>(ApiUrls.PullRequests(repositoryId), newPullRequest);
        }

        /// <summary>
        /// Create a pull request for the specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/#update-a-pull-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The PullRequest number</param>
        /// <param name="pullRequestUpdate">An <see cref="PullRequestUpdate"/> instance describing the changes to make to the PullRequest
        /// </param>
        [ManualRoute("PATCH", "/repos/{owner}/{repo}/pulls/{pull_number}")]
        public Task<PullRequest> Update(string owner, string name, int number, PullRequestUpdate pullRequestUpdate)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(pullRequestUpdate, nameof(pullRequestUpdate));

            return ApiConnection.Patch<PullRequest>(ApiUrls.PullRequest(owner, name, number), pullRequestUpdate);
        }

        /// <summary>
        /// Create a pull request for the specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/#update-a-pull-request</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The PullRequest number</param>
        /// <param name="pullRequestUpdate">An <see cref="PullRequestUpdate"/> instance describing the changes to make to the PullRequest
        /// </param>
        [ManualRoute("PATCH", "/repositories/{id}/pulls/{number}")]
        public Task<PullRequest> Update(long repositoryId, int number, PullRequestUpdate pullRequestUpdate)
        {
            Ensure.ArgumentNotNull(pullRequestUpdate, nameof(pullRequestUpdate));

            return ApiConnection.Patch<PullRequest>(ApiUrls.PullRequest(repositoryId, number), pullRequestUpdate);
        }

        /// <summary>
        /// Merge a pull request.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/#merge-a-pull-request-merge-buttontrade</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <param name="mergePullRequest">A <see cref="MergePullRequest"/> instance describing a pull request merge</param>
        [ManualRoute("PUT", "/repos/{owner}/{repo}/pulls/{pull_number}/merge")]
        public async Task<PullRequestMerge> Merge(string owner, string name, int number, MergePullRequest mergePullRequest)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(mergePullRequest, nameof(mergePullRequest));

            try
            {
                var endpoint = ApiUrls.MergePullRequest(owner, name, number);
                return await ApiConnection.Put<PullRequestMerge>(endpoint, mergePullRequest).ConfigureAwait(false);
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == HttpStatusCode.MethodNotAllowed)
                {
                    throw new PullRequestNotMergeableException(ex.HttpResponse);
                }

                if (ex.StatusCode == HttpStatusCode.Conflict)
                {
                    throw new PullRequestMismatchException(ex.HttpResponse);
                }

                throw;
            }
        }

        /// <summary>
        /// Merge a pull request.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/#merge-a-pull-request-merge-buttontrade</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <param name="mergePullRequest">A <see cref="MergePullRequest"/> instance describing a pull request merge</param>
        [ManualRoute("PUT", "/repositories/{id}/pulls/{number}/merge")]
        public async Task<PullRequestMerge> Merge(long repositoryId, int number, MergePullRequest mergePullRequest)
        {
            Ensure.ArgumentNotNull(mergePullRequest, nameof(mergePullRequest));

            try
            {
                var endpoint = ApiUrls.MergePullRequest(repositoryId, number);
                return await ApiConnection.Put<PullRequestMerge>(endpoint, mergePullRequest).ConfigureAwait(false);
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == HttpStatusCode.MethodNotAllowed)
                {
                    throw new PullRequestNotMergeableException(ex.HttpResponse);
                }

                if (ex.StatusCode == HttpStatusCode.Conflict)
                {
                    throw new PullRequestMismatchException(ex.HttpResponse);
                }

                throw;
            }
        }

        /// <summary>
        /// Get the pull request merge status.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/#get-if-a-pull-request-has-been-merged</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request number</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/pulls/{pull_number}/merge")]
        public async Task<bool> Merged(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            try
            {
                var endpoint = ApiUrls.MergePullRequest(owner, name, number);
                var response = await Connection.Get<object>(endpoint, null, null).ConfigureAwait(false);
                return response.HttpResponse.IsTrue();
            }
            catch (NotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Get the pull request merge status.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/#get-if-a-pull-request-has-been-merged</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The pull request number</param>
        [ManualRoute("GET", "/repositories/{id}/pulls/{number}/merge")]
        public async Task<bool> Merged(long repositoryId, int number)
        {
            try
            {
                var endpoint = ApiUrls.MergePullRequest(repositoryId, number);
                var response = await Connection.Get<object>(endpoint, null, null).ConfigureAwait(false);
                return response.HttpResponse.IsTrue();
            }
            catch (NotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Get the list of commits on a pull request.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/#list-commits-on-a-pull-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request number</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/pulls/{pull_number}/commits")]
        public Task<IReadOnlyList<PullRequestCommit>> Commits(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.GetAll<PullRequestCommit>(ApiUrls.PullRequestCommits(owner, name, number));
        }

        /// <summary>
        /// Get the list of commits on a pull request.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/#list-commits-on-a-pull-request</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The pull request number</param>
        [ManualRoute("GET", "/repositories/{id}/pulls/{number}/commits")]
        public Task<IReadOnlyList<PullRequestCommit>> Commits(long repositoryId, int number)
        {
            return ApiConnection.GetAll<PullRequestCommit>(ApiUrls.PullRequestCommits(repositoryId, number));
        }

        /// <summary>
        /// Get the list of files on a pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/#list-pull-requests-files</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request number</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/pulls/{pull_number}/files")]
        public Task<IReadOnlyList<PullRequestFile>> Files(string owner, string name, int number)
        {
            return Files(owner, name, number, ApiOptions.None);
        }

        /// <summary>
        /// Get the list of files on a pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/#list-pull-requests-files</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/pulls/{pull_number}/files")]
        public Task<IReadOnlyList<PullRequestFile>> Files(string owner, string name, int number, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.GetAll<PullRequestFile>(ApiUrls.PullRequestFiles(owner, name, number), options);
        }

        /// <summary>
        /// Get the list of files on a pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/#list-pull-requests-files</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The pull request number</param>
        [ManualRoute("GET", "/repositories/{id}/pulls/{number}/files")]
        public Task<IReadOnlyList<PullRequestFile>> Files(long repositoryId, int number)
        {
            return Files(repositoryId, number, ApiOptions.None);
        }

        /// <summary>
        /// Get the list of files on a pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/#list-pull-requests-files</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/repositories/{id}/pulls/{number}/files")]
        public Task<IReadOnlyList<PullRequestFile>> Files(long repositoryId, int number, ApiOptions options)
        {
            return ApiConnection.GetAll<PullRequestFile>(ApiUrls.PullRequestFiles(repositoryId, number), options);
        }
    }
}
