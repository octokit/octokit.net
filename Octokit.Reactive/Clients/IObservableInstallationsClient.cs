using System;

namespace Octokit.Reactive
{
    public interface IObservableInstallationsClient
    {
        IObservableAccessTokensClient AccessTokens { get; }
        IObservable<Installation> GetAll();
        IObservable<Installation> GetAll(ApiOptions options);
    }
}
