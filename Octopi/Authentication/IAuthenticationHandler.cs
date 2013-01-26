using Octopi.Http;

namespace Octopi.Authentication
{
    public interface IAuthenticationHandler
    {
        void Authenticate(IRequest request, Credentials credentials);
    }
}