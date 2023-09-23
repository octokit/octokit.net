using System.IO;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Actions Artifacts API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/actions/artifacts/">Actions Artifacts API documentation</a> for more information.
    /// </remarks>
    public class ActionsArtifactsClient : ApiClient, IActionsArtifactsClient
    {
        /// <summary>
        /// Initializes a new GitHub Actions Artifacts API client
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public ActionsArtifactsClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        /// <inheritdoc cref="IActionsArtifactsClient.ListArtifacts"/>
        [ManualRoute("GET", "/repos/{owner}/{repository}/actions/artifacts")]
        public Task<ListArtifactsResponse> ListArtifacts(string owner, string repository)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repository, nameof(repository));
            
            return ApiConnection.Get<ListArtifactsResponse>(ApiUrls.ListArtifacts(owner, repository), null);
        }

        /// <inheritdoc cref="IActionsArtifactsClient.GetArtifact"/>
        [ManualRoute("GET", "/repos/{owner}/{repository}/actions/artifacts/{artifact_id}")]
        public Task<Artifact> GetArtifact(string owner, string repository, int artifactId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repository, nameof(repository));
            Ensure.ArgumentNotNullOrDefault(artifactId, nameof(artifactId));
            
            return ApiConnection.Get<Artifact>(ApiUrls.Artifact(owner, repository, artifactId), null);
        }

        /// <inheritdoc cref="IActionsArtifactsClient.DeleteArtifact"/>
        [ManualRoute("DELETE", "/repos/{owner}/{repository}/actions/artifacts/{artifact_id}")]
        public Task DeleteArtifact(string owner, string repository, int artifactId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repository, nameof(repository));
            Ensure.ArgumentNotNullOrDefault(artifactId, nameof(artifactId));

            return ApiConnection.Delete(ApiUrls.Artifact(owner, repository, artifactId), null);
        }

        /// <inheritdoc cref="IActionsArtifactsClient.DownloadArtifact"/>
        [ManualRoute("GET", "/repos/{owner}/{repository}/actions/artifacts/{artifact_id}/{archive_format}")]
        public Task<Stream> DownloadArtifact(string owner, string repository, int artifactId, string archiveFormat)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repository, nameof(repository));
            Ensure.ArgumentNotNullOrDefault(artifactId, nameof(artifactId));
            Ensure.ArgumentNotNullOrEmptyString(repository, nameof(archiveFormat));
            
            return ApiConnection.GetRawStream(ApiUrls.DownloadArtifact(owner, repository, artifactId, archiveFormat), null);
        }

        /// <inheritdoc cref="IActionsArtifactsClient.ListWorkflowArtifacts"/>
        [ManualRoute("GET", "/repos/{owner}/{repo}/actions/runs/{run_id}/artifacts")]
        public Task<ListArtifactsResponse> ListWorkflowArtifacts(string owner, string repository, int runId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repository, nameof(repository));
            Ensure.ArgumentNotNullOrDefault(runId, nameof(runId));

            return ApiConnection.Get<ListArtifactsResponse>(ApiUrls.ListWorkflowArtifacts(owner, repository, runId), null);
        }
    }
}
