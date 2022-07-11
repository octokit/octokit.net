using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Used to create an Oauth device flow initiation request.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class OauthDeviceFlowRequest
        : RequestParameters
    {
        /// <summary>
        /// Creates an instance of the OAuth login request with the required parameter.
        /// </summary>
        /// <param name="clientId">The client Id you received from GitHub when you registered the application.</param>
        public OauthDeviceFlowRequest(string clientId)
        {
            Ensure.ArgumentNotNullOrEmptyString(clientId, nameof(clientId));

            ClientId = clientId;
            Scopes = new Collection<string>();
        }

        /// <summary>
        /// The client Id you received from GitHub when you registered the application.
        /// </summary>
        [Parameter(Key = "client_id")]
        public string ClientId { get; private set; }

        /// <summary>
        /// A set of scopes to request. If not provided, scope defaults to an empty list of scopes for users that don’t
        /// have a valid token for the app. For users who do already have a valid token for the app, the user won't be
        /// shown the OAuth authorization page with the list of scopes. Instead, this step of the flow will
        /// automatically complete with the same scopes that were used last time the user completed the flow.
        /// </summary>
        /// <remarks>
        /// See the <see href="https://developer.github.com/v3/oauth/#scopes">scopes documentation</see> for more
        /// information about scopes.
        /// </remarks>
        [Parameter(Key = "scope")]
        public Collection<string> Scopes { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "ClientId: {0}, Scopes: {1}", ClientId, Scopes);
            }
        }
    }
}
