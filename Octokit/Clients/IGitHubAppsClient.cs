using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public interface IGitHubAppsClient
    {
        Task<GitHubApp> Get(string slug);
        Task<GitHubApp> GetCurrent();
        Task<AccessToken> CreateInstallationToken(long installationId);
        Task<IReadOnlyList<Installation>> GetAllInstallationsForCurrent();
        Task<IReadOnlyList<Installation>> GetAllInstallationsForCurrent(ApiOptions options);
    }
}