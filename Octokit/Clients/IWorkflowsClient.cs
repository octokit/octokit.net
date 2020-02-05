using System.Threading.Tasks;

namespace Octokit
{
    public interface IWorkflowsClient
    {
        Task<WorkflowsResponse> GetAll(string owner, string name);

        Task<WorkflowsResponse> GetAll(long repositoryId);

        Task<WorkflowsResponse> GetAll(string owner, string name, ApiOptions options);

        Task<WorkflowsResponse> GetAll(long repositoryId, ApiOptions options);

        Task<Workflow> Get(string owner, string name, long workflowId);

        Task<Workflow> Get(string owner, string name, string workflowFileName);

        Task<Workflow> Get(long repositoryId, long workflowId);

        Task<Workflow> Get(long repositoryId, string workflowFileName);
    }
}
