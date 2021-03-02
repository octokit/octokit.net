using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

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

        /// <summary>
        /// Gets or sets the error code or the response.
        /// </summary>
        [Parameter(Key = "error")]
        public string Error { get; set; }

        /// <summary>
        /// Gets or sets the error description.
        /// </summary>
        [Parameter(Key = "error_description")]
        public string ErrorDescription { get; set; }

        /// <summary>
        /// Gets or sets the error uri.
        /// </summary>
        [Parameter(Key = "error_uri")]
        public string ErrorUri { get; set; }

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
