﻿using System;
using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Used to create an Oauth login request.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class OauthTokenRequest : RequestParameters
    {
        /// <summary>
        /// Creates an instance of the OAuth login request with the required parameter.
        /// </summary>
        /// <param name="clientId">The client Id you received from GitHub when you registered the application.</param>
        /// <param name="clientSecret">The client secret you received from GitHub when you registered.</param>
        /// <param name="code">The code you received as a response to making the OAuth login request</param>
        public OauthTokenRequest(string clientId, string clientSecret, string code)
        {
            Ensure.ArgumentNotNullOrEmptyString(clientId, nameof(clientId));
            Ensure.ArgumentNotNullOrEmptyString(clientSecret, nameof(clientSecret));
            Ensure.ArgumentNotNullOrEmptyString(code, nameof(code));

            ClientId = clientId;
            ClientSecret = clientSecret;
            Code = code;
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
        /// The code you received as a response to making the <see cref="IOauthClient.CreateAccessToken">OAuth login
        /// request</see>.
        /// </summary>
        [Parameter(Key = "code")]
        public string Code { get; private set; }

        /// <summary>
        /// The URL in your app where users will be sent after authorization.
        /// </summary>
        /// <remarks>
        /// See the documentation about <see href="https://developer.github.com/v3/oauth/#redirect-urls">redirect urls
        /// </see> for more information.
        /// </remarks>
        [Parameter(Key = "redirect_uri")]
        public Uri RedirectUri { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "ClientId: {0}, ClientSecret: {1}, Code: {2}, RedirectUri: {3}",
                    ClientId,
                    ClientSecret,
                    Code,
                    RedirectUri);
            }
        }
    }
}
