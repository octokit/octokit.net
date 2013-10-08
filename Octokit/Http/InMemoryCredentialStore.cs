namespace Octokit.Http
{
    public class InMemoryCredentialStore : ICredentialStore
    {
        readonly Credentials _credentials;

        public InMemoryCredentialStore(Credentials credentials)
        {
            Ensure.ArgumentNotNull(credentials, "credentials");

            _credentials = credentials;
        }

        public Credentials GetCredentials()
        {
            return _credentials;
        }
    }
}