using System;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservableCheckSuitesClient : IObservableCheckSuitesClient
    {
        readonly ICheckSuitesClient _client;
        readonly IConnection _connection;

        public ObservableCheckSuitesClient(IGitHubClient gitHubClient)
        {
            _client = gitHubClient.Check.Suite;
            _connection = gitHubClient.Connection;
        }

        public IObservable<CheckSuite> Create(long repositoryId, NewCheckSuite newCheckSuite)
        {
            Ensure.ArgumentNotNull(newCheckSuite, nameof(newCheckSuite));

            return _client.Create(repositoryId, newCheckSuite).ToObservable();
        }

        public IObservable<CheckSuite> Create(string owner, string name, NewCheckSuite newCheckSuite)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(newCheckSuite, nameof(newCheckSuite));
            
            return _client.Create(owner, name, newCheckSuite).ToObservable();
        }

        public IObservable<CheckSuite> Get(long repositoryId, long checkSuiteId)
        {
            return _client.Get(repositoryId, checkSuiteId).ToObservable();
        }

        public IObservable<CheckSuite> Get(string owner, string name, long checkSuiteId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Get(owner, name, checkSuiteId).ToObservable();
        }

        public IObservable<CheckSuite> GetAllForReference(long repositoryId, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            return GetAllForReference(repositoryId, reference, ApiOptions.None);
        }

        public IObservable<CheckSuite> GetAllForReference(string owner, string name, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            return GetAllForReference(owner, name, reference, ApiOptions.None);
        }

        public IObservable<CheckSuite> GetAllForReference(long repositoryId, string reference, CheckSuiteRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAllForReference(repositoryId, reference, request, ApiOptions.None);
        }

        public IObservable<CheckSuite> GetAllForReference(string owner, string name, string reference, CheckSuiteRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAllForReference(owner, name, reference, request, ApiOptions.None);
        }

        public IObservable<CheckSuite> GetAllForReference(long repositoryId, string reference, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));
            Ensure.ArgumentNotNull(options, nameof(options));

            throw new NotImplementedException();
        }

        public IObservable<CheckSuite> GetAllForReference(string owner, string name, string reference, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));
            Ensure.ArgumentNotNull(options, nameof(options));

            throw new NotImplementedException();
        }

        public IObservable<CheckSuite> GetAllForReference(long repositoryId, string reference, CheckSuiteRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            throw new NotImplementedException();
        }

        public IObservable<CheckSuite> GetAllForReference(string owner, string name, string reference, CheckSuiteRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            throw new NotImplementedException();
        }

        public IObservable<CheckSuite> Request(long repositoryId, CheckSuiteTriggerRequest request)
        {
            return _client.Request(repositoryId, request).ToObservable();
        }

        public IObservable<CheckSuite> Request(string owner, string name, CheckSuiteTriggerRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Request(owner, name, request).ToObservable();
        }

        public IObservable<CheckSuitePreferences> UpdatePreferences(long repositoryId, AutoTriggerChecksObject preferences)
        {
            Ensure.ArgumentNotNull(preferences, nameof(preferences));

            return _client.UpdatePreferences(repositoryId, preferences).ToObservable();
        }

        public IObservable<CheckSuitePreferences> UpdatePreferences(string owner, string name, AutoTriggerChecksObject preferences)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(preferences, nameof(preferences));

            return _client.UpdatePreferences(owner, name, preferences).ToObservable();
        }
    }
}