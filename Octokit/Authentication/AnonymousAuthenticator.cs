using System.Net.Http.Headers;

namespace Octokit.Internal
{
    class AnonymousAuthenticator : IAuthenticationHandler
    {
        public AuthenticationHeaderValue GetAuthorizationHeaderValue(Credentials credentials)
        {
            return null;
        }

        public void Authenticate(IRequest request, Credentials credentials)
        {
            // Do nothing. Retain your anonymity.
        }
    }
}
