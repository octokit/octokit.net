using System;
using System.Diagnostics;
using System.Globalization;

// FIXME: What is this project's policy on generic types, inheritence, etc?
// We could create a Token base class and have specific types for various operations, but I'm not sure what's best based on established practice.

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RunnerRegistrationToken
    {
        public RunnerRegistrationToken() { }

        public RunnerRegistrationToken(string token, DateTimeOffset expiresAt)
        {
            Token = token;
            ExpiresAt = expiresAt;
        }

        /// <summary>
        /// The access token
        /// </summary>
        public string Token { get; protected set; }

        /// <summary>
        /// The expiration date
        /// </summary>
        public DateTimeOffset ExpiresAt { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Token: {0}, ExpiresAt: {1}", Token, ExpiresAt);
            }
        }
    }
}
