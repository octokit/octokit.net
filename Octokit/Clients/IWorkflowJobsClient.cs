using System;
using System.Threading.Tasks;

namespace Octokit
{
    public interface IWorkflowJobsClient
    {
        Task<WorkflowJobsResponse> GetAll(string owner, string name, long runId);
        Task<WorkflowJobsResponse> GetAll(long repositoryId, long runId);

        Task<WorkflowJobsResponse> GetAll(string owner, string name, long runId, ApiOptions options);
        Task<WorkflowJobsResponse> GetAll(long repositoryId, long runId, ApiOptions options);

        Task<WorkflowJob> Get(string owner, string name, long jobId);
        Task<WorkflowJob> Get(long repositoryId, long jobId);

        Task<string> LogsUrl(string owner, string name, long jobId);
        Task<string> LogsUrl(long repositoryId, long jobId);

    }
}
