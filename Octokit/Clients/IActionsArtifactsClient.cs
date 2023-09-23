using System.Collections.Generic;
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
        Task<ListArtifactsResponse> ListArtifacts(string owner, string repository);
        Task<Artifact> GetArtifact(string owner, string repository, int artifactId);
        Task DeleteArtifact(string owner, string repository, int artifactId);
        Task<byte[]> DownloadArtifact(string owner, string repository, int artifactId, string archiveFormat);
        Task<ListArtifactsResponse> ListWorkflowArtifacts(string owner, string repository, int runId);
    }
}
