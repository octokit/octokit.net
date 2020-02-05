using System;
using System.Reactive;

namespace Octokit.Reactive
{
    public interface IObservableWorkflowArtifactsClient
    {
        IObservable<WorkflowArtifactsResponse> GetAll(string owner, string name, long runId);
        IObservable<WorkflowArtifactsResponse> GetAll(long repositoryId, long runId);

        IObservable<WorkflowArtifactsResponse> GetAll(string owner, string name, long runId, ApiOptions options);
        IObservable<WorkflowArtifactsResponse> GetAll(long repositoryId, long runId, ApiOptions options);

        IObservable<WorkflowArtifact> Get(string owner, string name, long artifactId);
        IObservable<WorkflowArtifact> Get(long repositoryId, long artifactId);

        IObservable<string> DownloadUrl(string owner, string name, long artifactId, string archiveFormat);
        IObservable<string> DownloadUrl(long repositoryId, long artifactId, string archiveFormat);

        IObservable<Unit> Delete(string name, string owner, long artifactId);
        IObservable<Unit> Delete(long repositoryId, long artifactId);
    }
}
