using System;
using System.Linq;
using System.Threading.Tasks;

namespace Octokit
{
    public class WorkflowRunsClient : ApiClient, IWorkflowRunsClient
    {
        public WorkflowRunsClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        public Task<WorkflowRunsResponse> GetAllForRepository(long repositoryId)
            => GetAllForRepository(repositoryId, new WorkflowRunsRequest(), ApiOptions.None);

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

        private async Task<WorkflowRunsResponse> RequestAndReturnWorkflowRunsResponse(Uri uri, WorkflowRunsRequest request, ApiOptions options)
        {
            var results = await ApiConnection.GetAll<WorkflowRunsResponse>(uri, request.ToParametersDictionary(), options);
            return new WorkflowRunsResponse(
                results.Count > 0 ? results.Max(x => x.TotalCount) : 0,
                results.SelectMany(x => x.WorkflowRuns).ToList());
        }
    }
}
