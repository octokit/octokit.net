using Octokit.Http;

namespace Octokit.Authentication
{
    interface IAuthenticationHandler
    {
        void Authenticate(IRequest request, Credentials credentials);
    }
}