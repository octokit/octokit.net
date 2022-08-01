using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Check Runs API
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/checks/runs/">Check Runs API documentation</a> for more information.
    /// </remarks>
    public class CheckRunsClient : ApiClient, ICheckRunsClient
    {
        /// <summary>
        /// Initializes a new GitHub Check Runs API client
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public CheckRunsClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        /// <summary>
        /// Creates a new check run for a specific commit in a repository
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/#create-a-check-run">Check Runs API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="newCheckRun">Details of the Check Run to create</param>
        [ManualRoute("POST", "/repos/{owner}/{repo}/check-runs")]
        public Task<CheckRun> Create(string owner, string name, NewCheckRun newCheckRun)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(newCheckRun, nameof(newCheckRun));

            return ApiConnection.Post<CheckRun>(ApiUrls.CheckRuns(owner, name), newCheckRun);
        }

        /// <summary>
        /// Creates a new check run for a specific commit in a repository
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/#create-a-check-run">Check Runs API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="newCheckRun">Details of the Check Run to create</param>
        [ManualRoute("POST", "/repositories/{id}/check-runs")]
        public Task<CheckRun> Create(long repositoryId, NewCheckRun newCheckRun)
        {
            Ensure.ArgumentNotNull(newCheckRun, nameof(newCheckRun));

            return ApiConnection.Post<CheckRun>(ApiUrls.CheckRuns(repositoryId), newCheckRun);
        }

        /// <summary>
        /// Updates a check run for a specific commit in a repository
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/#update-a-check-run">Check Runs API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="checkRunId">The Id of the check run</param>
        /// <param name="checkRunUpdate">The updates to the check run</param>
        [ManualRoute("PATCH", "/repos/{owner}/{repo}/check-runs/{check_run_id}")]
        public Task<CheckRun> Update(string owner, string name, long checkRunId, CheckRunUpdate checkRunUpdate)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(checkRunUpdate, nameof(checkRunUpdate));

            return ApiConnection.Patch<CheckRun>(ApiUrls.CheckRun(owner, name, checkRunId), checkRunUpdate);
        }

        /// <summary>
        /// Updates a check run for a specific commit in a repository
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/#update-a-check-run">Check Runs API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="checkRunId">The Id of the check run</param>
        /// <param name="checkRunUpdate">The updates to the check run</param>
        [ManualRoute("PATCH", "/repositories/{id}/check-runs/{check_run_id}")]
        public Task<CheckRun> Update(long repositoryId, long checkRunId, CheckRunUpdate checkRunUpdate)
        {
            Ensure.ArgumentNotNull(checkRunUpdate, nameof(checkRunUpdate));

            return ApiConnection.Patch<CheckRun>(ApiUrls.CheckRun(repositoryId, checkRunId), checkRunUpdate);
        }

        /// <summary>
        /// Lists check runs for a commit ref. The ref can be a SHA, branch name, or a tag name
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/#list-check-runs-for-a-specific-ref">Check Runs API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The commit reference (can be a SHA, branch name, or a tag name)</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/commits/{commit_sha}/check-runs")]
        public Task<CheckRunsResponse> GetAllForReference(string owner, string name, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            return GetAllForReference(owner, name, reference, new CheckRunRequest(), ApiOptions.None);
        }

        /// <summary>
        /// Lists check runs for a commit ref. The ref can be a SHA, branch name, or a tag name
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/#list-check-runs-for-a-specific-ref">Check Runs API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The commit reference (can be a SHA, branch name, or a tag name)</param>
        [ManualRoute("GET", "/repositories/{id}/commits/{commit_sha}/check-runs")]
        public Task<CheckRunsResponse> GetAllForReference(long repositoryId, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            return GetAllForReference(repositoryId, reference, new CheckRunRequest(), ApiOptions.None);
        }

        /// <summary>
        /// Lists check runs for a commit ref. The ref can be a SHA, branch name, or a tag name
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/#list-check-runs-for-a-specific-ref">Check Runs API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The commit reference (can be a SHA, branch name, or a tag name)</param>
        /// <param name="checkRunRequest">Details to filter the request, such as by check name</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/commits/{commit_sha}/check-runs")]
        public Task<CheckRunsResponse> GetAllForReference(string owner, string name, string reference, CheckRunRequest checkRunRequest)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));
            Ensure.ArgumentNotNull(checkRunRequest, nameof(checkRunRequest));

            return GetAllForReference(owner, name, reference, checkRunRequest, ApiOptions.None);
        }

        /// <summary>
        /// Lists check runs for a commit ref. The ref can be a SHA, branch name, or a tag name
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/#list-check-runs-for-a-specific-ref">Check Runs API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The commit reference (can be a SHA, branch name, or a tag name)</param>
        /// <param name="checkRunRequest">Details to filter the request, such as by check name</param>
        [ManualRoute("GET", "/repositories/{id}/commits/{commit_sha}/check-runs")]
        public Task<CheckRunsResponse> GetAllForReference(long repositoryId, string reference, CheckRunRequest checkRunRequest)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));
            Ensure.ArgumentNotNull(checkRunRequest, nameof(checkRunRequest));

            return GetAllForReference(repositoryId, reference, checkRunRequest, ApiOptions.None);
        }

        /// <summary>
        /// Lists check runs for a commit ref. The ref can be a SHA, branch name, or a tag name
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/#list-check-runs-for-a-specific-ref">Check Runs API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The commit reference (can be a SHA, branch name, or a tag name)</param>
        /// <param name="checkRunRequest">Details to filter the request, such as by check name</param>
        /// <param name="options">Options to change the API response</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/commits/{commit_sha}/check-runs")]
        public async Task<CheckRunsResponse> GetAllForReference(string owner, string name, string reference, CheckRunRequest checkRunRequest, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));
            Ensure.ArgumentNotNull(checkRunRequest, nameof(checkRunRequest));
            Ensure.ArgumentNotNull(options, nameof(options));

            var results = await ApiConnection.GetAll<CheckRunsResponse>(ApiUrls.CheckRunsForReference(owner, name, reference), checkRunRequest.ToParametersDictionary(), options).ConfigureAwait(false);

            return new CheckRunsResponse(
                results.Count > 0 ? results.Max(x => x.TotalCount) : 0,
                results.SelectMany(x => x.CheckRuns).ToList());
        }

        /// <summary>
        /// Lists check runs for a commit ref. The ref can be a SHA, branch name, or a tag name
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/#list-check-runs-for-a-specific-ref">Check Runs API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The commit reference (can be a SHA, branch name, or a tag name)</param>
        /// <param name="checkRunRequest">Details to filter the request, such as by check name</param>
        /// <param name="options">Options to change the API response</param>
        [ManualRoute("GET", "/repositories/{id}/commits/{commit_sha}/check-runs")]
        public async Task<CheckRunsResponse> GetAllForReference(long repositoryId, string reference, CheckRunRequest checkRunRequest, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));
            Ensure.ArgumentNotNull(checkRunRequest, nameof(checkRunRequest));
            Ensure.ArgumentNotNull(options, nameof(options));

            var results = await ApiConnection.GetAll<CheckRunsResponse>(ApiUrls.CheckRunsForReference(repositoryId, reference), checkRunRequest.ToParametersDictionary(), options).ConfigureAwait(false);

            return new CheckRunsResponse(
                results.Count > 0 ? results.Max(x => x.TotalCount) : 0,
                results.SelectMany(x => x.CheckRuns).ToList());
        }

        /// <summary>
        /// Lists check runs for a check suite using its Id
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/#list-check-runs-in-a-check-suite">Check Runs API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="checkSuiteId">The Id of the check suite</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/check-suite/{check_suite_id}/check-runs")]
        public Task<CheckRunsResponse> GetAllForCheckSuite(string owner, string name, long checkSuiteId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllForCheckSuite(owner, name, checkSuiteId, new CheckRunRequest(), ApiOptions.None);
        }

        /// <summary>
        /// Lists check runs for a check suite using its Id
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/#list-check-runs-in-a-check-suite">Check Runs API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="checkSuiteId">The Id of the check suite</param>
        [ManualRoute("GET", "/repositories/{id}/check-suites/{check_suite_id}/check-runs")]
        public Task<CheckRunsResponse> GetAllForCheckSuite(long repositoryId, long checkSuiteId)
        {
            return GetAllForCheckSuite(repositoryId, checkSuiteId, new CheckRunRequest(), ApiOptions.None);
        }

        /// <summary>
        /// Lists check runs for a check suite using its Id
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/#list-check-runs-in-a-check-suite">Check Runs API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="checkSuiteId">The Id of the check suite</param>
        /// <param name="checkRunRequest">Details to filter the request, such as by check name</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/check-suite/{check_suite_id}/check-runs")]
        public Task<CheckRunsResponse> GetAllForCheckSuite(string owner, string name, long checkSuiteId, CheckRunRequest checkRunRequest)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(checkRunRequest, nameof(checkRunRequest));

            return GetAllForCheckSuite(owner, name, checkSuiteId, checkRunRequest, ApiOptions.None);
        }

        /// <summary>
        /// Lists check runs for a check suite using its Id
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/#list-check-runs-in-a-check-suite">Check Runs API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="checkSuiteId">The Id of the check suite</param>
        /// <param name="checkRunRequest">Details to filter the request, such as by check name</param>
        [ManualRoute("GET", "/repositories/{id}/check-suites/{check_suite_id}/check-runs")]
        public Task<CheckRunsResponse> GetAllForCheckSuite(long repositoryId, long checkSuiteId, CheckRunRequest checkRunRequest)
        {
            Ensure.ArgumentNotNull(checkRunRequest, nameof(checkRunRequest));

            return GetAllForCheckSuite(repositoryId, checkSuiteId, checkRunRequest, ApiOptions.None);
        }

        /// <summary>
        /// Lists check runs for a check suite using its Id
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/#list-check-runs-in-a-check-suite">Check Runs API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="checkSuiteId">The Id of the check suite</param>
        /// <param name="checkRunRequest">Details to filter the request, such as by check name</param>
        /// <param name="options">Options to change the API response</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/check-suite/{check_suite_id}/check-runs")]
        public async Task<CheckRunsResponse> GetAllForCheckSuite(string owner, string name, long checkSuiteId, CheckRunRequest checkRunRequest, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(checkRunRequest, nameof(checkRunRequest));
            Ensure.ArgumentNotNull(options, nameof(options));

            var results = await ApiConnection.GetAll<CheckRunsResponse>(ApiUrls.CheckRunsForCheckSuite(owner, name, checkSuiteId), checkRunRequest.ToParametersDictionary(), options).ConfigureAwait(false);

            return new CheckRunsResponse(
                results.Count > 0 ? results.Max(x => x.TotalCount) : 0,
                results.SelectMany(x => x.CheckRuns).ToList());
        }

        /// <summary>
        /// Lists check runs for a check suite using its Id
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/#list-check-runs-in-a-check-suite">Check Runs API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="checkSuiteId">The Id of the check suite</param>
        /// <param name="checkRunRequest">Details to filter the request, such as by check name</param>
        /// <param name="options">Options to change the API response</param>
        [ManualRoute("GET", "/repositories/{id}/check-suites/{check_suite_id}/check-runs")]
        public async Task<CheckRunsResponse> GetAllForCheckSuite(long repositoryId, long checkSuiteId, CheckRunRequest checkRunRequest, ApiOptions options)
        {
            Ensure.ArgumentNotNull(checkRunRequest, nameof(checkRunRequest));
            Ensure.ArgumentNotNull(options, nameof(options));

            var results = await ApiConnection.GetAll<CheckRunsResponse>(ApiUrls.CheckRunsForCheckSuite(repositoryId, checkSuiteId), checkRunRequest.ToParametersDictionary(), options).ConfigureAwait(false);

            return new CheckRunsResponse(
                results.Count > 0 ? results.Max(x => x.TotalCount) : 0,
                results.SelectMany(x => x.CheckRuns).ToList());
        }

        /// <summary>
        /// Gets a single check run using its Id
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/#get-a-single-check-run">Check Runs API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="checkRunId">The Id of the check run</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/check-runs/{check_run_id}")]
        public Task<CheckRun> Get(string owner, string name, long checkRunId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Get<CheckRun>(ApiUrls.CheckRun(owner, name, checkRunId), null);
        }

        /// <summary>
        /// Gets a single check run using its Id
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/#get-a-single-check-run">Check Runs API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="checkRunId">The Id of the check run</param>
        [ManualRoute("GET", "/repositories/{id}/check-runs/{check_run_id}")]
        public Task<CheckRun> Get(long repositoryId, long checkRunId)
        {
            return ApiConnection.Get<CheckRun>(ApiUrls.CheckRun(repositoryId, checkRunId), null);
        }

        /// <summary>
        /// Lists annotations for a check run using the check run Id
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/#list-annotations-for-a-check-run">Check Runs API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="checkRunId">The Id of the check run</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/check-runs/{check_run_id}/annotations")]
        public Task<IReadOnlyList<CheckRunAnnotation>> GetAllAnnotations(string owner, string name, long checkRunId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllAnnotations(owner, name, checkRunId, ApiOptions.None);
        }

        /// <summary>
        /// Lists annotations for a check run using the check run Id
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/#list-annotations-for-a-check-run">Check Runs API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="checkRunId">The Id of the check run</param>
        /// <returns></returns>
        [ManualRoute("GET", "/repositories/{id}/check-runs/{check_run_id}/annotations")]
        public Task<IReadOnlyList<CheckRunAnnotation>> GetAllAnnotations(long repositoryId, long checkRunId)
        {
            return GetAllAnnotations(repositoryId, checkRunId, ApiOptions.None);
        }

        /// <summary>
        /// Lists annotations for a check run using the check run Id
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/#list-annotations-for-a-check-run">Check Runs API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="checkRunId">The Id of the check run</param>
        /// <param name="options">Options to change the API response</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/check-runs/{check_run_id}/annotations")]
        public Task<IReadOnlyList<CheckRunAnnotation>> GetAllAnnotations(string owner, string name, long checkRunId, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<CheckRunAnnotation>(ApiUrls.CheckRunAnnotations(owner, name, checkRunId), null, options);
        }

        /// <summary>
        /// Lists annotations for a check run using the check run Id
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/#list-annotations-for-a-check-run">Check Runs API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="checkRunId">The Id of the check run</param>
        /// <param name="options">Options to change the API response</param>
        [ManualRoute("GET", "/repositories/{id}/check-runs/{check_run_id}/annotations")]
        public Task<IReadOnlyList<CheckRunAnnotation>> GetAllAnnotations(long repositoryId, long checkRunId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<CheckRunAnnotation>(ApiUrls.CheckRunAnnotations(repositoryId, checkRunId), null, options);
        }
    }
}
