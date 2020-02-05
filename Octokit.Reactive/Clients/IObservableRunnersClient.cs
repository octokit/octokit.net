using System;
using System.Reactive;

namespace Octokit.Reactive
{
    public interface IObservableRunnersClient
    {
        IObservable<RunnerDownload> GetAllDownloads(string owner, string name);
        IObservable<RunnerDownload> GetAllDownloads(long repositoryId);

        IObservable<RunnerRegistrationToken> CreateRegistrationToken(string owner, string name);
        IObservable<RunnerRegistrationToken> CreateRegistrationToken(long repositoryId);

        IObservable<Runner> GetAll(string owner, string name);
        IObservable<Runner> GetAll(long repositoryId);

        IObservable<Runner> GetAll(string owner, string name, ApiOptions options);
        IObservable<Runner> GetAll(long repositoryId, ApiOptions options);

        IObservable<Runner> Get(string owner, string name, long runnerId);
        IObservable<Runner> Get(long repositoryId, long runnerId);

        IObservable<RunnerRemoveToken> CreateRemoveToken(string owner, string name);
        IObservable<RunnerRemoveToken> CreateRemoveToken(long repositoryId);

        IObservable<Unit> Remove(string owner, string name, long runnerId);
        IObservable<Unit> Remove(long repositoryId, long runnerId);

    }
}
