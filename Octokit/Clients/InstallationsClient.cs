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

        public Task<IReadOnlyList<Installation>> GetAll()
        {
            return ApiConnection.GetAll<Installation>(ApiUrls.Installations(), null, AcceptHeaders.MachineManPreview);
        }

        public Task<IReadOnlyList<Installation>> GetAll(ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<Installation>(ApiUrls.Installations(), null, AcceptHeaders.MachineManPreview, options);
        }
    }
}
