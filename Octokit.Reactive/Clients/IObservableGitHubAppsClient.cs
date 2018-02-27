using System;

namespace Octokit.Reactive
{
    public interface IObservableGitHubAppsClient
    {
        IObservable<GitHubApp> GetCurrent();
        IObservable<AccessToken> CreateInstallationToken(long installationId);
        IObservable<Installation> GetAllInstallationsForCurrent();
        IObservable<Installation> GetAllInstallationsForCurrent(ApiOptions options);
    }
}
