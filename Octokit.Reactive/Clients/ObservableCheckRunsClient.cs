using System;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;
using System.Threading;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Check Runs API
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/checks/runs/">Check Runs API documentation</a> for more information.
    /// </remarks>
    public class ObservableCheckRunsClient : IObservableCheckRunsClient
    {
        readonly ICheckRunsClient _client;
        readonly IConnection _connection;

        /// <summary>
        /// Initializes a new GitHub Check Runs API client
        /// </summary>
        /// <param name="client">An <see cref="IGitHubClient" /> used to make the requests</param>
        public ObservableCheckRunsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Check.Run;
            _connection = client.Connection;
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
        public IObservable<CheckRun> Create(string owner, string name, NewCheckRun newCheckRun, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(newCheckRun, nameof(newCheckRun));

            return _client.Create(owner, name, newCheckRun, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Creates a new check run for a specific commit in a repository
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/#create-a-check-run">Check Runs API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="newCheckRun">Details of the Check Run to create</param>
        public IObservable<CheckRun> Create(long repositoryId, NewCheckRun newCheckRun, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(newCheckRun, nameof(newCheckRun));

            return _client.Create(repositoryId, newCheckRun, cancellationToken).ToObservable();
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
        public IObservable<CheckRun> Update(string owner, string name, long checkRunId, CheckRunUpdate checkRunUpdate, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(checkRunUpdate, nameof(checkRunUpdate));

            return _client.Update(owner, name, checkRunId, checkRunUpdate, cancellationToken).ToObservable();
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
        public IObservable<CheckRun> Update(long repositoryId, long checkRunId, CheckRunUpdate checkRunUpdate, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(checkRunUpdate, nameof(checkRunUpdate));

            return _client.Update(repositoryId, checkRunId, checkRunUpdate, cancellationToken).ToObservable();
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
        public IObservable<CheckRunsResponse> GetAllForReference(string owner, string name, string reference, CancellationToken cancellationToken = default)
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
        public IObservable<CheckRunsResponse> GetAllForReference(long repositoryId, string reference, CancellationToken cancellationToken = default)
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
        public IObservable<CheckRunsResponse> GetAllForReference(string owner, string name, string reference, CheckRunRequest checkRunRequest, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));
            Ensure.ArgumentNotNull(checkRunRequest, nameof(checkRunRequest));

            return GetAllForReference(owner, name, reference, checkRunRequest, ApiOptions.None, cancellationToken);
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
        public IObservable<CheckRunsResponse> GetAllForReference(long repositoryId, string reference, CheckRunRequest checkRunRequest, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));
            Ensure.ArgumentNotNull(checkRunRequest, nameof(checkRunRequest));

            return GetAllForReference(repositoryId, reference, checkRunRequest, ApiOptions.None, cancellationToken);
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
        public IObservable<CheckRunsResponse> GetAllForReference(string owner, string name, string reference, CheckRunRequest checkRunRequest, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));
            Ensure.ArgumentNotNull(checkRunRequest, nameof(checkRunRequest));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<CheckRunsResponse>(ApiUrls.CheckRunsForReference(owner, name, reference), checkRunRequest.ToParametersDictionary(), options);
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
        public IObservable<CheckRunsResponse> GetAllForReference(long repositoryId, string reference, CheckRunRequest checkRunRequest, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));
            Ensure.ArgumentNotNull(checkRunRequest, nameof(checkRunRequest));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<CheckRunsResponse>(ApiUrls.CheckRunsForReference(repositoryId, reference), checkRunRequest.ToParametersDictionary(), options);
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
        public IObservable<CheckRunsResponse> GetAllForCheckSuite(string owner, string name, long checkSuiteId, CancellationToken cancellationToken = default)
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
        public IObservable<CheckRunsResponse> GetAllForCheckSuite(long repositoryId, long checkSuiteId, CancellationToken cancellationToken = default)
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
        public IObservable<CheckRunsResponse> GetAllForCheckSuite(string owner, string name, long checkSuiteId, CheckRunRequest checkRunRequest, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(checkRunRequest, nameof(checkRunRequest));

            return GetAllForCheckSuite(owner, name, checkSuiteId, checkRunRequest, ApiOptions.None, cancellationToken);
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
        public IObservable<CheckRunsResponse> GetAllForCheckSuite(long repositoryId, long checkSuiteId, CheckRunRequest checkRunRequest, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(checkRunRequest, nameof(checkRunRequest));

            return GetAllForCheckSuite(repositoryId, checkSuiteId, checkRunRequest, ApiOptions.None, cancellationToken);
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
        public IObservable<CheckRunsResponse> GetAllForCheckSuite(string owner, string name, long checkSuiteId, CheckRunRequest checkRunRequest, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(checkRunRequest, nameof(checkRunRequest));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<CheckRunsResponse>(ApiUrls.CheckRunsForCheckSuite(owner, name, checkSuiteId), checkRunRequest.ToParametersDictionary(), options);
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
        public IObservable<CheckRunsResponse> GetAllForCheckSuite(long repositoryId, long checkSuiteId, CheckRunRequest checkRunRequest, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(checkRunRequest, nameof(checkRunRequest));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<CheckRunsResponse>(ApiUrls.CheckRunsForCheckSuite(repositoryId, checkSuiteId), checkRunRequest.ToParametersDictionary(), options);
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
        public IObservable<CheckRun> Get(string owner, string name, long checkRunId, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Get(owner, name, checkRunId, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Gets a single check run using its Id
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/checks/runs/#get-a-single-check-run">Check Runs API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="checkRunId">The Id of the check run</param>
        public IObservable<CheckRun> Get(long repositoryId, long checkRunId, CancellationToken cancellationToken = default)
        {
            return _client.Get(repositoryId, checkRunId, cancellationToken).ToObservable();
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
        public IObservable<CheckRunAnnotation> GetAllAnnotations(string owner, string name, long checkRunId, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllAnnotations(owner, name, checkRunId, ApiOptions.None, cancellationToken);
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
        public IObservable<CheckRunAnnotation> GetAllAnnotations(long repositoryId, long checkRunId, CancellationToken cancellationToken = default)
        {
            return GetAllAnnotations(repositoryId, checkRunId, ApiOptions.None, cancellationToken);
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
        public IObservable<CheckRunAnnotation> GetAllAnnotations(string owner, string name, long checkRunId, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<CheckRunAnnotation>(ApiUrls.CheckRunAnnotations(owner, name, checkRunId), null, options);
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
        public IObservable<CheckRunAnnotation> GetAllAnnotations(long repositoryId, long checkRunId, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<CheckRunAnnotation>(ApiUrls.CheckRunAnnotations(repositoryId, checkRunId), null, options, cancellationToken);
        }
    }
}
