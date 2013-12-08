﻿using System;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace Octokit.Internal
{
    class BasicAuthenticator : IAuthenticationHandler
    {
        ///<summary>
        /// 
        ///</summary>
        ///<param name="request"></param>
        ///<param name="credentials">Login & Password</param>
        public void Authenticate(IRequest request, Credentials credentials)
        {
            Ensure.ArgumentNotNull(request, "request");
            Ensure.ArgumentNotNull(credentials, "credentials");
            Ensure.ArgumentNotNull(credentials.Login, "credentials.Login");
            Debug.Assert(credentials.Password != null, "It should be impossible for the password to be null");

            var header = string.Format(
                CultureInfo.InvariantCulture,
                "Basic {0}",
                Convert.ToBase64String(Encoding.UTF8.GetBytes(
                    string.Format(CultureInfo.InvariantCulture, "{0}:{1}", credentials.Login, credentials.Password))));

            request.Headers["Authorization"] = header;
        }
    }
}
