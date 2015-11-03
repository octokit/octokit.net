using System.Threading.Tasks;

namespace Octokit.Internal
{
    /// <summary>
    /// Abstraction for interacting with credentials
    /// </summary>
    public class InMemoryCredentialStore : ICredentialStore
    {
        readonly Credentials _credentials;

        /// <summary>
        /// Create an instance of the InMemoryCredentialStore
        /// </summary>
        /// <param name="credentials"></param>
        public InMemoryCredentialStore(Credentials credentials)
        {
            Ensure.ArgumentNotNull(credentials, "credentials");

            _credentials = credentials;
        }

        /// <summary>
        /// Retrieve the credentials from the underlying store
        /// </summary>
        /// <returns>A continuation containing credentials</returns>
        public Task<Credentials> GetCredentials()
        {
            return Task.FromResult(_credentials);
        }
    }
}