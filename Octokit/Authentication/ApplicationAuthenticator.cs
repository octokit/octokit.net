using System.Collections.Generic;

namespace Octokit.Internal
{
    class ApplicationAuthenticator : IAuthenticationHandler
    {
        public void Authenticate(IRequest request, Credentials credentials)
        {
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(credentials, nameof(credentials));
            Ensure.ArgumentNotNull(credentials.Login, nameof(credentials.Login));
            Ensure.ArgumentNotNull(credentials.Password, nameof(credentials.Password));

            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                { "client_id", credentials.Login },
                { "client_secret", credentials.Password }
            };

            ((Request)request).Endpoint = request.Endpoint.ApplyParameters(parameters);
        }
    }
}
