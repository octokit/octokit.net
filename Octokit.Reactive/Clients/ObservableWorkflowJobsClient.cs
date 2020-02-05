using System;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservableWorkflowJobsClient : IObservableWorkflowJobsClient
    {
        readonly IConnection _connection;
        readonly IWorkflowJobsClient _client;

        public ObservableWorkflowJobsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _connection = client.Connection;
            _client = client.Action.Job;
        }

        public IObservable<WorkflowJob> Get(string owner, string name, long jobId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Get(owner, name, jobId).ToObservable();
        }

        public IObservable<WorkflowJob> Get(long repositoryId, long jobId)
        {
            return _client.Get(repositoryId, jobId).ToObservable();
        }

        public IObservable<WorkflowJobsResponse> GetAll(string owner, string name, long runId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAll(owner, name, runId, ApiOptions.None);
        }

        public IObservable<WorkflowJobsResponse> GetAll(long repositoryId, long runId)
        {
            return GetAll(repositoryId, runId, ApiOptions.None);
        }

        public IObservable<WorkflowJobsResponse> GetAll(string owner, string name, long runId, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<WorkflowJobsResponse>(ApiUrls.WorkflowRunJobs(owner, name, runId), options);
        }

        public IObservable<WorkflowJobsResponse> GetAll(long repositoryId, long runId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<WorkflowJobsResponse>(ApiUrls.WorkflowRunJobs(repositoryId, runId), options);
        }

        public IObservable<string> LogsUrl(string owner, string name, long jobId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.LogsUrl(owner, name, jobId).ToObservable();
        }

        public IObservable<string> LogsUrl(long repositoryId, long jobId)
        {
            return _client.LogsUrl(repositoryId, jobId).ToObservable();
        }
    }
}
