using System;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservableWorkflowsClient : IObservableWorkflowsClient
    {
        readonly IConnection _connection;
        readonly IWorkflowsClient _client;

        public ObservableWorkflowsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _connection = client.Connection;
            _client = client.Action.Workflow;
        }

        public IObservable<WorkflowsResponse> GetAll(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAll(owner, name, ApiOptions.None);
        }

        public IObservable<WorkflowsResponse> GetAll(long repositoryId)
        {
            throw new NotImplementedException();
        }

        public IObservable<WorkflowsResponse> GetAll(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<WorkflowsResponse>(ApiUrls.Workflows(owner, name), options);
        }

        public IObservable<WorkflowsResponse> GetAll(long repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<WorkflowsResponse>(ApiUrls.Workflows(repositoryId), options);
        }

        public IObservable<Workflow> Get(string owner, string name, long workflowId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Get(owner, name, workflowId).ToObservable();
        }

        public IObservable<Workflow> Get(string owner, string name, string workflowFileName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(workflowFileName, nameof(workflowFileName));

            return _client.Get(owner, name, workflowFileName).ToObservable();
        }

        public IObservable<Workflow> Get(long repositoryId, long workflowId)
        {
            return _client.Get(repositoryId, workflowId).ToObservable();
        }

        public IObservable<Workflow> Get(long repositoryId, string workflowFileName)
        {
            Ensure.ArgumentNotNullOrEmptyString(workflowFileName, nameof(workflowFileName));

            return _client.Get(repositoryId, workflowFileName).ToObservable();
        }


    }
}
