using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class AccessToken
    {
        public AccessToken() { }

        public AccessToken(string token, DateTimeOffset expiresAt)
        {
            Token = token;
            ExpiresAt = expiresAt;
        }

        /// <summary>
        /// The access token
        /// </summary>
        public string Token { get; private set; }

        /// <summary>
        /// The expiration date
        /// </summary>
        public DateTimeOffset ExpiresAt { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Token: {0}, ExpiresAt: {1}", Token, ExpiresAt);
            }
        }
    }
}
