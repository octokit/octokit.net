namespace Octokit.Reactive
{
    public interface IObservableInstallationsClient
    {
        IObservableAccessTokensClient AccessTokens { get; }
    }
}
