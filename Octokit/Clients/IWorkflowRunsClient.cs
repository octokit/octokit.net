using System.Threading.Tasks;

namespace Octokit
{
    public interface IWorkflowRunsClient
    {
        Task<WorkflowRunsResponse> GetAllForRepository(long repositoryId);
        Task<WorkflowRunsResponse> GetAllForRepository(long repositoryId, WorkflowRunsRequest request);
        Task<WorkflowRunsResponse> GetAllForRepository(long repositoryId, WorkflowRunsRequest request, ApiOptions options);

        Task<WorkflowRunsResponse> GetAllForRepository(string owner, string name);
        Task<WorkflowRunsResponse> GetAllForRepository(string owner, string name, WorkflowRunsRequest request);
        Task<WorkflowRunsResponse> GetAllForRepository(string owner, string name, WorkflowRunsRequest request, ApiOptions options);
    }
}
