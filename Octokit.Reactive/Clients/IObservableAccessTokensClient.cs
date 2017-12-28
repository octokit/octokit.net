using System;

namespace Octokit.Reactive
{
    public interface IObservableAccessTokensClient
    {
        IObservable<AccessToken> Create(int installationId);
    }
}
