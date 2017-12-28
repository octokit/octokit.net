namespace Octokit
{
    class InstallationsClient : ApiClient, IInstallationsClient
    {
        public InstallationsClient(IApiConnection apiConnection) : base(apiConnection)
        {
            AccessTokens = new AccessTokensClient(apiConnection);
        }

        public IAccessTokensClient AccessTokens { get; private set; }
    }
}
