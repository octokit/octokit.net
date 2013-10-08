using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octokit.Internal
{
    public interface ICredentialStore
    {
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification="Nope")]
        Task<Credentials> GetCredentials();
    }
}
