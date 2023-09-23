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

        public Task<ListArtifactsResponse> ListArtifacts(string owner, string repository)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repository, nameof(repository));
            
            return ApiConnection.Get<ListArtifactsResponse>(ApiUrls.ListArtifacts(owner, repository), null);
        }

        public Task<Artifact> GetArtifact(string owner, string repository, int artifactId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repository, nameof(repository));
            Ensure.ArgumentNotNullOrDefault(artifactId, nameof(artifactId));
            
            return ApiConnection.Get<Artifact>(ApiUrls.Artifact(owner, repository, artifactId), null);
        }

        public Task DeleteArtifact(string owner, string repository, int artifactId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repository, nameof(repository));
            Ensure.ArgumentNotNullOrDefault(artifactId, nameof(artifactId));

            return ApiConnection.Delete(ApiUrls.Artifact(owner, repository, artifactId), null);
        }

        public Task<byte[]> DownloadArtifact(string owner, string repository, int artifactId, string archiveFormat)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repository, nameof(repository));
            Ensure.ArgumentNotNullOrDefault(artifactId, nameof(artifactId));
            Ensure.ArgumentNotNullOrEmptyString(repository, nameof(archiveFormat));
            
            return ApiConnection.GetRaw(ApiUrls.DownloadArtifact(owner, repository, artifactId, archiveFormat), null);
        }

        public Task<ListArtifactsResponse> ListWorkflowArtifacts(string owner, string repository, int runId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repository, nameof(repository));
            Ensure.ArgumentNotNullOrDefault(runId, nameof(runId));

            return ApiConnection.Get<ListArtifactsResponse>(ApiUrls.ListWorkflowArtifacts(owner, repository, runId), null);
        }
    }
}
