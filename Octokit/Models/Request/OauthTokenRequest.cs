using System;

namespace Octokit
{
    public class OauthTokenRequest : RequestParameters
    {
                /// <summary>
        /// Creates an instance of the OAuth login request with the required parameter.
        /// </summary>
        /// <param name="clientId">The client ID you received from GitHub when you registered the application.</param>
        /// <param name="clientSecret">The client secret you received from GitHub when you registered.</param>
        /// <param name="code">The code you received as a response to making the
        /// <see cref="IOAuthClient.GetGitHubLoginUrl">OAuth login request</see></param>
        public OauthTokenRequest(string clientId, string clientSecret, string code)
        {
            Ensure.ArgumentNotNullOrEmptyString(clientId, "clientId");
            Ensure.ArgumentNotNullOrEmptyString(clientSecret, "clientSecret");
            Ensure.ArgumentNotNullOrEmptyString(code, "code");

            ClientId = clientId;
            ClientSecret = clientSecret;
            Code = code;
        }

        /// <summary>
        /// The client ID you received from GitHub when you registered the application.
        /// </summary>
        public string ClientId { get; private set; }

        /// <summary>
        /// The client secret you received from GitHub when you registered.
        /// </summary>
        public string ClientSecret { get; private set; }

        /// <summary>
        /// The code you received as a response to making the <see cref="IOAuthClient.GetGitHubLoginUrl">OAuth login
        /// request</see>.
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// The URL in your app where users will be sent after authorization.
        /// </summary>
        /// <remarks>
        /// See the documentation about <see href="https://developer.github.com/v3/oauth/#redirect-urls">redirect urls
        /// </see> for more information.
        /// </remarks>
        public Uri RedirectUri { get; set; }
    }
}
