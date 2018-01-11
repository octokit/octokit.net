using System.Threading.Tasks;

namespace Octokit
{
    class AccessTokensClient : ApiClient, IAccessTokensClient
    {
        private const string AcceptHeader = "application/vnd.github.machine-man-preview+json";

        public AccessTokensClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        public Task<AccessToken> Create(int installationId)
        {
            return ApiConnection.Post<AccessToken>(ApiUrls.AccessTokens(installationId), "", AcceptHeader);
        }
    }
}
