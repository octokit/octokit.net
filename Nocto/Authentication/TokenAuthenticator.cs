using System;
using System.Globalization;
using Nocto.Http;

namespace Nocto.Authentication
{
    public class TokenAuthenticator : IAuthenticationHandler
    {
        public void Authenticate(IRequest request, Credentials credentials)
        {
            Ensure.ArgumentNotNull(request, "request");
            Ensure.ArgumentNotNull(credentials, "credentials");
            Ensure.ArgumentNotNull(credentials.Password, "credentials.Password");

            var token = credentials.GetToken();
            if (credentials.Login != null)
            {
                throw new InvalidOperationException("The Login is not null for a token authentication request. You " + 
                    "probably did something wrong.");
            }
            if (token != null)
            {
                request.Headers["Authorization"] = string.Format(CultureInfo.InvariantCulture, "Token {0}", token);    
            }
        }
    }
}
