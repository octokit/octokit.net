using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Octokit.Models.Response;

namespace Octokit.Reactive.Clients
{
    public class ObservableWorkflowClient : IObservableWorkflowsClient
    {
        readonly IWorkflowsClient _client;

        public ObservableWorkflowClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Actions.Workflows;
        }

        public IObservable<IReadOnlyList<Workflow>> GetAllForRepository(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.GetAllForRepository(owner, name).ToObservable();
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
