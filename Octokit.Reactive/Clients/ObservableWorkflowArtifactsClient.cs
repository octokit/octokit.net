using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservableWorkflowArtifactsClient : IObservableWorkflowArtifactsClient
    {
        readonly IConnection _connection;
        readonly IWorkflowArtifactsClient _client;

        public ObservableWorkflowArtifactsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _connection = client.Connection;
            _client = client.Action.Artifact;
        }

        public IObservable<Unit> Delete(string name, string owner, long artifactId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Delete(owner, name, artifactId).ToObservable();
        }

        public IObservable<Unit> Delete(long repositoryId, long artifactId)
        {
            return _client.Delete(repositoryId, artifactId).ToObservable();
        }

        public IObservable<string> DownloadUrl(string owner, string name, long artifactId, string archiveFormat)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.DownloadUrl(owner, name, artifactId, archiveFormat).ToObservable();
        }

        public IObservable<string> DownloadUrl(long repositoryId, long artifactId, string archiveFormat)
        {
            return _client.DownloadUrl(repositoryId, artifactId, archiveFormat).ToObservable();
        }

        public IObservable<WorkflowArtifact> Get(string owner, string name, long artifactId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Get(owner, name, artifactId).ToObservable();
        }

        public IObservable<WorkflowArtifact> Get(long repositoryId, long artifactId)
        {
            return _client.Get(repositoryId, artifactId).ToObservable();
        }

        public IObservable<WorkflowArtifactsResponse> GetAll(string owner, string name, long runId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAll(owner, name, runId, ApiOptions.None);
        }

        public IObservable<WorkflowArtifactsResponse> GetAll(long repositoryId, long runId)
        {
            return GetAll(repositoryId, runId, ApiOptions.None);
        }

        public IObservable<WorkflowArtifactsResponse> GetAll(string owner, string name, long runId, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _connection.GetAndFlattenAllPages<WorkflowArtifactsResponse>(ApiUrls.WorkflowArtifacts(owner, name, runId), options);

        }

        public IObservable<WorkflowArtifactsResponse> GetAll(long repositoryId, long runId, ApiOptions options)
        {
            return _connection.GetAndFlattenAllPages<WorkflowArtifactsResponse>(ApiUrls.WorkflowArtifacts(repositoryId, runId), options);
        }
    }
}