using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public interface IInstallationsClient
    {
        Task<IReadOnlyList<Installation>> GetInstallations();
        IAccessTokensClient AccessTokens { get; }
    }
}
