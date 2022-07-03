using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Used to create an Oauth login request for the device flow.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    internal class OauthTokenRequestForDeviceFlow : RequestParameters
    {
        /// <summary>
        /// Creates an instance of the OAuth login request with the required parameter.
        /// </summary>
        /// <param name="clientId">The client Id you received from GitHub when you registered the application.</param>
        /// <param name="deviceCode">The device code you received from the device flow initiation call.</param>
        public OauthTokenRequestForDeviceFlow(string clientId, string deviceCode)
        {
            Ensure.ArgumentNotNullOrEmptyString(clientId, nameof(clientId));
            Ensure.ArgumentNotNullOrEmptyString(deviceCode, nameof(deviceCode));

            ClientId = clientId;
            DeviceCode = deviceCode;
        }

        /// <summary>
        /// The client Id you received from GitHub when you registered the application.
        /// </summary>
        [Parameter(Key = "client_id")]
        public string ClientId { get; private set; }

        /// <summary>
        /// The device code you received from the device flow initiation call.
        /// </summary>
        [Parameter(Key = "device_code")]
        public string DeviceCode { get; private set; }

        /// <summary>
        /// The authorization grant type.
        /// </summary>
        [Parameter(Key = "grant_type")]
        public string GrantType => "urn:ietf:params:oauth:grant-type:device_code";

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "ClientId: {0}, DeviceCode: {1}",
                    ClientId,
                    DeviceCode);
            }
        }
    }
}
