using System;
using System.IO;
using System.Reactive;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    public class ObservableActionsArtifactsClient : IObservableActionsArtifactsClient
    {
        readonly IActionsArtifactsClient _client;

        /// <summary>
        /// Instantiate a new GitHub Actions Artifacts API client.
        /// </summary>
        /// <param name="client">A GitHub client.</param>
        public ObservableActionsArtifactsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Actions.Artifacts;
        }

        /// <inheritdoc/>
        public IObservable<ListArtifactsResponse> ListArtifacts(string owner, string repository)
        {
            return _client.ListArtifacts(owner, repository).ToObservable();
        }

        /// <inheritdoc/>
        public IObservable<Artifact> GetArtifact(string owner, string repository, int artifactId)
        {
            return _client.GetArtifact(owner, repository, artifactId).ToObservable();
        }

        /// <inheritdoc/>
        public IObservable<Unit> DeleteArtifact(string owner, string repository, int artifactId)
        {
            return _client.DeleteArtifact(owner, repository, artifactId).ToObservable();
        }

        /// <inheritdoc/>
        public IObservable<Stream> DownloadArtifact(string owner, string repository, int artifactId, string archiveFormat)
        {
            return _client.DownloadArtifact(owner, repository, artifactId, archiveFormat).ToObservable();
        }
        
        /// <inheritdoc/>
        public IObservable<ListArtifactsResponse> ListWorkflowArtifacts(string owner, string repository, int runId)
        {
            return _client.ListWorkflowArtifacts(owner, repository, runId).ToObservable();
        }
    }
}
