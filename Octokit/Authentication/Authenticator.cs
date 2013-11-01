using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit.Internal
{
    class Authenticator
    {
        readonly Dictionary<AuthenticationType, IAuthenticationHandler> authenticators =
            new Dictionary<AuthenticationType, IAuthenticationHandler>
            {
                { AuthenticationType.Anonymous, new AnonymousAuthenticator() },
                { AuthenticationType.Basic, new BasicAuthenticator() },
                { AuthenticationType.Oauth, new TokenAuthenticator() }
            };

        public Authenticator(ICredentialStore credentialStore)
        {
            Ensure.ArgumentNotNull(credentialStore, "credentialStore");

            CredentialStore = credentialStore;
        }

        public async Task Apply(IRequest request)
        {
            Ensure.ArgumentNotNull(request, "request");

            var credentials = await CredentialStore.GetCredentials().ConfigureAwait(false) ?? Credentials.Anonymous;
            authenticators[credentials.AuthenticationType].Authenticate(request, credentials);
        }

        public ICredentialStore CredentialStore { get; set; }
    }
}
