using System;
using System.Linq;
using System.Threading.Tasks;

namespace Octokit
{
    public class WorkflowsClient : ApiClient, IWorkflowsClient
    {
        public WorkflowsClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        public Task<WorkflowsResponse> GetAll(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAll(owner, name, ApiOptions.None);
        }

        public Task<WorkflowsResponse> GetAll(long repositoryId)
        {
            return GetAll(repositoryId, ApiOptions.None);
        }

        public Task<WorkflowsResponse> GetAll(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return RequestAndReturnWorkflowsResponse(ApiUrls.Workflows(owner, name), options);
        }

        public Task<WorkflowsResponse> GetAll(long repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return RequestAndReturnWorkflowsResponse(ApiUrls.Workflows(repositoryId), options);
        }

        public Task<Workflow> Get(string owner, string name, long workflowId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Get<Workflow>(ApiUrls.Workflow(owner, name, workflowId));
        }

        public Task<Workflow> Get(string owner, string name, string workflowFileName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(workflowFileName, nameof(workflowFileName));

            return ApiConnection.Get<Workflow>(ApiUrls.Workflow(owner, name, workflowFileName));
        }

        public Task<Workflow> Get(long repositoryId, long workflowId)
        {
            return ApiConnection.Get<Workflow>(ApiUrls.Workflow(repositoryId, workflowId));
        }

        public Task<Workflow> Get(long repositoryId, string workflowFileName)
        {
            Ensure.ArgumentNotNullOrEmptyString(workflowFileName, nameof(workflowFileName));

            return ApiConnection.Get<Workflow>(ApiUrls.Workflow(repositoryId, workflowFileName));
        }

        private async Task<WorkflowsResponse> RequestAndReturnWorkflowsResponse(Uri uri, ApiOptions options)
        {
            var results = await ApiConnection.GetAll<WorkflowsResponse>(uri, options);
            return new WorkflowsResponse(
                results.Count > 0 ? results.Max(x => x.TotalCount) : 0,
                results.SelectMany(x => x.Workflows).ToList());
        }

    }
}
