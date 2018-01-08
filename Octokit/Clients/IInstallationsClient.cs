namespace Octokit
{
    public interface IInstallationsClient
    {
        IAccessTokensClient AccessTokens { get; }
    }
}
