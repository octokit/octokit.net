using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Octokit.Models.Response;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive.Clients
{
    public class ObservableWorkflowClient : IObservableWorkflowsClient
    {
        readonly IWorkflowsClient _client;
        readonly IConnection _connection;

        public ObservableWorkflowClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Actions.Workflows;
            _connection = client.Connection;
        }

        public IObservable<Workflow> GetAllForRepository(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllForRepository(owner, name, ApiOptions.None);
        }

        public IObservable<Workflow> GetAllForRepository(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));
            
            return _connection.GetAndFlattenAllPages<Workflow>(ApiUrls.Workflows(owner, name), options);
        }

        public IObservable<Unit> CreateWorkflowDispatchEvent(string owner, string name, string workflowId, WorkflowDispatchEvent workflowDispatchEvent)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(workflowId, nameof(workflowId));
            Ensure.ArgumentNotNull(workflowDispatchEvent, nameof(workflowDispatchEvent));
            Ensure.ArgumentNotNullOrEmptyString(workflowDispatchEvent.Ref, nameof(workflowDispatchEvent.Ref));

            return _client.CreateWorkflowDispatchEvent(owner, name, workflowId, workflowDispatchEvent).ToObservable();
        }
    }
}
