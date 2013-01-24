using System.Threading.Tasks;

namespace Nocto.Http
{
    public class InMemoryCredentialStore : ICredentialStore
    {
        readonly Credentials credentials;

        public InMemoryCredentialStore(Credentials credentials)
        {
            Ensure.ArgumentNotNull(credentials, "credentials");

            this.credentials = credentials;
        }

        public Credentials GetCredentials()
        {
            return credentials;
        }
    }
}