using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class OauthToken
    {
        /// <summary>
        /// The type of OAuth token
        /// </summary>
        public string TokenType { get; set; }

        /// <summary>
        /// The secret OAuth access token. Use this to authenticate Octokit.net's client.
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// The list of scopes the token includes.
        /// </summary>
        public IReadOnlyCollection<string> Scope { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "TokenType: {0}, AccessToken: {1}, Scopes: {2}",
                    TokenType,
                    AccessToken,
                    Scope);
            }
        }
    }
}
