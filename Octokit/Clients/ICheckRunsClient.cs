using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Check Runs API
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/checks/runs/">Check Runs API documentation</a> for more information.
    /// </remarks>
    public interface ICheckRunsClient
    {
        /// <summary>
        /// Creates a new check run for a specific commit in a repository
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/#create-a-check-run">Check Runs API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="newCheckRun">Details of the Check Run to create</param>
        Task<CheckRun> Create(string owner, string name, NewCheckRun newCheckRun);

        /// <summary>
        /// Creates a new check run for a specific commit in a repository
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/#create-a-check-run">Check Runs API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="newCheckRun">Details of the Check Run to create</param>
        Task<CheckRun> Create(long repositoryId, NewCheckRun newCheckRun);

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
        Task<CheckRun> Update(string owner, string name, long checkRunId, CheckRunUpdate checkRunUpdate);

        /// <summary>
        /// Updates a check run for a specific commit in a repository
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/#update-a-check-run">Check Runs API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="checkRunId">The Id of the check run</param>
        /// <param name="checkRunUpdate">The updates to the check run</param>
        Task<CheckRun> Update(long repositoryId, long checkRunId, CheckRunUpdate checkRunUpdate);

        /// <summary>
        /// Lists check runs for a commit ref. The ref can be a SHA, branch name, or a tag name
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/#list-check-runs-for-a-specific-ref">Check Runs API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The commit reference (can be a SHA, branch name, or a tag name)</param>
        Task<CheckRunsResponse> GetAllForReference(string owner, string name, string reference);

        /// <summary>
        /// Lists check runs for a commit ref. The ref can be a SHA, branch name, or a tag name
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/#list-check-runs-for-a-specific-ref">Check Runs API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The commit reference (can be a SHA, branch name, or a tag name)</param>
        Task<CheckRunsResponse> GetAllForReference(long repositoryId, string reference);

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
        Task<CheckRunsResponse> GetAllForReference(string owner, string name, string reference, CheckRunRequest checkRunRequest);

        /// <summary>
        /// Lists check runs for a commit ref. The ref can be a SHA, branch name, or a tag name
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/#list-check-runs-for-a-specific-ref">Check Runs API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The commit reference (can be a SHA, branch name, or a tag name)</param>
        /// <param name="checkRunRequest">Details to filter the request, such as by check name</param>
        Task<CheckRunsResponse> GetAllForReference(long repositoryId, string reference, CheckRunRequest checkRunRequest);

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
        Task<CheckRunsResponse> GetAllForReference(string owner, string name, string reference, CheckRunRequest checkRunRequest, ApiOptions options);

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
        Task<CheckRunsResponse> GetAllForReference(long repositoryId, string reference, CheckRunRequest checkRunRequest, ApiOptions options);

        /// <summary>
        /// Lists check runs for a check suite using its Id
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/#list-check-runs-in-a-check-suite">Check Runs API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="checkSuiteId">The Id of the check suite</param>
        Task<CheckRunsResponse> GetAllForCheckSuite(string owner, string name, long checkSuiteId);

        /// <summary>
        /// Lists check runs for a check suite using its Id
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/#list-check-runs-in-a-check-suite">Check Runs API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="checkSuiteId">The Id of the check suite</param>
        Task<CheckRunsResponse> GetAllForCheckSuite(long repositoryId, long checkSuiteId);

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
        Task<CheckRunsResponse> GetAllForCheckSuite(string owner, string name, long checkSuiteId, CheckRunRequest checkRunRequest);

        /// <summary>
        /// Lists check runs for a check suite using its Id
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/#list-check-runs-in-a-check-suite">Check Runs API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="checkSuiteId">The Id of the check suite</param>
        /// <param name="checkRunRequest">Details to filter the request, such as by check name</param>
        Task<CheckRunsResponse> GetAllForCheckSuite(long repositoryId, long checkSuiteId, CheckRunRequest checkRunRequest);

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
        Task<CheckRunsResponse> GetAllForCheckSuite(string owner, string name, long checkSuiteId, CheckRunRequest checkRunRequest, ApiOptions options);

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
        Task<CheckRunsResponse> GetAllForCheckSuite(long repositoryId, long checkSuiteId, CheckRunRequest checkRunRequest, ApiOptions options);

        /// <summary>
        /// Gets a single check run using its Id
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/#get-a-single-check-run">Check Runs API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="checkRunId">The Id of the check run</param>
        Task<CheckRun> Get(string owner, string name, long checkRunId);

        /// <summary>
        /// Gets a single check run using its Id
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/#get-a-single-check-run">Check Runs API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="checkRunId">The Id of the check run</param>
        Task<CheckRun> Get(long repositoryId, long checkRunId);

        /// <summary>
        /// Lists annotations for a check run using the check run Id
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/#list-annotations-for-a-check-run">Check Runs API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="checkRunId">The Id of the check run</param>
        Task<IReadOnlyList<CheckRunAnnotation>> GetAllAnnotations(string owner, string name, long checkRunId);

        /// <summary>
        /// Lists annotations for a check run using the check run Id
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/#list-annotations-for-a-check-run">Check Runs API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="checkRunId">The Id of the check run</param>
        /// <returns></returns>
        Task<IReadOnlyList<CheckRunAnnotation>> GetAllAnnotations(long repositoryId, long checkRunId);

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
        Task<IReadOnlyList<CheckRunAnnotation>> GetAllAnnotations(string owner, string name, long checkRunId, ApiOptions options);

        /// <summary>
        /// Lists annotations for a check run using the check run Id
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/#list-annotations-for-a-check-run">Check Runs API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="checkRunId">The Id of the check run</param>
        /// <param name="options">Options to change the API response</param>
        Task<IReadOnlyList<CheckRunAnnotation>> GetAllAnnotations(long repositoryId, long checkRunId, ApiOptions options);
    }
}
