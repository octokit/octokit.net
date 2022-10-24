using System;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Check Suites API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/checks/suites/">Check Suites API documentation</a> for more information.
    /// </remarks>
    public class ObservableCheckSuitesClient : IObservableCheckSuitesClient
    {
        readonly ICheckSuitesClient _client;
        readonly IConnection _connection;

        /// <summary>
        /// Initializes a new GitHub Check Suites API client.
        /// </summary>
        /// <param name="client">An <see cref="IGitHubClient" /> used to make the requests</param>
        public ObservableCheckSuitesClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Check.Suite;
            _connection = client.Connection;
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
        public IObservable<CheckSuite> Get(string owner, string name, long checkSuiteId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Get(owner, name, checkSuiteId).ToObservable();
        }

        /// <summary>
        /// Gets a single Check Suite by Id
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/suites/#get-a-single-check-suite">Check Suites API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="checkSuiteId">The Id of the check suite</param>
        public IObservable<CheckSuite> Get(long repositoryId, long checkSuiteId)
        {
            return _client.Get(repositoryId, checkSuiteId).ToObservable();
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
        public IObservable<CheckSuitesResponse> GetAllForReference(string owner, string name, string reference)
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
        public IObservable<CheckSuitesResponse> GetAllForReference(long repositoryId, string reference)
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
        public IObservable<CheckSuitesResponse> GetAllForReference(string owner, string name, string reference, CheckSuiteRequest request)
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
        public IObservable<CheckSuitesResponse> GetAllForReference(long repositoryId, string reference, CheckSuiteRequest request)
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
        public IObservable<CheckSuitesResponse> GetAllForReference(string owner, string name, string reference, CheckSuiteRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<CheckSuitesResponse>(ApiUrls.CheckSuitesForReference(owner, name, reference), request.ToParametersDictionary(), options);
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
        public IObservable<CheckSuitesResponse> GetAllForReference(long repositoryId, string reference, CheckSuiteRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<CheckSuitesResponse>(ApiUrls.CheckSuitesForReference(repositoryId, reference), request.ToParametersDictionary(), options);
        }

        /// <summary>
        /// Updates Check Suites prefrences on a repository, such as disabling automatic creation when code is pushed
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/suites/#set-preferences-for-check-suites-on-a-repository">Check Suites API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="preferences">The check suite preferences</param>
        public IObservable<CheckSuitePreferencesResponse> UpdatePreferences(string owner, string name, CheckSuitePreferences preferences)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(preferences, nameof(preferences));

            return _client.UpdatePreferences(owner, name, preferences).ToObservable();
        }

        /// <summary>
        /// Updates Check Suites prefrences on a repository, such as disabling automatic creation when code is pushed
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/suites/#set-preferences-for-check-suites-on-a-repository">Check Suites API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="preferences">The check suite preferences</param>
        public IObservable<CheckSuitePreferencesResponse> UpdatePreferences(long repositoryId, CheckSuitePreferences preferences)
        {
            Ensure.ArgumentNotNull(preferences, nameof(preferences));

            return _client.UpdatePreferences(repositoryId, preferences).ToObservable();
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
        public IObservable<CheckSuite> Create(string owner, string name, NewCheckSuite newCheckSuite)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(newCheckSuite, nameof(newCheckSuite));

            return _client.Create(owner, name, newCheckSuite).ToObservable();
        }

        /// <summary>
        /// Creates a new Check Suite
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/suites/#create-a-check-suite">Check Suites API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="newCheckSuite">Details of the Check Suite to create</param>
        public IObservable<CheckSuite> Create(long repositoryId, NewCheckSuite newCheckSuite)
        {
            Ensure.ArgumentNotNull(newCheckSuite, nameof(newCheckSuite));

            return _client.Create(repositoryId, newCheckSuite).ToObservable();
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
        public IObservable<bool> Rerequest(string owner, string name, long checkSuiteId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Rerequest(owner, name, checkSuiteId).ToObservable();
        }

        /// <summary>
        /// Triggers GitHub to rerequest an existing check suite, without pushing new code to a repository
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/suites/#request-check-suites">Check Suites API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="checkSuiteId">The Id of the check suite</param>
        public IObservable<bool> Rerequest(long repositoryId, long checkSuiteId)
        {
            return _client.Rerequest(repositoryId, checkSuiteId).ToObservable();
        }
    }
}
