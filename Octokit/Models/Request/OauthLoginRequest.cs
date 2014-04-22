using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class OauthLoginRequest : RequestParameters
    {
        /// <summary>
        /// Creates an instance of the OAuth login request with the required parameter.
        /// </summary>
        /// <param name="clientId">The client ID you received from GitHub when you registered the application.</param>
        public OauthLoginRequest(string clientId)
        {
            Ensure.ArgumentNotNullOrEmptyString(clientId, "clientId");

            ClientId = clientId;
            Scopes = new Collection<string>();
        }

        /// <summary>
        /// The client ID you received from GitHub when you registered the application.
        /// </summary>
        [Parameter(Key = "client_id")]
        public string ClientId { get; private set; }

        /// <summary>
        /// The URL in your app where users will be sent after authorization.
        /// </summary>
        /// <remarks>
        /// See the documentation about <see href="https://developer.github.com/v3/oauth/#redirect-urls">redirect urls
        /// </see> for more information.
        /// </remarks>
        [Parameter(Key = "redirect_uri")]
        public Uri RedirectUri { get; set; }

        /// <summary>
        /// A set of scopes to request. If not provided, scope defaults to an empty list of scopes for users that don’t
        /// have a valid token for the app. For users who do already have a valid token for the app, the user won’t be
        /// shown the OAuth authorization page with the list of scopes. Instead, this step of the flow will
        /// automatically complete with the same scopes that were used last time the user completed the flow.
        /// </summary>
        /// <remarks>
        /// See the <see href="https://developer.github.com/v3/oauth/#scopes">scopes documentation</see> for more
        /// information about scopes.
        /// </remarks>
        [Parameter(Key = "scope")]
        public Collection<string> Scopes { get; private set; }

        /// <summary>
        /// An unguessable random string. It is used to protect against cross-site request forgery attacks. In ASP.NET
        /// MVC this would correspond to an anti-forgery token.
        /// </summary>
        [Parameter(Key = "state")]
        public string State { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "ClientId: {0}, RedirectUri: {1}, Scopes: {2}",
                    ClientId,
                    RedirectUri,
                    Scopes);
            }
        }
    }
}
