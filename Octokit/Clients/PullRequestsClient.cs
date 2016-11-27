using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Pull Requests API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/activity/notifications/">Pull Requests API documentation</a> for more information.
    /// </remarks>
    public class PullRequestsClient : ApiClient, IPullRequestsClient
    {
        public PullRequestsClient(IApiConnection apiConnection) : base(apiConnection)
        {
            Comment = new PullRequestReviewCommentsClient(apiConnection);
        }

        /// <summary>
        /// Client for managing comments.
        /// </summary>
        public IPullRequestReviewCommentsClient Comment { get; private set; }

        /// <summary>
        /// Get a pull request by number.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/pulls/#get-a-single-pull-request
        /// </remarks>
        public Task<PullRequest> Get(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.Get<PullRequest>(ApiUrls.PullRequest(owner, name, number));
        }

        /// <summary>
        /// Get a pull request by number.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/pulls/#get-a-single-pull-request
        /// </remarks>
        public Task<PullRequest> Get(long repositoryId, int number)
        {
            return ApiConnection.Get<PullRequest>(ApiUrls.PullRequest(repositoryId, number));
        }

        /// <summary>
        /// Get all open pull requests for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/pulls/#list-pull-requests
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        public Task<IReadOnlyList<PullRequest>> GetAllForRepository(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return GetAllForRepository(owner, name, new PullRequestRequest(), ApiOptions.None);
        }

        /// <summary>
        /// Get all open pull requests for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/pulls/#list-pull-requests
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
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
        public Task<IReadOnlyList<PullRequest>> GetAllForRepository(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(options, "options");

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
        public Task<IReadOnlyList<PullRequest>> GetAllForRepository(long repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");

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
        public Task<IReadOnlyList<PullRequest>> GetAllForRepository(string owner, string name, PullRequestRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(request, "request");

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
        public Task<IReadOnlyList<PullRequest>> GetAllForRepository(long repositoryId, PullRequestRequest request)
        {
            Ensure.ArgumentNotNull(request, "request");

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
        public Task<IReadOnlyList<PullRequest>> GetAllForRepository(string owner, string name, PullRequestRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(request, "request");
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<PullRequest>(ApiUrls.PullRequests(owner, name),
                request.ToParametersDictionary(), options);
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
        public Task<IReadOnlyList<PullRequest>> GetAllForRepository(long repositoryId, PullRequestRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNull(request, "request");
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<PullRequest>(ApiUrls.PullRequests(repositoryId),
                request.ToParametersDictionary(), options);
        }

        /// <summary>
        /// Create a pull request for the specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/#create-a-pull-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="newPullRequest">A <see cref="NewPullRequest"/> instance describing the new PullRequest to create</param>
        public Task<PullRequest> Create(string owner, string name, NewPullRequest newPullRequest)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(newPullRequest, "newPullRequest");

            return ApiConnection.Post<PullRequest>(ApiUrls.PullRequests(owner, name), newPullRequest);
        }

        /// <summary>
        /// Create a pull request for the specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/#create-a-pull-request</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="newPullRequest">A <see cref="NewPullRequest"/> instance describing the new PullRequest to create</param>
        public Task<PullRequest> Create(long repositoryId, NewPullRequest newPullRequest)
        {
            Ensure.ArgumentNotNull(newPullRequest, "newPullRequest");

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
        public Task<PullRequest> Update(string owner, string name, int number, PullRequestUpdate pullRequestUpdate)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(pullRequestUpdate, "pullRequestUpdate");

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
        public Task<PullRequest> Update(long repositoryId, int number, PullRequestUpdate pullRequestUpdate)
        {
            Ensure.ArgumentNotNull(pullRequestUpdate, "pullRequestUpdate");

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
        public async Task<PullRequestMerge> Merge(string owner, string name, int number, MergePullRequest mergePullRequest)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(mergePullRequest, "mergePullRequest");

            try
            {
                var endpoint = ApiUrls.MergePullRequest(owner, name, number);
                return await ApiConnection.Put<PullRequestMerge>(endpoint, mergePullRequest, null, AcceptHeaders.SquashCommitPreview).ConfigureAwait(false);
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
        public async Task<PullRequestMerge> Merge(long repositoryId, int number, MergePullRequest mergePullRequest)
        {
            Ensure.ArgumentNotNull(mergePullRequest, "mergePullRequest");

            try
            {
                var endpoint = ApiUrls.MergePullRequest(repositoryId, number);
                return await ApiConnection.Put<PullRequestMerge>(endpoint, mergePullRequest, null, AcceptHeaders.SquashCommitPreview).ConfigureAwait(false);
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
        public async Task<bool> Merged(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

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
        public Task<IReadOnlyList<PullRequestCommit>> Commits(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.GetAll<PullRequestCommit>(ApiUrls.PullRequestCommits(owner, name, number));
        }

        /// <summary>
        /// Get the list of commits on a pull request.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/#list-commits-on-a-pull-request</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The pull request number</param>
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
        public Task<IReadOnlyList<PullRequestFile>> Files(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.GetAll<PullRequestFile>(ApiUrls.PullRequestFiles(owner, name, number));
        }

        /// <summary>
        /// Get the list of files on a pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/#list-pull-requests-files</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The pull request number</param>
        public Task<IReadOnlyList<PullRequestFile>> Files(long repositoryId, int number)
        {
            return ApiConnection.GetAll<PullRequestFile>(ApiUrls.PullRequestFiles(repositoryId, number));
        }
    }
}
