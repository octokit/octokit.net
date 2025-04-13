using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    public class ObservableActionsWorkflowsClient : IObservableActionsWorkflowsClient
    {
        readonly IActionsWorkflowsClient _client;

        /// <summary>
        /// Instantiate a new GitHub Actions Workflows API client.
        /// </summary>
        /// <param name="client">A GitHub client.</param>
        public ObservableActionsWorkflowsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Actions.Workflows;

            Jobs = new ObservableActionsWorkflowJobsClient(client);
            Runs = new ObservableActionsWorkflowRunsClient(client);
        }

        /// <summary>
        /// Manually triggers a GitHub Actions workflow run in a repository by slug.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflows/#create-a-workflow-dispatch-event
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="workflowFileName">The workflow file name.</param>
        /// <param name="createDispatch">The parameters to use to trigger the workflow run.</param>
        public IObservable<Unit> CreateDispatch(string owner, string name, string workflowFileName, CreateWorkflowDispatch createDispatch)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(workflowFileName, nameof(workflowFileName));
            Ensure.ArgumentNotNull(createDispatch, nameof(createDispatch));

            return _client.CreateDispatch(owner, name, workflowFileName, createDispatch).ToObservable();
        }

        /// <summary>
        /// Manually triggers a GitHub Actions workflow run in a repository by slug.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflows/#create-a-workflow-dispatch-event
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="workflowId">The Id of the workflow.</param>
        /// <param name="createDispatch">The parameters to use to trigger the workflow run.</param>
        public IObservable<Unit> CreateDispatch(string owner, string name, long workflowId, CreateWorkflowDispatch createDispatch)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(createDispatch, nameof(createDispatch));

            return _client.CreateDispatch(owner, name, workflowId, createDispatch).ToObservable();
        }

        /// <summary>
        /// Manually triggers a GitHub Actions workflow run in a repository by slug.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflows/#create-a-workflow-dispatch-event
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository.</param>
        /// <param name="workflowFileName">The workflow file name.</param>
        /// <param name="createDispatch">The parameters to use to trigger the workflow run.</param>
        public IObservable<Unit> CreateDispatch(long repositoryId, string workflowFileName, CreateWorkflowDispatch createDispatch)
        {
            Ensure.ArgumentNotNullOrEmptyString(workflowFileName, nameof(workflowFileName));
            Ensure.ArgumentNotNull(createDispatch, nameof(createDispatch));

            return _client.CreateDispatch(repositoryId, workflowFileName, createDispatch).ToObservable();
        }

        /// <summary>
        /// Manually triggers a GitHub Actions workflow run in a repository by Id.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflows/#create-a-workflow-dispatch-event
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository.</param>
        /// <param name="workflowId">The Id of the workflow.</param>
        /// <param name="createDispatch">The parameters to use to trigger the workflow run.</param>
        public IObservable<Unit> CreateDispatch(long repositoryId, long workflowId, CreateWorkflowDispatch createDispatch)
        {
            Ensure.ArgumentNotNull(createDispatch, nameof(createDispatch));

            return _client.CreateDispatch(repositoryId, workflowId, createDispatch).ToObservable();
        }

        /// <summary>
        /// Disables a specific workflow in a repository by Id.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflows/#disable-a-workflow
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="workflowFileName">The workflow file name.</param>
        public IObservable<Unit> Disable(string owner, string name, string workflowFileName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(workflowFileName, nameof(workflowFileName));

            return _client.Disable(owner, name, workflowFileName).ToObservable();
        }

        /// <summary>
        /// Disables a specific workflow in a repository by Id.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflows/#disable-a-workflow
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="workflowId">The Id of the workflow.</param>
        public IObservable<Unit> Disable(string owner, string name, long workflowId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Disable(owner, name, workflowId).ToObservable();
        }

        /// <summary>
        /// Enables a specific workflow in a repository by Id.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflows/#enable-a-workflow
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="workflowFileName">The workflow file name.</param>
        public IObservable<Unit> Enable(string owner, string name, string workflowFileName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(workflowFileName, nameof(workflowFileName));

            return _client.Enable(owner, name, workflowFileName).ToObservable();
        }

        /// <summary>
        /// Enables a specific workflow in a repository by Id.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflows/#enable-a-workflow
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="workflowId">The Id of the workflow.</param>
        public IObservable<Unit> Enable(string owner, string name, long workflowId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Enable(owner, name, workflowId).ToObservable();
        }

        /// <summary>
        /// Gets a specific workflow in a repository by Id. Anyone with read access to the repository can use this endpoint.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflows/#get-a-workflow
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="workflowFileName">The workflow file name.</param>
        public IObservable<Workflow> Get(string owner, string name, string workflowFileName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(workflowFileName, nameof(workflowFileName));

            return _client.Get(owner, name, workflowFileName).ToObservable();
        }

        /// <summary>
        /// Gets a specific workflow in a repository by Id. Anyone with read access to the repository can use this endpoint.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflows/#get-a-workflow
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="workflowId">The Id of the workflow.</param>
        public IObservable<Workflow> Get(string owner, string name, long workflowId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Get(owner, name, workflowId).ToObservable();
        }

        /// <summary>
        /// Gets usage of a specific workflow in a repository by Id. Anyone with read access to the repository can use this endpoint.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflows/#get-workflow-usage
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="workflowFileName">The workflow file name.</param>
        public IObservable<WorkflowUsage> GetUsage(string owner, string name, string workflowFileName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(workflowFileName, nameof(workflowFileName));

            return _client.GetUsage(owner, name, workflowFileName).ToObservable();
        }

        /// <summary>
        /// Gets usage of a specific workflow in a repository by Id. Anyone with read access to the repository can use this endpoint.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflows/#get-workflow-usage
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="workflowId">The Id of the workflow.</param>
        public IObservable<WorkflowUsage> GetUsage(string owner, string name, long workflowId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.GetUsage(owner, name, workflowId).ToObservable();
        }

        /// <summary>
        /// Lists the workflows in a repository. Anyone with read access to the repository can use this endpoint.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflows/#list-repository-workflows
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        public IObservable<WorkflowsResponse> List(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.List(owner, name).ToObservable();
        }

        /// <summary>
        /// Lists the workflows in a repository. Anyone with read access to the repository can use this endpoint.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflows/#list-repository-workflows
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="options">Options to change the API response.</param>
        public IObservable<WorkflowsResponse> List(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _client.List(owner, name, options).ToObservable();
        }

        /// <summary>
        /// Client for the Workflow jobs API.
        /// </summary>
        public IObservableActionsWorkflowJobsClient Jobs { get; private set; }

        /// <summary>
        /// Client for the Workflow runs API.
        /// </summary>
        public IObservableActionsWorkflowRunsClient Runs { get; private set; }
    }
}
