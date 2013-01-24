using Nocto.Http;

namespace Nocto.Authentication
{
    public interface IAuthenticationHandler
    {
        void Authenticate(IRequest request, Credentials credentials);
    }
}