using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public interface IGitHubAppsClient
    {
        Task<Application> Get(string slug);
        Task<Application> GetCurrent();
        Task<AccessToken> CreateInstallationToken(long installationId);
        Task<IReadOnlyList<Installation>> GetAllInstallationsForCurrent();
        Task<IReadOnlyList<Installation>> GetAllInstallationsForCurrent(ApiOptions options);
    }
}