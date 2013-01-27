using Octopi.Http;

namespace Octopi.Authentication
{
    class AnonymousAuthenticator : IAuthenticationHandler
    {
        public void Authenticate(IRequest request, Credentials credentials)
        {
            // Do nothing. Retain your anonymity.
        }
    }
}
