using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// Abstraction for interacting with credentials
    /// </summary>
    public interface ICredentialStore
    {
        /// <summary>
        /// Retrieve the credentials from the underlying store
        /// </summary>
        /// <returns>A continuation containing credentials</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Nope")]
        Task<Credentials> GetCredentials();
    }
}
