using System.Threading.Tasks;

namespace Octokit.Internal
{
    public class InMemoryCredentialStore : ICredentialStore
    {
        readonly Credentials _credentials;

        public InMemoryCredentialStore(Credentials credentials)
        {
            Ensure.ArgumentNotNull(credentials, "credentials");

            _credentials = credentials;
        }

        public Task<Credentials> GetCredentials()
        {
            return Task.Factory.StartNew(() => _credentials);
        }
    }
}