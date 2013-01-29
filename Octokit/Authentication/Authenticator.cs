using System.Collections.Generic;
using Octokit.Http;

namespace Octokit.Authentication
{
    class Authenticator
    {
        readonly Dictionary<AuthenticationType, IAuthenticationHandler> authenticators =
            new Dictionary<AuthenticationType, IAuthenticationHandler>
            {
                { AuthenticationType.Anonymous, new AnonymousAuthenticator() },
                { AuthenticationType.Basic, new BasicAuthenticator() },
                { AuthenticationType.Oauth, new AnonymousAuthenticator() }
            };

        public Authenticator(ICredentialStore credentialStore)
        {
            Ensure.ArgumentNotNull(credentialStore, "credentialStore");

            CredentialStore = credentialStore;
        }

        public void Apply(IRequest request)
        {
            Ensure.ArgumentNotNull(request, "request");

            // TODO: What if the credentials simply don't exist? We should probably 
            // throw an exception that can be handled and provide guidance to 
            // ICredentialStore implementors.
            var credentials = CredentialStore.GetCredentials() ?? Credentials.Anonymous;
            authenticators[credentials.AuthenticationType].Authenticate(request, credentials);
        }

        public ICredentialStore CredentialStore { get; set; }
    }
}
