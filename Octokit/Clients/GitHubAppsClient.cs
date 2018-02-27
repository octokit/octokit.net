using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public class GitHubAppsClient : ApiClient, IGitHubAppsClient
    {
        public GitHubAppsClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        public Task<GitHubApp> Get(string slug)
        {
            return ApiConnection.Get<GitHubApp>(ApiUrls.App(slug), null, AcceptHeaders.MachineManPreview);
        }

        public Task<GitHubApp> GetCurrent()
        {
            return ApiConnection.Get<GitHubApp>(ApiUrls.App(), null, AcceptHeaders.MachineManPreview);
        }

        public Task<AccessToken> CreateInstallationToken(long installationId)
        {
            return ApiConnection.Post<AccessToken>(ApiUrls.AccessTokens(installationId), string.Empty, AcceptHeaders.MachineManPreview);
        }

        public Task<IReadOnlyList<Installation>> GetAllInstallationsForCurrent()
        {
            return ApiConnection.GetAll<Installation>(ApiUrls.Installations(), null, AcceptHeaders.MachineManPreview);
        }

        public Task<IReadOnlyList<Installation>> GetAllInstallationsForCurrent(ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Installation>(ApiUrls.Installations(), null, AcceptHeaders.MachineManPreview, options);
        }
    }
}