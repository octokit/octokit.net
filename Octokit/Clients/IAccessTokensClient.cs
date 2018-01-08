using System.Threading.Tasks;

namespace Octokit
{
    public interface IAccessTokensClient
    {
        Task<AccessToken> Create(int installationId);
    }
}
