using System;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Check Suites API
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/checks/suites/">Check Suites API documentation</a> for more information.
    /// </remarks>
    public interface ICheckSuitesClient
    {
        /// <summary>
        /// Gets a single Check Suite by Id
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/suites/#get-a-single-check-suite">Check Suites API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="checkSuiteId">The Id of the check suite</param>
        Task<CheckSuite> Get(string owner, string name, long checkSuiteId);

        /// <summary>
        /// Gets a single Check Suite by Id
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/suites/#get-a-single-check-suite">Check Suites API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="checkSuiteId">The Id of the check suite</param>
        Task<CheckSuite> Get(long repositoryId, long checkSuiteId);

        /// <summary>
        /// Lists Check Suites for a commit reference (SHA, branch name or tag name)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/suites/#list-check-suites-for-a-specific-ref">Check Suites API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The reference (SHA, branch name or tag name) to list check suites for</param>
        Task<CheckSuitesResponse> GetAllForReference(string owner, string name, string reference);

        /// <summary>
        /// Lists Check Suites for a commit reference (SHA, branch name or tag name)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/suites/#list-check-suites-for-a-specific-ref">Check Suites API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The reference (SHA, branch name or tag name) to list check suites for</param>
        Task<CheckSuitesResponse> GetAllForReference(long repositoryId, string reference);

        /// <summary>
        /// Lists Check Suites for a commit reference (SHA, branch name or tag name)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/suites/#list-check-suites-for-a-specific-ref">Check Suites API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The reference (SHA, branch name or tag name) to list check suites for</param>
        /// <param name="request">Details to filter the request, such as by App Id or Check Name</param>
        Task<CheckSuitesResponse> GetAllForReference(string owner, string name, string reference, CheckSuiteRequest request);

        /// <summary>
        /// Lists Check Suites for a commit reference (SHA, branch name or tag name)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/suites/#list-check-suites-for-a-specific-ref">Check Suites API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The reference (SHA, branch name or tag name) to list check suites for</param>
        /// <param name="request">Details to filter the request, such as by App Id or Check Name</param>
        Task<CheckSuitesResponse> GetAllForReference(long repositoryId, string reference, CheckSuiteRequest request);

        /// <summary>
        /// Lists Check Suites for a commit reference (SHA, branch name or tag name)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/suites/#list-check-suites-for-a-specific-ref">Check Suites API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The reference (SHA, branch name or tag name) to list check suites for</param>
        /// <param name="request">Details to filter the request, such as by App Id or Check Name</param>
        /// <param name="options">Options to change the API response</param>
        Task<CheckSuitesResponse> GetAllForReference(string owner, string name, string reference, CheckSuiteRequest request, ApiOptions options);

        /// <summary>
        /// Lists Check Suites for a commit reference (SHA, branch name or tag name)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/suites/#list-check-suites-for-a-specific-ref">Check Suites API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The reference (SHA, branch name or tag name) to list check suites for</param>
        /// <param name="request">Details to filter the request, such as by App Id or Check Name</param>
        /// <param name="options">Options to change the API response</param>
        Task<CheckSuitesResponse> GetAllForReference(long repositoryId, string reference, CheckSuiteRequest request, ApiOptions options);

        /// <summary>
        /// Updates Check Suites preferences on a repository, such as disabling automatic creation when code is pushed
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/suites/#set-preferences-for-check-suites-on-a-repository">Check Suites API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="preferences">The check suite preferences</param>
        Task<CheckSuitePreferencesResponse> UpdatePreferences(string owner, string name, CheckSuitePreferences preferences);

        /// <summary>
        /// Updates Check Suites preferences on a repository, such as disabling automatic creation when code is pushed
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/suites/#set-preferences-for-check-suites-on-a-repository">Check Suites API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="preferences">The check suite preferences</param>
        Task<CheckSuitePreferencesResponse> UpdatePreferences(long repositoryId, CheckSuitePreferences preferences);

        /// <summary>
        /// Creates a new Check Suite
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/suites/#create-a-check-suite">Check Suites API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="newCheckSuite">Details of the Check Suite to create</param>
        Task<CheckSuite> Create(string owner, string name, NewCheckSuite newCheckSuite);

        /// <summary>
        /// Creates a new Check Suite
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/suites/#create-a-check-suite">Check Suites API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="newCheckSuite">Details of the Check Suite to create</param>
        Task<CheckSuite> Create(long repositoryId, NewCheckSuite newCheckSuite);

        /// <summary>
        /// Triggers GitHub to rerequest an existing check suite, without pushing new code to a repository
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/suites/#request-check-suites">Check Suites API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="checkSuiteId">The Id of the check suite</param>
        Task<bool> Rerequest(string owner, string name, long checkSuiteId);

        /// <summary>
        /// Triggers GitHub to rerequest an existing check suite, without pushing new code to a repository
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/suites/#request-check-suites">Check Suites API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="checkSuiteId">The Id of the check suite</param>
        Task<bool> Rerequest(long repositoryId, long checkSuiteId);
    }
}
