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
    public interface IActionsArtifactsClient
    {
        /// <summary>
        /// Lists artifacts for a repository
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="repository"></param>
        /// <returns></returns>
        Task<ListArtifactsResponse> ListArtifacts(string owner, string repository, ListArtifactsRequest listArtifactsRequest = null);
        
        /// <summary>
        /// Gets the specified artifact
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="repository"></param>
        /// <param name="artifactId"></param>
        /// <returns></returns>
        Task<Artifact> GetArtifact(string owner, string repository, long artifactId);
        
        /// <summary>
        /// Deletes the specified artifact
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="repository"></param>
        /// <param name="artifactId"></param>
        /// <returns></returns>
        Task DeleteArtifact(string owner, string repository, long artifactId);
        
        /// <summary>
        /// Downloads the specified artifact's contents
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="repository"></param>
        /// <param name="artifactId"></param>
        /// <param name="archiveFormat"></param>
        /// <returns></returns>
        Task<Stream> DownloadArtifact(string owner, string repository, long artifactId, string archiveFormat);
        
        /// <summary>
        /// Lists the artifacts for a specific workflow run
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="repository"></param>
        /// <param name="runId"></param>
        /// <returns></returns>
        Task<ListArtifactsResponse> ListWorkflowArtifacts(string owner, string repository, long runId, ListArtifactsRequest listArtifactsRequest = null);
    }
}
