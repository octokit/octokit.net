using System.Threading.Tasks;

namespace Octokit
{
    public interface IWorkflowRunsClient
    {
        Task<WorkflowRunsResponse> GetAllForWorkflowId(string owner, string name, long workflowId);
        Task<WorkflowRunsResponse> GetAllForWorkflowId(long repositoryId, long workflowId);

        Task<WorkflowRunsResponse> GetAllForWorkflowId(string owner, string name, long workflowId, WorkflowRunsRequest request);
        Task<WorkflowRunsResponse> GetAllForWorkflowId(long repositoryId, long workflowId, WorkflowRunsRequest request);

        Task<WorkflowRunsResponse> GetAllForWorkflowId(string owner, string name, long workflowId, WorkflowRunsRequest request, ApiOptions options);
        Task<WorkflowRunsResponse> GetAllForWorkflowId(long repositoryId, long workflowId, WorkflowRunsRequest request, ApiOptions options);

        Task<WorkflowRunsResponse> GetAllForRepository(string owner, string name);
        Task<WorkflowRunsResponse> GetAllForRepository(long repositoryId);

        Task<WorkflowRunsResponse> GetAllForRepository(string owner, string name, WorkflowRunsRequest request);
        Task<WorkflowRunsResponse> GetAllForRepository(long repositoryId, WorkflowRunsRequest request);

        Task<WorkflowRunsResponse> GetAllForRepository(string owner, string name, WorkflowRunsRequest request, ApiOptions options);
        Task<WorkflowRunsResponse> GetAllForRepository(long repositoryId, WorkflowRunsRequest request, ApiOptions options);

        Task<WorkflowRun> Get(string owner, string name, long runId);
        Task<WorkflowRun> Get(long repositoryId, long runId);

        Task<bool> ReRun(string owner, string name, long runId);

        Task<bool> ReRun(long repositoryId, long runId);

        Task<bool> Cancel(string owner, string name, long runId);

        Task<bool> Cancel(long repositoryId, long runId);

        Task<string> LogsUrl(string owner, string name, long runId);

        Task<string> LogsUrl(long repositoryId, long runId);

    }
}
