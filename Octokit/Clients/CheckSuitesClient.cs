﻿using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Check Suites API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/checks/suites/">Check Suites API documentation</a> for more information.
    /// </remarks>
    public class CheckSuitesClient : ApiClient, ICheckSuitesClient
    {
        /// <summary>
        /// Initializes a new GitHub Check Suites API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public CheckSuitesClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        /// <summary>
        /// Gets a single Check Suite by Id
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/suites/#get-a-single-check-suite">Check Suites API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="checkSuiteId">The Id of the check suite</param>
        public Task<CheckSuite> Get(string owner, string name, long checkSuiteId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Get<CheckSuite>(ApiUrls.CheckSuite(owner, name, checkSuiteId), null, AcceptHeaders.ChecksApiPreview);
        }

        /// <summary>
        /// Gets a single Check Suite by Id
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/suites/#get-a-single-check-suite">Check Suites API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="checkSuiteId">The Id of the check suite</param>
        public Task<CheckSuite> Get(long repositoryId, long checkSuiteId)
        {
            return ApiConnection.Get<CheckSuite>(ApiUrls.CheckSuite(repositoryId, checkSuiteId), null, AcceptHeaders.ChecksApiPreview);
        }

        /// <summary>
        /// Lists Check Suites for a commit reference (SHA, branch name or tag name)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/suites/#list-check-suites-for-a-specific-ref">Check Suites API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The reference (SHA, branch name or tag name) to list check suites for</param>
        public Task<CheckSuitesResponse> GetAllForReference(string owner, string name, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            return GetAllForReference(owner, name, reference, new CheckSuiteRequest(), ApiOptions.None);
        }

        /// <summary>
        /// Lists Check Suites for a commit reference (SHA, branch name or tag name)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/suites/#list-check-suites-for-a-specific-ref">Check Suites API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The reference (SHA, branch name or tag name) to list check suites for</param>
        public Task<CheckSuitesResponse> GetAllForReference(long repositoryId, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            return GetAllForReference(repositoryId, reference, new CheckSuiteRequest(), ApiOptions.None);
        }

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
        public Task<CheckSuitesResponse> GetAllForReference(string owner, string name, string reference, CheckSuiteRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAllForReference(owner, name, reference, request, ApiOptions.None);
        }

        /// <summary>
        /// Lists Check Suites for a commit reference (SHA, branch name or tag name)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/suites/#list-check-suites-for-a-specific-ref">Check Suites API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The reference (SHA, branch name or tag name) to list check suites for</param>
        /// <param name="request">Details to filter the request, such as by App Id or Check Name</param>
        public Task<CheckSuitesResponse> GetAllForReference(long repositoryId, string reference, CheckSuiteRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAllForReference(repositoryId, reference, request, ApiOptions.None);
        }

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
        public async Task<CheckSuitesResponse> GetAllForReference(string owner, string name, string reference, CheckSuiteRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            var results = await ApiConnection.GetAll<CheckSuitesResponse>(ApiUrls.CheckSuitesForReference(owner, name, reference), request.ToParametersDictionary(), AcceptHeaders.ChecksApiPreview, options).ConfigureAwait(false);

            return new CheckSuitesResponse(
                results.Count > 0 ? results.Max(x => x.TotalCount) : 0,
                results.SelectMany(x => x.CheckSuites).ToList());
        }

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
        public async Task<CheckSuitesResponse> GetAllForReference(long repositoryId, string reference, CheckSuiteRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            var results = await ApiConnection.GetAll<CheckSuitesResponse>(ApiUrls.CheckSuitesForReference(repositoryId, reference), request.ToParametersDictionary(), AcceptHeaders.ChecksApiPreview, options).ConfigureAwait(false);

            return new CheckSuitesResponse(
                results.Count > 0 ? results.Max(x => x.TotalCount) : 0,
                results.SelectMany(x => x.CheckSuites).ToList());
        }

        /// <summary>
        /// Updates Check Suites preferences on a repository, such as disabling automatic creation when code is pushed
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/suites/#set-preferences-for-check-suites-on-a-repository">Check Suites API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="preferences">The check suite preferences</param>
        public Task<CheckSuitePreferencesResponse> UpdatePreferences(string owner, string name, CheckSuitePreferences preferences)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(preferences, nameof(preferences));

            return ApiConnection.Patch<CheckSuitePreferencesResponse>(ApiUrls.CheckSuitePreferences(owner, name), preferences, AcceptHeaders.ChecksApiPreview);
        }

        /// <summary>
        /// Updates Check Suites preferences on a repository, such as disabling automatic creation when code is pushed
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/suites/#set-preferences-for-check-suites-on-a-repository">Check Suites API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="preferences">The check suite preferences</param>
        public Task<CheckSuitePreferencesResponse> UpdatePreferences(long repositoryId, CheckSuitePreferences preferences)
        {
            Ensure.ArgumentNotNull(preferences, nameof(preferences));

            return ApiConnection.Patch<CheckSuitePreferencesResponse>(ApiUrls.CheckSuitePreferences(repositoryId), preferences, AcceptHeaders.ChecksApiPreview);
        }

        /// <summary>
        /// Creates a new Check Suite
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/suites/#create-a-check-suite">Check Suites API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="newCheckSuite">Details of the Check Suite to create</param>
        public Task<CheckSuite> Create(string owner, string name, NewCheckSuite newCheckSuite)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(newCheckSuite, nameof(newCheckSuite));

            return ApiConnection.Post<CheckSuite>(ApiUrls.CheckSuites(owner, name), newCheckSuite, AcceptHeaders.ChecksApiPreview);
        }

        /// <summary>
        /// Creates a new Check Suite
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/suites/#create-a-check-suite">Check Suites API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="newCheckSuite">Details of the Check Suite to create</param>
        public Task<CheckSuite> Create(long repositoryId, NewCheckSuite newCheckSuite)
        {
            Ensure.ArgumentNotNull(newCheckSuite, nameof(newCheckSuite));

            return ApiConnection.Post<CheckSuite>(ApiUrls.CheckSuites(repositoryId), newCheckSuite, AcceptHeaders.ChecksApiPreview);
        }

        /// <summary>
        /// Triggers GitHub to create a new check suite, without pushing new code to a repository
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/suites/#request-check-suites">Check Suites API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">Details of the Check Suite request</param>
        [Obsolete("This method has been deprecated in the GitHub Api, however can still be used on GitHub Enterprise 2.14")]
        public async Task<bool> Request(string owner, string name, CheckSuiteTriggerRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(request, nameof(request));

            var httpStatusCode = await Connection.Post(ApiUrls.CheckSuiteRequests(owner, name), request, AcceptHeaders.ChecksApiPreview).ConfigureAwait(false);

            if (httpStatusCode != HttpStatusCode.Created)
            {
                throw new ApiException("Invalid Status Code returned. Expected a 201", httpStatusCode);
            }

            return httpStatusCode == HttpStatusCode.Created;
        }

        /// <summary>
        /// Triggers GitHub to create a new check suite, without pushing new code to a repository
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/suites/#request-check-suites">Check Suites API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">Details of the Check Suite request</param>
        [Obsolete("This method has been deprecated in the GitHub Api, however can still be used on GitHub Enterprise 2.14")]
        public async Task<bool> Request(long repositoryId, CheckSuiteTriggerRequest request)
        {
            Ensure.ArgumentNotNull(request, nameof(request));

            var httpStatusCode = await Connection.Post(ApiUrls.CheckSuiteRequests(repositoryId), request, AcceptHeaders.ChecksApiPreview).ConfigureAwait(false);

            if (httpStatusCode != HttpStatusCode.Created)
            {
                throw new ApiException("Invalid Status Code returned. Expected a 201", httpStatusCode);
            }

            return httpStatusCode == HttpStatusCode.Created;
        }

        /// <summary>
        /// Triggers GitHub to rerequest an existing check suite, without pushing new code to a repository
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/suites/#request-check-suites">Check Suites API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="checkSuiteId">The Id of the check suite</param>
        public async Task<bool> Rerequest(string owner, string name, long checkSuiteId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            var httpStatusCode = await Connection.Post(ApiUrls.CheckSuiteRerequest(owner, name, checkSuiteId), null, AcceptHeaders.ChecksApiPreview).ConfigureAwait(false);

            if (httpStatusCode != HttpStatusCode.Created)
            {
                throw new ApiException("Invalid Status Code returned. Expected a 201", httpStatusCode);
            }

            return httpStatusCode == HttpStatusCode.Created;
        }

        /// <summary>
        /// Triggers GitHub to rerequest an existing check suite, without pushing new code to a repository
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/suites/#request-check-suites">Check Suites API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="checkSuiteId">The Id of the check suite</param>
        public async Task<bool> Rerequest(long repositoryId, long checkSuiteId)
        {
            var httpStatusCode = await Connection.Post(ApiUrls.CheckSuiteRerequest(repositoryId, checkSuiteId), null, AcceptHeaders.ChecksApiPreview).ConfigureAwait(false);

            if (httpStatusCode != HttpStatusCode.Created)
            {
                throw new ApiException("Invalid Status Code returned. Expected a 201", httpStatusCode);
            }

            return httpStatusCode == HttpStatusCode.Created;
        }
    }
}