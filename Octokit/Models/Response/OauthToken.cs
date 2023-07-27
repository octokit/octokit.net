using System;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="OauthToken"/> class.
        /// Use this constructor when you don't have a refreshToken.
        /// </summary>
        /// <param name="tokenType">The type of token returned by GitHub.</param>
        /// <param name="accessToken">The access token returned by GitHub.</param>
        /// <param name="scope">The auhtorization scope of the returned token.</param>
        /// <param name="error">The error code returned by the GitHub API.</param>
        /// <param name="errorDescription">The error message, if any, returned by the GitHub API.</param>
        /// <param name="errorUri">The GitHub documentation link, detailing the error message.</param>
        [Obsolete("This constructor is being deprecated and will be removed in the future. Use OauthToken.OauthToken (with refreshToken paramters) instead.")]
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
        /// Initializes a new instance of the <see cref="OauthToken"/> class.
        /// Use this constructor by default.
        /// </summary>
        /// <param name="tokenType">The type of token returned by GitHub.</param>
        /// <param name="accessToken">The access token returned by GitHub.</param>
        /// <param name="expiresIn">The amount of seconds, before the access token expires.</param>
        /// <param name="refreshToken">The refresh token returned by GitHub. Use this, to get a new access token if it expires.</param>
        /// <param name="refreshTokenExpiresIn">The amount of seconds, before the refresh token expires.</param>
        /// <param name="scope">The auhtorization scope of the returned token.</param>
        /// <param name="error">The error code returned by the GitHub API.</param>
        /// <param name="errorDescription">The error message, if any, returned by the GitHub API.</param>
        /// <param name="errorUri">The GitHub documentation link, detailing the error message.</param>
        public OauthToken(string tokenType, string accessToken, int expiresIn, string refreshToken, int refreshTokenExpiresIn, IReadOnlyList<string> scope, string error, string errorDescription, string errorUri)
        {
            this.TokenType = tokenType;
            this.AccessToken = accessToken;
            this.ExpiresIn = expiresIn;
            this.RefreshToken = refreshToken;
            this.RefreshTokenExpiresIn = refreshTokenExpiresIn;
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
        /// The amount of seconds, until the acces token expires.
        /// </summary>
        [Parameter(Key = "expires_in")]
        public int ExpiresIn { get; private set; }

        /// <summary>
        /// The secret refresh token.
        /// Use this to get a new access token, without going through the OAuth flow again.
        /// </summary>
        [Parameter(Key = "refresh_token")]
        public string RefreshToken { get; private set; }

        /// <summary>
        /// The amount of seconds, until the refresh token expires.
        /// </summary>
        [Parameter(Key = "refresh_token_expires_in")]
        public int RefreshTokenExpiresIn { get; private set; }

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
