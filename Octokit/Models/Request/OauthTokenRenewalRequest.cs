using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Used to create an Oauth login request.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class OauthTokenRenewalRequest : RequestParameters
    {
        /// <summary>
        /// Creates an instance of the OAuth token refresh request.
        /// </summary>
        /// <param name="clientId">The client Id you received from GitHub when you registered the application.</param>
        /// <param name="clientSecret">The client secret you received from GitHub when you registered.</param>
        /// <param name="refreshToken">The refresh token you received when making the original oauth token request.</param>
        public OauthTokenRenewalRequest(string clientId, string clientSecret, string refreshToken)
        {
            Ensure.ArgumentNotNullOrEmptyString(clientId, nameof(clientId));
            Ensure.ArgumentNotNullOrEmptyString(clientSecret, nameof(clientSecret));
            Ensure.ArgumentNotNullOrEmptyString(refreshToken, nameof(refreshToken));

            ClientId = clientId;
            ClientSecret = clientSecret;
            RefreshToken = refreshToken;
        }

        /// <summary>
        /// The client Id you received from GitHub when you registered the application.
        /// </summary>
        [Parameter(Key = "client_id")]
        public string ClientId { get; private set; }

        /// <summary>
        /// The client secret you received from GitHub when you registered.
        /// </summary>
        [Parameter(Key = "client_secret")]
        public string ClientSecret { get; private set; }

        /// <summary>
        /// The type of grant. Should be ommited, unless renewing an access token.
        /// </summary>
        [Parameter(Key = "grant_type")]
        public string GrantType { get; private set; } = "refresh_token";

        /// <summary>
        /// The refresh token you received as a response to making the <see cref="IOauthClient.CreateAccessToken">OAuth login
        /// request</see>.
        /// </summary>
        [Parameter(Key = "refresh_token")]
        public string RefreshToken { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "ClientId: {0}, ClientSecret: {1}, GrantType: {2}, RefreshToken: {3}",
                    ClientId,
                    ClientSecret,
                    GrantType,
                    RefreshToken);
            }
        }
    }
}
