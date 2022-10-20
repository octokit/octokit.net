using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Actions Workflow jobs API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/actions/workflow-jobs/">Actions Workflow jobs API documentation</a> for more information.
    /// </remarks>
    public class ObservableActionsWorkflowJobsClient : IObservableActionsWorkflowJobsClient
    {
        readonly IActionsWorkflowJobsClient _client;

        /// <summary>
        /// Instantiate a new GitHub Actions Workflows jobs API client.
        /// </summary>
        /// <param name="client">A GitHub client.</param>
        public ObservableActionsWorkflowJobsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Actions.Workflows.Jobs;
        }

        /// <summary>
        /// Re-runs a specific workflow job in a repository.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/actions/workflow-runs/#re-run-a-job-from-a-workflow-run
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="jobId">The Id of the workflow job.</param>
        public IObservable<Unit> Rerun(string owner, string name, long jobId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Rerun(owner, name, jobId).ToObservable();
        }
    }
}
