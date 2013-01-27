using Octopi.Http;

namespace Octopi.Authentication
{
    interface IAuthenticationHandler
    {
        void Authenticate(IRequest request, Credentials credentials);
    }
}