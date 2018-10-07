using System;
using System.Globalization;

namespace Octokit.Internal
{
    class BearerTokenAuthenticator : IAuthenticationHandler
    {
        public void Authenticate(IRequest request, Credentials credentials)
        {
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(credentials, nameof(credentials));
            Ensure.ArgumentNotNull(credentials.Password, nameof(credentials.Password));

            if (credentials.Login != null)
            {
                throw new InvalidOperationException("The Login is not null for a token authentication request. You " +
                                                    "probably did something wrong.");
            }

            request.Headers["Authorization"] = string.Format(CultureInfo.InvariantCulture, "Bearer {0}", credentials.Password);
        }
    }
}
