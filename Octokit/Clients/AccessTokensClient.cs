using System.Threading.Tasks;

namespace Octokit
{
    class AccessTokensClient : ApiClient, IAccessTokensClient
    {
        public AccessTokensClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        public Task<AccessToken> Create(long installationId)
        {
            return ApiConnection.Post<AccessToken>(ApiUrls.AccessTokens(installationId), string.Empty, AcceptHeaders.MachineManPreview);
        }
    }
}
