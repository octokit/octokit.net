using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octokit.Internal
{
    public interface ICredentialStore
    {
        Task<Credentials> GetCredentials();
    }
}
