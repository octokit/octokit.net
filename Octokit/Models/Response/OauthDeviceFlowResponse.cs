using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class OauthDeviceFlowResponse
    {
        public OauthDeviceFlowResponse() { }

        public OauthDeviceFlowResponse(string deviceCode, string userCode, string verificationUri, int expiresIn, int interval)
        {
            DeviceCode = deviceCode;
            UserCode = userCode;
            VerificationUri = verificationUri;
            ExpiresIn = expiresIn;
            Interval = interval;
        }

        /// <summary>
        /// The device verification code is 40 characters and used to verify the device.
        /// </summary>
        public string DeviceCode { get; private set; }

        /// <summary>
        /// The user verification code is displayed on the device so the user can enter the code in a browser. This code is 8 characters with a hyphen in the middle.
        /// </summary>
        public string UserCode { get; private set; }

        /// <summary>
        /// The verification URL where users need to enter the UserCode: https://github.com/login/device.
        /// </summary>
        public string VerificationUri { get; private set; }

        /// <summary>
        /// The number of seconds before the DeviceCode and UserCode expire. The default is 900 seconds or 15 minutes.
        /// </summary>
        public int ExpiresIn { get; private set; }

        /// <summary>
        /// The minimum number of seconds that must pass before you can make a new access token request (POST https://github.com/login/oauth/access_token) to complete the device authorization.
        /// For example, if the interval is 5, then you cannot make a new request until 5 seconds pass. If you make more than one request over 5 seconds, then you will hit the rate limit
        /// and receive a slow_down error.
        /// </summary>
        public int Interval { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "DeviceCode: {0}, UserCode: {1}, VerificationUri: {2}, ExpiresIn: {3}, Interval: {4}",
                    DeviceCode,
                    UserCode,
                    VerificationUri,
                    ExpiresIn,
                    Interval);
            }
        }
    }
}
