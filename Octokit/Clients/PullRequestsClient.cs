using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Octokit
{
    public class PullRequestsClient : ApiClient, IPullRequestsClient
    {
        public PullRequestsClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        /// <summary>
        /// Gets a single Pull Request by number.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/pulls/#get-a-single-pull-request
        /// </remarks>
        /// <returns></returns>
        public Task<PullRequest> Get(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.Get<PullRequest>(ApiUrls.PullRequest(owner, name, number));
        }

        /// <summary>
        /// Gets all open pull requests for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/pulls/#list-pull-requests
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        public Task<IReadOnlyList<PullRequest>> GetForRepository(string owner, string name)
        {
            return GetForRepository(owner, name, new PullRequestRequest());
        }

        /// <summary>
        /// Gets all open pull requests for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/pulls/#list-pull-requests
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">Used to filter the list of Pull Requests returned</param>
        /// <returns></returns>
        public Task<IReadOnlyList<PullRequest>> GetForRepository(string owner, string name, PullRequestRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(request, "request");

            return ApiConnection.GetAll<PullRequest>(ApiUrls.PullRequests(owner, name),
                request.ToParametersDictionary());
        }

        /// <summary>
        /// Creates a Pull Request for the specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/#create-a-pull-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="newPullRequest">A <see cref="NewPullRequest"/> instance describing the new PullRequest to create</param>
        /// <returns></returns>
        public Task<PullRequest> Create(string owner, string name, NewPullRequest newPullRequest)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(newPullRequest, "newPullRequest");

            return ApiConnection.Post<PullRequest>(ApiUrls.PullRequests(owner, name), newPullRequest);
        }

        /// <summary>
        /// Creates a Pull Request for the specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/#update-a-pull-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The PullRequest number</param>
        /// <param name="pullRequestUpdate">An <see cref="PullRequestUpdate"/> instance describing the changes to make to the PullRequest
        /// </param>
        /// <returns></returns>
        public Task<PullRequest> Update(string owner, string name, int number, PullRequestUpdate pullRequestUpdate)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(pullRequestUpdate, "pullRequestUpdate");

            return ApiConnection.Patch<PullRequest>(ApiUrls.PullRequest(owner, name, number), pullRequestUpdate);
        }

        /// <summary>
        /// Merges a pull request.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/#merge-a-pull-request-merge-buttontrade</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <param name="mergePullRequest">A <see cref="MergePullRequest"/> instance describing a pull request merge</param>
        /// <returns></returns>
        public Task<PullRequestMerge> Merge(string owner, string name, int number, MergePullRequest mergePullRequest) 
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(mergePullRequest, "mergePullRequest");

            return ApiConnection.Put<PullRequestMerge>(ApiUrls.MergePullRequest(owner, name, number), mergePullRequest);
        }

        /// <summary>
        /// Gets the pull request merge status.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/#get-if-a-pull-request-has-been-merged</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <returns></returns>
        public async Task<bool> Merged(string owner, string name, int number) 
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            //return ApiConnection.Get<PullRequestMerge>(ApiUrls.MergePullRequest(owner, name, number));

            try
            {
                var response = await Connection.GetAsync<object>(ApiUrls.MergePullRequest(owner, name, number), null, null)
                                               .ConfigureAwait(false);
                if (response.StatusCode != HttpStatusCode.NotFound && 
                    response.StatusCode != HttpStatusCode.NoContent)
                {
                    throw new ApiException("Invalid Status Code returned. Expected a 204 or 404", response.StatusCode);
                }
                return response.StatusCode == HttpStatusCode.NoContent;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the list of commits on a pull request.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/#list-commits-on-a-pull-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <returns></returns>
        public Task<IReadOnlyList<Commit>> Commits(string owner, string name, int number) 
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.GetAll<Commit>(ApiUrls.PullRequestCommits(owner, name, number));
        }
    }
}
