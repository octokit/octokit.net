using System.Collections.Generic;
using Nocto.Http;

namespace Nocto.Authentication
{
    public class Authenticator : Middleware
    {
        readonly ICredentialStore credentialStore;

        readonly Dictionary<AuthenticationType, IAuthenticationHandler> authenticators = 
            new Dictionary<AuthenticationType, IAuthenticationHandler>
            {
                {AuthenticationType.Anonymous, new AnonymousAuthenticator()},
                {AuthenticationType.Basic, new BasicAuthenticator()},
                {AuthenticationType.Oauth, new AnonymousAuthenticator()}
            };

        public Authenticator(IApplication app, ICredentialStore credentialStore)
            : base(app)
        {
            Ensure.ArgumentNotNull(credentialStore, "credentialStore");

            this.credentialStore = credentialStore;
        }

        protected override void Before<T>(Environment<T> environment)
        {
            Ensure.ArgumentNotNull(environment, "environment");

            var credentials = credentialStore.GetCredentials();
            authenticators[credentials.AuthenticationType].Authenticate(environment.Request, credentials);
        }

        protected override void After<T>(Environment<T> environment)
        {
        }
    }
}