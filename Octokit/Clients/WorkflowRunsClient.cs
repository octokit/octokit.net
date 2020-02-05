using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Octokit.Internal;

namespace Octokit
{
    public class WorkflowRunsClient : ApiClient, IWorkflowRunsClient
    {
        public WorkflowRunsClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        public Task<WorkflowRunsResponse> GetAllForRepository(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllForRepository(owner, name, new WorkflowRunsRequest(), ApiOptions.None);
        }

        public Task<WorkflowRunsResponse> GetAllForRepository(string owner, string name, WorkflowRunsRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAllForRepository(owner, name, request, ApiOptions.None);
        }

        public Task<WorkflowRunsResponse> GetAllForRepository(string owner, string name, WorkflowRunsRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return RequestAndReturnWorkflowRunsResponse(ApiUrls.WorkflowRunsForRepository(owner, name), request, options);
        }

        public Task<WorkflowRunsResponse> GetAllForRepository(long repositoryId)
        {
            return GetAllForRepository(repositoryId, new WorkflowRunsRequest(), ApiOptions.None);
        }

        public Task<WorkflowRunsResponse> GetAllForRepository(long repositoryId, WorkflowRunsRequest request)
        {
            Ensure.ArgumentNotNull(request, nameof(request));
            return GetAllForRepository(repositoryId, request, ApiOptions.None);
        }

        public Task<WorkflowRunsResponse> GetAllForRepository(long repositoryId, WorkflowRunsRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return RequestAndReturnWorkflowRunsResponse(ApiUrls.WorkflowRunsForRepository(repositoryId), request, options);
        }

        public Task<WorkflowRunsResponse> GetAllForWorkflowId(string owner, string name, long workflowId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllForWorkflowId(owner, name, workflowId, new WorkflowRunsRequest(), ApiOptions.None);
        }

        public Task<WorkflowRunsResponse> GetAllForWorkflowId(string owner, string name, long workflowId, WorkflowRunsRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAllForWorkflowId(owner, name, workflowId, request, ApiOptions.None);
        }

        public Task<WorkflowRunsResponse> GetAllForWorkflowId(string owner, string name, long workflowId, WorkflowRunsRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return RequestAndReturnWorkflowRunsResponse(ApiUrls.WorkflowRuns(owner, name, workflowId), request, options);
        }

        public Task<WorkflowRunsResponse> GetAllForWorkflowId(long repositoryId, long workflowId)
        {
            return GetAllForWorkflowId(repositoryId, workflowId, new WorkflowRunsRequest(), ApiOptions.None);
        }

        public Task<WorkflowRunsResponse> GetAllForWorkflowId(long repositoryId, long workflowId, WorkflowRunsRequest request)
        {
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAllForWorkflowId(repositoryId, workflowId, request, ApiOptions.None);
        }

        public Task<WorkflowRunsResponse> GetAllForWorkflowId(long repositoryId, long workflowId, WorkflowRunsRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return RequestAndReturnWorkflowRunsResponse(ApiUrls.WorkflowRuns(repositoryId, workflowId), request, options);
        }

        public Task<WorkflowRun> Get(string owner, string name, long runId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Get<WorkflowRun>(ApiUrls.WorkflowRun(owner, name, runId));
        }

        public Task<WorkflowRun> Get(long repositoryId, long runId)
        {
            return ApiConnection.Get<WorkflowRun>(ApiUrls.WorkflowRun(repositoryId, runId));
        }


        public async Task<bool> ReRun(string owner, string name, long runId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            var httpStatusCode = await Connection.Post(ApiUrls.WorkflowRunReRun(owner, name, runId)).ConfigureAwait(false);
            if (httpStatusCode != HttpStatusCode.Created)
            {
                throw new ApiException("Invalid status code returned. Expected a 201", httpStatusCode);
            }
            return httpStatusCode == HttpStatusCode.Created;
        }

        public async Task<bool> ReRun(long repositoryId, long runId)
        {
            var httpStatusCode = await Connection.Post(ApiUrls.WorkflowRunReRun(repositoryId, runId)).ConfigureAwait(false);
            if (httpStatusCode != HttpStatusCode.Created)
            {
                throw new ApiException("Invalid status code returned. Expected a 201", httpStatusCode);
            }
            return httpStatusCode == HttpStatusCode.Created;
        }


        public async Task<bool> Cancel(string owner, string name, long runId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            var httpStatusCode = await Connection.Post(ApiUrls.WorkflowRunCancel(owner, name, runId)).ConfigureAwait(false);
            if (httpStatusCode != HttpStatusCode.Accepted)
            {
                throw new ApiException("Invalid status code returned. Expected a 202", httpStatusCode);
            }
            return httpStatusCode == HttpStatusCode.Accepted;
        }

        public async Task<bool> Cancel(long repositoryId, long runId)
        {
            var httpStatusCode = await Connection.Post(ApiUrls.WorkflowRunCancel(repositoryId, runId)).ConfigureAwait(false);
            if (httpStatusCode != HttpStatusCode.Accepted)
            {
                throw new ApiException("Invalid status code returned. Expected a 202", httpStatusCode);
            }
            return httpStatusCode == HttpStatusCode.Accepted;
        }

        public async Task<string> LogsUrl(string owner, string name, long runId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            // FIXME: Is this idiomatic?
            var response = await Connection.Get<object>(ApiUrls.WorkflowRunLogs(owner, name, runId), null, null).ConfigureAwait(false);
            var statusCode = response.HttpResponse.StatusCode;
            if (statusCode != HttpStatusCode.Found)
            {
                throw new ApiException("Invalid status code returned. Expected a 302", statusCode);
            }
            return response.HttpResponse.Headers.SafeGet("Location");
        }

        public async Task<string> LogsUrl(long repositoryId, long runId)
        {
            var response = await Connection.Get<object>(ApiUrls.WorkflowRunLogs(repositoryId, runId), null, null).ConfigureAwait(false);
            var statusCode = response.HttpResponse.StatusCode;
            if (statusCode != HttpStatusCode.Found)
            {
                throw new ApiException("Invalid status code returned. Expected a 302", statusCode);
            }
            return response.HttpResponse.Headers.SafeGet("Location");
        }

        // Private support methods
        private async Task<WorkflowRunsResponse> RequestAndReturnWorkflowRunsResponse(Uri uri, WorkflowRunsRequest request, ApiOptions options)
        {
            var results = await ApiConnection.GetAll<WorkflowRunsResponse>(uri, request.ToParametersDictionary(), options);
            return new WorkflowRunsResponse(
                results.Count > 0 ? results.Max(x => x.TotalCount) : 0,
                results.SelectMany(x => x.WorkflowRuns).ToList());
        }
    }
}