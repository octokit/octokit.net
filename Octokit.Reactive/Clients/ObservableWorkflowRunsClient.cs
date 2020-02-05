using System;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservableWorkflowRunsClient : IObservableWorkflowRunsClient
    {
        readonly IConnection _connection;
        readonly IWorkflowRunsClient _client;

        public ObservableWorkflowRunsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _connection = client.Connection;
            _client = client.Action.Run;
        }


        public IObservable<WorkflowRunsResponse> GetAllForWorkflowId(string owner, string name, long workflowId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllForWorkflowId(owner, name, workflowId, new WorkflowRunsRequest(), ApiOptions.None);
        }

        public IObservable<WorkflowRunsResponse> GetAllForWorkflowId(long repositoryId, long workflowId)
        {
            return GetAllForWorkflowId(repositoryId, workflowId, new WorkflowRunsRequest(), ApiOptions.None);
        }

        public IObservable<WorkflowRunsResponse> GetAllForWorkflowId(string owner, string name, long workflowId, WorkflowRunsRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAllForWorkflowId(owner, name, workflowId, request, ApiOptions.None);
        }

        public IObservable<WorkflowRunsResponse> GetAllForWorkflowId(long repositoryId, long workflowId, WorkflowRunsRequest request)
        {
            Ensure.ArgumentNotNull(request, nameof(request));
            return GetAllForWorkflowId(repositoryId, workflowId, request, ApiOptions.None);
        }

        public IObservable<WorkflowRunsResponse> GetAllForWorkflowId(string owner, string name, long workflowId, WorkflowRunsRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<WorkflowRunsResponse>(ApiUrls.WorkflowRuns(owner, name, workflowId), request.ToParametersDictionary(), options);
        }

        public IObservable<WorkflowRunsResponse> GetAllForWorkflowId(long repositoryId, long workflowId, WorkflowRunsRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<WorkflowRunsResponse>(ApiUrls.WorkflowRuns(repositoryId, workflowId), request.ToParametersDictionary(), options);
        }

        public IObservable<WorkflowRunsResponse> GetAllForRepository(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllForRepository(owner, name, new WorkflowRunsRequest(), ApiOptions.None);
        }

        public IObservable<WorkflowRunsResponse> GetAllForRepository(long repositoryId)
        {
            return GetAllForRepository(repositoryId, new WorkflowRunsRequest(), ApiOptions.None);
        }

        public IObservable<WorkflowRunsResponse> GetAllForRepository(string owner, string name, WorkflowRunsRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAllForRepository(owner, name, request, ApiOptions.None);
        }

        public IObservable<WorkflowRunsResponse> GetAllForRepository(long repositoryId, WorkflowRunsRequest request)
        {
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAllForRepository(repositoryId, request, ApiOptions.None);
        }

        public IObservable<WorkflowRunsResponse> GetAllForRepository(string owner, string name, WorkflowRunsRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<WorkflowRunsResponse>(ApiUrls.WorkflowRunsForRepository(owner, name), request.ToParametersDictionary(), options);
        }

        public IObservable<WorkflowRunsResponse> GetAllForRepository(long repositoryId, WorkflowRunsRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<WorkflowRunsResponse>(ApiUrls.WorkflowRunsForRepository(repositoryId), request.ToParametersDictionary(), options);
        }


        public IObservable<WorkflowRun> Get(string owner, string name, long runId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Get(owner, name, runId).ToObservable();
        }

        public IObservable<WorkflowRun> Get(long repositoryId, long runId)
        {
            return _client.Get(repositoryId, runId).ToObservable();
        }

        public IObservable<bool> ReRun(string owner, string name, long runId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.ReRun(owner, name, runId).ToObservable();
        }

        public IObservable<bool> ReRun(long repositoryId, long runId)
        {
            return _client.ReRun(repositoryId, runId).ToObservable();
        }


        public IObservable<bool> Cancel(string owner, string name, long runId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Cancel(owner, name, runId).ToObservable();
        }

        public IObservable<bool> Cancel(long repositoryId, long runId)
        {
            return _client.Cancel(repositoryId, runId).ToObservable();
        }

        public IObservable<string> LogsUrl(string owner, string name, long runId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.LogsUrl(owner, name, runId).ToObservable();
        }

        public IObservable<string> LogsUrl(long repositoryId, long runId)
        {
            return _client.LogsUrl(repositoryId, runId).ToObservable();
        }

    }
}