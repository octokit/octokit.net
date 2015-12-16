using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class OauthToken
    {
        public OauthToken() { }

        public OauthToken(string tokenType, string accessToken, IReadOnlyList<string> scope)
        {
            TokenType = tokenType;
            AccessToken = accessToken;
            Scope = scope;
        }

        /// <summary>
        /// The type of OAuth token
        /// </summary>
        public string TokenType { get; protected set; }

        /// <summary>
        /// The secret OAuth access token. Use this to authenticate Octokit.net's client.
        /// </summary>
        public string AccessToken { get; protected set; }

        /// <summary>
        /// The list of scopes the token includes.
        /// </summary>
        public IReadOnlyList<string> Scope { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "TokenType: {0}, AccessToken: {1}, Scopes: {2}",
                    TokenType,
                    AccessToken,
                    Scope);
            }
        }
    }
}
