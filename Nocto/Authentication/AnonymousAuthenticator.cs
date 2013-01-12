using Nocto.Http;

namespace Nocto.Authentication
{
    public class AnonymousAuthenticator : IAuthenticationHandler
    {
        public void Authenticate(IRequest request, Credentials credentials)
        {
            // Do nothing. Retain your anonymity.
        }
    }
}
