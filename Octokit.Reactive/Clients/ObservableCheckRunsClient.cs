using System;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservableCheckRunsClient : IObservableCheckRunsClient
    {
        readonly ICheckRunsClient _client;
        readonly IConnection _connection;

        public ObservableCheckRunsClient(IGitHubClient gitHubClient)
        {
            _client = gitHubClient.Check.Run;
            _connection = gitHubClient.Connection;
        }

        public IObservable<CheckRun> Create(string owner, string name, NewCheckRun newCheckRun)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(newCheckRun, nameof(newCheckRun));

            return _client.Create(owner, name, newCheckRun).ToObservable();
        }

        public IObservable<CheckRun> Create(long repositoryId, NewCheckRun newCheckRun)
        {
            Ensure.ArgumentNotNull(newCheckRun, nameof(newCheckRun));

            return _client.Create(repositoryId, newCheckRun).ToObservable();
        }

        public IObservable<CheckRun> Get(string owner, string name, long checkRunId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Get(owner, name, checkRunId).ToObservable();
        }

        public IObservable<CheckRun> Get(long repositoryId, long checkRunId)
        {
            return _client.Get(repositoryId, checkRunId).ToObservable();
        }

        public IObservable<CheckRunAnnotation> GetAllAnnotations(string owner, string name, long checkRunId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllAnnotations(owner, name, checkRunId, ApiOptions.None);
        }

        public IObservable<CheckRunAnnotation> GetAllAnnotations(long repositoryId, long checkRunId)
        {
            return GetAllAnnotations(repositoryId, checkRunId, ApiOptions.None);
        }

        public IObservable<CheckRunAnnotation> GetAllAnnotations(string owner, string name, long checkRunId, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<CheckRunAnnotation>(ApiUrls.CheckRunAnnotations(owner, name, checkRunId), null, AcceptHeaders.ChecksApiPreview, options);
        }

        public IObservable<CheckRunAnnotation> GetAllAnnotations(long repositoryId, long checkRunId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<CheckRunAnnotation>(ApiUrls.CheckRunAnnotations(repositoryId, checkRunId), null, AcceptHeaders.ChecksApiPreview, options);
        }

        public IObservable<CheckRun> GetAllForCheckSuite(string owner, string name, long checkSuiteId, CheckRunRequest checkRunRequest)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(checkRunRequest, nameof(checkRunRequest));

            return GetAllForCheckSuite(owner, name, checkSuiteId, checkRunRequest, ApiOptions.None);
        }

        public IObservable<CheckRun> GetAllForCheckSuite(long repositoryId, long checkSuiteId, CheckRunRequest checkRunRequest)
        {
            Ensure.ArgumentNotNull(checkRunRequest, nameof(checkRunRequest));

            return GetAllForCheckSuite(repositoryId, checkSuiteId, checkRunRequest, ApiOptions.None);
        }

        public IObservable<CheckRun> GetAllForCheckSuite(string owner, string name, long checkSuiteId, CheckRunRequest checkRunRequest, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(checkRunRequest, nameof(checkRunRequest));
            Ensure.ArgumentNotNull(options, nameof(options));

            throw new NotImplementedException();
        }

        public IObservable<CheckRun> GetAllForCheckSuite(long repositoryId, long checkSuiteId, CheckRunRequest checkRunRequest, ApiOptions options)
        {
            Ensure.ArgumentNotNull(checkRunRequest, nameof(checkRunRequest));
            Ensure.ArgumentNotNull(options, nameof(options));

            throw new NotImplementedException();
        }

        public IObservable<CheckRun> GetAllForReference(string owner, string name, string reference, CheckRunRequest checkRunRequest)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));
            Ensure.ArgumentNotNull(checkRunRequest, nameof(checkRunRequest));

            throw new NotImplementedException();
        }

        public IObservable<CheckRun> GetAllForReference(long repositoryId, string reference, CheckRunRequest checkRunRequest)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));
            Ensure.ArgumentNotNull(checkRunRequest, nameof(checkRunRequest));

            throw new NotImplementedException();
        }

        public IObservable<CheckRun> GetAllForReference(string owner, string name, string reference, CheckRunRequest checkRunRequest, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));
            Ensure.ArgumentNotNull(checkRunRequest, nameof(checkRunRequest));

            throw new NotImplementedException();
        }

        public IObservable<CheckRun> GetAllForReference(long repositoryId, string reference, CheckRunRequest checkRunRequest, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));
            Ensure.ArgumentNotNull(checkRunRequest, nameof(checkRunRequest));

            throw new NotImplementedException();
        }

        public IObservable<CheckRun> Update(string owner, string name, long checkRunId, CheckRunUpdate checkRunUpdate)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(checkRunUpdate, nameof(checkRunUpdate));

            return _client.Update(owner, name, checkRunId, checkRunUpdate).ToObservable();
        }

        public IObservable<CheckRun> Update(long repositoryId, long checkRunId, CheckRunUpdate checkRunUpdate)
        {
            Ensure.ArgumentNotNull(checkRunUpdate, nameof(checkRunUpdate));

            return _client.Update(repositoryId, checkRunId, checkRunUpdate).ToObservable();
        }
    }
}
