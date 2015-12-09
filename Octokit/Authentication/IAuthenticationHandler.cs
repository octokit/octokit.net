using System.Net.Http.Headers;

namespace Octokit.Internal
{
    interface IAuthenticationHandler
    {
        AuthenticationHeaderValue GetAuthorizationHeaderValue(Credentials credentials);
        void Authenticate(IRequest request, Credentials credentials);
    }
}