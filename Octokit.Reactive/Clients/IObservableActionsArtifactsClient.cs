using System;
using System.IO;
using System.Reactive;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Actions Artifacts API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/actions/artifacts/">Actions Artifacts API documentation</a> for more information.
    /// </remarks>
    public interface IObservableActionsArtifactsClient
    {
        IObservable<ListArtifactsResponse> ListArtifacts(string owner, string repository, ListArtifactsRequest listArtifactsRequest = null);
        
        /// <summary>
        /// Gets the specified artifact
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="repository"></param>
        /// <param name="artifactId"></param>
        /// <returns></returns>
        IObservable<Artifact> GetArtifact(string owner, string repository, long artifactId);
        
        /// <summary>
        /// Deletes the specified artifact
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="repository"></param>
        /// <param name="artifactId"></param>
        /// <returns></returns>
        IObservable<Unit> DeleteArtifact(string owner, string repository, long artifactId);
        
        /// <summary>
        /// Downloads the specified artifact's contents
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="repository"></param>
        /// <param name="artifactId"></param>
        /// <param name="archiveFormat"></param>
        /// <returns></returns>
        IObservable<Stream> DownloadArtifact(string owner, string repository, long artifactId, string archiveFormat);
        
        /// <summary>
        /// Lists the artifacts for a specific workflow run
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="repository"></param>
        /// <param name="runId"></param>
        /// <returns></returns>
        IObservable<ListArtifactsResponse> ListWorkflowArtifacts(string owner, string repository, long runId, ListArtifactsRequest listArtifactsRequest = null);
    }
}
