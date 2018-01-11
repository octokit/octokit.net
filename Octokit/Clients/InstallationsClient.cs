using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    class InstallationsClient : ApiClient, IInstallationsClient
    {
        public InstallationsClient(IApiConnection apiConnection) : base(apiConnection)
        {
            AccessTokens = new AccessTokensClient(apiConnection);
        }

        public IAccessTokensClient AccessTokens { get; private set; }

        public Task<IReadOnlyList<Installation>> GetInstallations()
        {
            return ApiConnection.GetAll<Installation>(ApiUrls.Installations(), null, AcceptHeaders.MachineManPreview);
        }
    }
}
