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

        public OauthToken(string tokenType, string accessToken, IReadOnlyList<string> scope, string error, string errorDescription, string errorUri)
        {
            this.TokenType = tokenType;
            this.AccessToken = accessToken;
            this.Scope = scope;
            this.Error = error;
            this.ErrorDescription = errorDescription;
            this.ErrorUri = errorUri;
        }

        /// <summary>
        /// The type of OAuth token
        /// </summary>
        public string TokenType { get; private set; }

        /// <summary>
        /// The secret OAuth access token. Use this to authenticate Octokit.net's client.
        /// </summary>
        public string AccessToken { get; private set; }

        /// <summary>
        /// The list of scopes the token includes.
        /// </summary>
        public IReadOnlyList<string> Scope { get; private set; }

        /// <summary>
        /// Gets or sets the error code or the response.
        /// </summary>
        [Parameter(Key = "error")]
        public string Error { get; private set; }

        /// <summary>
        /// Gets or sets the error description.
        /// </summary>
        [Parameter(Key = "error_description")]
        public string ErrorDescription { get; private set; }

        /// <summary>
        /// Gets or sets the error uri.
        /// </summary>
        [Parameter(Key = "error_uri")]
        public string ErrorUri { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "TokenType: {0}, AccessToken: {1}, Scopes: {2}",
                    this.TokenType,
                    this.AccessToken,
                    this.Scope);
            }
        }
    }
}
