using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class AccessToken
    {
        public AccessToken() { }

        public AccessToken(string token, DateTime expiresAt)
        {
            Token = token;
            ExpiresAt = expiresAt;
        }

        public string Token { get; protected set; }
        public DateTime ExpiresAt { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Token: {0}, ExpiresAt: {1}", Token, ExpiresAt);
            }
        }
    }
}
