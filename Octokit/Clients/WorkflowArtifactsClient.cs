using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Octokit
{
    public class WorkflowArtifactsClient : ApiClient, IWorkflowArtifactsClient
    {
        public WorkflowArtifactsClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        public Task Delete(string owner, string name, long artifactId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Delete(ApiUrls.WorkflowArtifact(owner, name, artifactId));
        }

        public Task Delete(long repositoryId, long artifactId)
        {
            return ApiConnection.Delete(ApiUrls.WorkflowArtifact(repositoryId, artifactId));
        }

        public Task<string> DownloadUrl(string owner, string name, long artifactId, string archiveFormat)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(archiveFormat, nameof(archiveFormat));

            return RequestAndReturnDownloadUrl(ApiUrls.WorkflowArtifactDownloadUrl(owner, name, artifactId, archiveFormat));
        }

        public Task<string> DownloadUrl(long repositoryId, long artifactId, string archiveFormat)
        {
            Ensure.ArgumentNotNullOrEmptyString(archiveFormat, nameof(archiveFormat));

            return RequestAndReturnDownloadUrl(ApiUrls.WorkflowArtifactDownloadUrl(repositoryId, artifactId, archiveFormat));
        }

        public Task<WorkflowArtifact> Get(string owner, string name, long artifactId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Get<WorkflowArtifact>(ApiUrls.WorkflowArtifact(owner, name, artifactId));
        }

        public Task<WorkflowArtifact> Get(long repositoryId, long artifactId)
        {
            return ApiConnection.Get<WorkflowArtifact>(ApiUrls.WorkflowArtifact(repositoryId, artifactId));
        }

        public Task<WorkflowArtifactsResponse> GetAll(string owner, string name, long runId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAll(owner, name, runId, ApiOptions.None);
        }

        public Task<WorkflowArtifactsResponse> GetAll(long repositoryId, long runId)
        {
            return GetAll(repositoryId, runId, ApiOptions.None);
        }

        public Task<WorkflowArtifactsResponse> GetAll(string owner, string name, long runId, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return RequestAndReturnWorkflowArtifactsResponse(ApiUrls.WorkflowArtifacts(owner, name, runId), options);
        }

        public Task<WorkflowArtifactsResponse> GetAll(long repositoryId, long runId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return RequestAndReturnWorkflowArtifactsResponse(ApiUrls.WorkflowArtifacts(repositoryId, runId), options);
        }

        private async Task<string> RequestAndReturnDownloadUrl(Uri uri)
        {
            var response = await Connection.Get<object>(uri, null, null).ConfigureAwait(false);
            var statusCode = response.HttpResponse.StatusCode;
            if (statusCode != HttpStatusCode.Found)
            {
                throw new ApiException("Invalid status code returned. Expected a 302", statusCode);
            }
            return response.HttpResponse.Headers.SafeGet("Location");
        }

        private async Task<WorkflowArtifactsResponse> RequestAndReturnWorkflowArtifactsResponse(Uri uri, ApiOptions options)
        {
            var results = await ApiConnection.GetAll<WorkflowArtifactsResponse>(uri, options);
            return new WorkflowArtifactsResponse(
                results.Count > 0 ? results.Max(x => x.TotalCount) : 0,
                results.SelectMany(x => x.Artifacts).ToList());
        }
    }
}
