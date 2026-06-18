using System;
using System.Threading;
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
        public IObservable<ListArtifactsResponse> ListArtifacts(string owner, string repository, ListArtifactsRequest listArtifactsRequest = null, CancellationToken cancellationToken = default)
        {
            return _client.ListArtifacts(owner, repository, listArtifactsRequest, cancellationToken).ToObservable();
        }

        /// <inheritdoc/>
        public IObservable<Artifact> GetArtifact(string owner, string repository, long artifactId, CancellationToken cancellationToken = default)
        {
            return _client.GetArtifact(owner, repository, artifactId, cancellationToken).ToObservable();
        }

        /// <inheritdoc/>
        public IObservable<Unit> DeleteArtifact(string owner, string repository, long artifactId, CancellationToken cancellationToken = default)
        {
            return _client.DeleteArtifact(owner, repository, artifactId, cancellationToken).ToObservable();
        }

        /// <inheritdoc/>
        public IObservable<Stream> DownloadArtifact(string owner, string repository, long artifactId, string archiveFormat, CancellationToken cancellationToken = default)
        {
            return _client.DownloadArtifact(owner, repository, artifactId, archiveFormat, cancellationToken).ToObservable();
        }
        
        /// <inheritdoc/>
        public IObservable<ListArtifactsResponse> ListWorkflowArtifacts(string owner, string repository, long runId, ListArtifactsRequest listArtifactsRequest = null, CancellationToken cancellationToken = default)
        {
            return _client.ListWorkflowArtifacts(owner, repository, runId, listArtifactsRequest, cancellationToken).ToObservable();
        }
    }
}
