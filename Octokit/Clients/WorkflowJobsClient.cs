using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Octokit
{
    public class WorkflowJobsClient : ApiClient, IWorkflowJobsClient
    {
        public WorkflowJobsClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        public Task<WorkflowJob> Get(string owner, string name, long jobId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Get<WorkflowJob>(ApiUrls.WorkflowJob(owner, name, jobId));
        }

        public Task<WorkflowJob> Get(long repositoryId, long jobId)
        {
            return ApiConnection.Get<WorkflowJob>(ApiUrls.WorkflowJob(repositoryId, jobId));
        }

        public Task<WorkflowJobsResponse> GetAll(string owner, string name, long runId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAll(owner, name, runId, ApiOptions.None);
        }

        public Task<WorkflowJobsResponse> GetAll(long repositoryId, long runId)
        {
            return GetAll(repositoryId, runId, ApiOptions.None);
        }

        public Task<WorkflowJobsResponse> GetAll(string owner, string name, long runId, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return RequestAndReturnWorkflowJobsResponse(ApiUrls.WorkflowRunJobs(owner, name, runId), options);
        }

        public Task<WorkflowJobsResponse> GetAll(long repositoryId, long runId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return RequestAndReturnWorkflowJobsResponse(ApiUrls.WorkflowRunJobs(repositoryId, runId), options);
        }

        public Task<string> LogsUrl(string owner, string name, long jobId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return RequestAndReturnLogsUrl(ApiUrls.WorkflowJobLogs(owner, name, jobId));
        }

        public Task<string> LogsUrl(long repositoryId, long jobId)
        {
            return RequestAndReturnLogsUrl(ApiUrls.WorkflowJobLogs(repositoryId, jobId));
        }

        private async Task<string> RequestAndReturnLogsUrl(Uri uri)
        {
            var response = await Connection.Get<object>(uri, null, null).ConfigureAwait(false);
            var statusCode = response.HttpResponse.StatusCode;
            if (statusCode != HttpStatusCode.Found)
            {
                throw new ApiException("Invalid status code returned. Expected a 302", statusCode);
            }
            return response.HttpResponse.Headers.SafeGet("Location");
        }

        private async Task<WorkflowJobsResponse> RequestAndReturnWorkflowJobsResponse(Uri uri, ApiOptions options)
        {
            var results = await ApiConnection.GetAll<WorkflowJobsResponse>(uri, options);
            return new WorkflowJobsResponse(
                results.Count > 0 ? results.Max(x => x.TotalCount) : 0,
                results.SelectMany(x => x.WorkflowJobs).ToList());
        }
    }
}
