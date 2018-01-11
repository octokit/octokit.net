using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public interface IInstallationsClient
    {
        IAccessTokensClient AccessTokens { get; }

        Task<IReadOnlyList<Installation>> GetAll();
        Task<IReadOnlyList<Installation>> GetAll(ApiOptions options);
    }
}
